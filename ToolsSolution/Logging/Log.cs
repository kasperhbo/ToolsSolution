// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using ImGuiNET;

namespace ToolsSolution.Logging;


public static class Log
{
    private static List<string> _logs = new();
    private static List<string> _warnings = new();
    private static List<string> _errors = new();
    private static List<string> _succes = new();
    
    
    private static bool debugging = true;
    public static void Message([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, object? arg0)
    {
        if (!debugging) return;
        Message(string.Format(format, arg0));
    }

    public static void Message(string? message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, bool showFile = true,
        bool showLine = true, bool showFunction = true)
    {
        if (!debugging) return;
        Console.ForegroundColor = ConsoleColor.White;
        string logst = GetString(showFile, showLine, showFunction, message, memberName, sourceFilePath,
            sourceLineNumber);
        
        Console.WriteLine(logst);
        
        _logs.Add(logst);
        
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Succes([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, object? arg0)
    {
        if (!debugging) return;
        Succes(string.Format(format, arg0));
    }

    public static void Succes(string? message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, bool showFile = true,
        bool showLine = true, bool showFunction = true)
    {
        if (!debugging) return;
        Console.ForegroundColor = ConsoleColor.Green;
        string logst = GetString(showFile, showLine, showFunction, message, memberName, sourceFilePath,
            sourceLineNumber);
        Console.WriteLine(logst);
        _succes.Add(logst);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Warning([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, object? arg0)
    {
        if (!debugging) return;
        Warning(string.Format(format, arg0));
    }

    public static void Warning(string? message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, bool showFile = true,
        bool showLine = true, bool showFunction = true)
    {
        if (!debugging) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        string logst = GetString(showFile, showLine, showFunction, message, memberName, sourceFilePath,
            sourceLineNumber);
        
        Console.WriteLine(logst);
        _warnings.Add(logst);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Error([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format, object? arg0)
    {
        if (!debugging) return;
        Error(string.Format(format, arg0));
    }

    public static void Error(string? message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, bool showFile = true,
        bool showLine = true, bool showFunction = true)
    {
        if (!debugging) return;
        Console.ForegroundColor = ConsoleColor.Red;
        string logst = GetString(showFile, showLine, showFunction, message, memberName, sourceFilePath,
            sourceLineNumber);
        Console.WriteLine(logst);
        Console.ForegroundColor = ConsoleColor.White;
        
    }


    private static string GetString(bool showFile, bool showLine, bool showFunction, string? message, string memberName,
        string sourceFilePath, int sourceLineNumber)
    {
        
        var sb = new StringBuilder();
        sb.Append("[" + "time: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "]");
        
        if (showFunction)
            sb.Append(" | " + memberName);
        if (showFile)
            sb.Append(" | " + sourceFilePath);
        if (showLine)
            sb.Append(" | " + sourceLineNumber);

        sb.Append(" | " + message);

        return sb.ToString();
    }

    internal static void OnGui()
    {
        ImGui.Begin("Logs");
        ImGui.BeginTabBar("Logs");
        if (ImGui.BeginTabItem("Logs"))
        {
            ImGui.BeginChild("Logs");
            foreach (var log in _logs)
            {
                ImGui.Text(log);
            }
            ImGui.SetScrollHereY(0.999f);
            ImGui.EndChild();
            ImGui.EndTabItem();
        }
        
        if (ImGui.BeginTabItem("Warnings"))
        {
            ImGui.BeginChild("Warnings");
            foreach (var log in _warnings)
            {
                ImGui.Text(log);
            }
            ImGui.SetScrollHereY(0.999f);
            ImGui.EndChild();
            ImGui.EndTabItem();
        }
        
        if (ImGui.BeginTabItem("Errors"))
        {
            ImGui.BeginChild("Errors");
            foreach (var log in _errors)
            {
                ImGui.Text(log);
            }
            ImGui.SetScrollHereY(0.999f);
            ImGui.EndChild();
            ImGui.EndTabItem();
        }
        
        if (ImGui.BeginTabItem("Succes"))
        {
            ImGui.BeginChild("succes");
            foreach (var log in _succes)
            {
                ImGui.Text(log);
            }

            ImGui.SetScrollHereY(0.999f);
            ImGui.EndChild();
            ImGui.EndTabItem();
        }
        
        ImGui.EndTabBar();
        ImGui.End();
    }
}