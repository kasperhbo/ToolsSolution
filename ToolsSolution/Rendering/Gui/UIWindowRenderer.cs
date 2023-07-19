// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ImGuiNET;
using OpenTK.Windowing.Common;
using ToolsSolution.Logging;

namespace ToolsSolution.Rendering.Gui;

internal static class UIWindowRenderer
{
    private static ImGuiController _controller;
    
    private static List<GuiWindow> _windows = new List<GuiWindow>();
    private static List<GuiWindow> _windowsToRemove = new List<GuiWindow>();

    static UIWindowRenderer()
    {
  
    }

    internal static void Init()
    {
        _controller = new ImGuiController();
        _controller.Init(MainWindow.Get().ClientSize.X, MainWindow.Get().ClientSize.Y);
    }
    
    internal static void AddWindow(GuiWindow window)
    {
        _windows.Add(window);
    }
    
    internal static void RemoveWindow(GuiWindow window)
    {
        _windowsToRemove.Add(window);
    }
    
    internal static void Render(FrameEventArgs args)
    {
        Log.Message("render ui");
        _controller.Update(MainWindow.Get(), (float)args.Time);
        
        _windowsToRemove.Clear();

        ImGui.DockSpaceOverViewport();
        
        ImGui.ShowDemoWindow();
        ImGui.ShowAboutWindow();

        foreach (var window in _windows)
        {
            window.Render(args);
        }
        
        foreach (var window in _windowsToRemove)
        {
            _windows.Remove(window);
        }

        _controller.Render();
    }

    internal static void Resize(ResizeEventArgs args)
    {
        _controller.WindowResized(args);
    }
}