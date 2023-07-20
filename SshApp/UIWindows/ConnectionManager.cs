// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ImGuiNET;
using ToolsSolution.Logging;
using ToolsSolution.Rendering.Gui;

namespace SshApp.UIWindows;

internal class ConnectionManagerWindow : GuiWindow
{
    private List<Connection> _connections = new();
    
    public ConnectionManagerWindow() : base("Connection Manager")
    {
    }

    private string _host="172.18.45.200";
    private string _username="root";
    private string _password=".root";
    
    protected override void OnGui()
    {
        ImGui.BeginTabBar("Connections Manager");

        if (ImGui.BeginTabItem("New Connection"))
        {
            ImGui.InputText("Host", ref _host, 100);
            ImGui.InputText("Username", ref _username, 100);
            ImGui.InputText("Password", ref _password, 100);

            if (ImGui.Button("Connect"))
            {
                Connection connection = new Connection(_host, _username, _password);
                Log.Message("trying to connect to: " + _host);
                if (connection.Connect())
                {
                    _connections.Add(connection);
                    Log.Succes("Succesfully connected to: " + _host);
                }
                else
                {
                    Log.Error("Failed to connect to: " + _host);
                }
            }
            
            ImGui.EndTabItem();
        }
        
        if (ImGui.BeginTabItem("All Connections"))
        {
            foreach (var connection in _connections)
            {
                ImGui.Text(connection.Host);
                
            }
            
            ImGui.EndTabItem();
        }

        foreach (var connection in _connections)
        {
            if (ImGui.BeginTabItem(connection.Host))
            {
                connection.OnGui();
                ImGui.EndTabItem();
            }
            
        }
        
        ImGui.EndTabBar();
        
    }
}