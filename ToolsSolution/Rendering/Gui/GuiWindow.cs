// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ImGuiNET;
using OpenTK.Windowing.Common;

namespace ToolsSolution.Rendering.Gui;

internal abstract class GuiWindow
{
    private string _title = "";
    private int UID { get; set; } = -1;
    
    internal bool IsVisible { get; set; } = true;

    protected GuiWindow(string title)
    {
        _title = title;
        UID = UIDManager.GetUID();
    }

    public void Render(FrameEventArgs frameEventArgs)
    {
        if (IsVisible) RenderWindow();
    }
    
    private void RenderWindow()
    {
        ImGui.Begin(_title);
        ImGui.PushID(UID);
        
        OnGui();
        
        ImGui.PopID();
        ImGui.End();
    }

    protected abstract void OnGui();
}