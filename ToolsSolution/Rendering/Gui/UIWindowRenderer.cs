// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ImGuiNET;
using OpenTK.Windowing.Common;
using ToolsSolution.Apps;
using ToolsSolution.Logging;

namespace ToolsSolution.Rendering.Gui;

internal static class UIWindowRenderer
{
    private static ImGuiController _controller;

    private static bool _renderWindows = false;
    
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
        AppManager.CurrentApp?.Windows.Add(window);
    }
    
    internal static void RemoveWindow(GuiWindow window)
    {
        AppManager.CurrentApp?.Windows.Add(window);
    }
    
    internal static void Render(FrameEventArgs args)
    {
        _controller.Update(MainWindow.Get(), (float)args.Time);
        
        AppManager.CurrentApp?.WindowsToRemove.Clear();

        ImGui.DockSpaceOverViewport();
        
        ImGui.ShowDemoWindow();
        ImGui.ShowAboutWindow();
        
        if (AppManager.CurrentApp?.Windows != null && _renderWindows)
        {
            foreach (var window in AppManager.CurrentApp?.Windows)
            {
                window.Render(args);
            }

            foreach (var window in AppManager.CurrentApp?.WindowsToRemove)
            {
                AppManager.CurrentApp?.Windows.Remove(window);
            }
        }

        Log.OnGui();
        
        _controller.Render();
    }

    internal static void Resize(ResizeEventArgs args)
    {
        _controller.WindowResized(args);
    }

    internal static void TextInput(TextInputEventArgs args)
    {
        _controller.PressChar(args);
    }
}