// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using System.Numerics;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Cryptography;
using ImGuiNET;
using Renci.SshNet;
using ToolsSolution.Logging;
using X11;

namespace SshApp.UIWindows;

internal class Connection
{
    public SshClient? SshClient = null;
    
    public string Host { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    
    private List<string> _messages = new();
    private int _commandIndex = -1;
    private string _command = "";
    private string _setCommand = "";
    

    public Connection(string host, string username, string password)
    {
        Host = host;
        Username = username;
        Password = password;

        try
        {
            SshClient = new SshClient(host, username, password);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
        }
    }

    private ShellStream? shellStreamSSH = null;
    public bool Connect()
    {
        if (SshClient != null)
        {
            SshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(120);
            SshClient?.Connect();

            shellStreamSSH = SshClient?.CreateShellStream("vt-100", 80, 50, 1024, 1024, 1024);

            Thread thread = new Thread(() => recvSSHData(shellStreamSSH));

            thread.Start();

            return SshClient is { IsConnected: true };
        }

        return false;
    }
    
    public void recvSSHData(ShellStream shellStreamSSH)
    {
        while (true)
        {
            try
            {
                if (shellStreamSSH != null && shellStreamSSH.DataAvailable)
                {
                    string strData = shellStreamSSH.Read();
                    _messages.Add(strData);
                    
                }
            }
            catch
            {
                Log.Error("Failed to read data from ssh");
            }
            System.Threading.Thread.Sleep(200);
        }
        
    }

    public void OnGui()
    {
        if(SshClient != null && !SshClient.IsConnected)
        {
            if (ImGui.Button("Connect"))
            {
                Connect();
            }
        }
        else if(SshClient != null && SshClient.IsConnected)
        {
            if (ImGui.Button("Disconnect"))
            {
                SshClient?.Disconnect();
            }
            ImGui.Text("Connected with: " + SshClient.ConnectionInfo.Username + "@" + SshClient.ConnectionInfo.Host);
            
            TerminalUI();
        }
    }

    private void TerminalUI()
    {
        //rest of the gui
        ImGui.BeginChild("terminal: " + Username + "@" + SshClient.ConnectionInfo.Host, 
            new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y - 55),
            true);

        for (int i = 0; i < _messages.Count; i++)
        {
            ImGui.Text(_messages[i]);
        }
        
        
        ImGui.EndChild();

        ImGui.Text(_command);
        
        if (ImGui.InputText("Command: ", ref _command, 2000, ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AlwaysOverwrite))
        {
            RunCommand();
        }

        ImGui.SameLine();
        if (ImGui.Button("Run"))
        {
            RunCommand();
        }
        
        
        
        
    }

    private void RunCommand()
    {
        shellStreamSSH?.Write(_command + "\n");
        shellStreamSSH?.Flush();

        while (shellStreamSSH != null && shellStreamSSH.DataAvailable)
        {
            string strData = shellStreamSSH.Read();
            Console.WriteLine(strData);
        }
    }
}