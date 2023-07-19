// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ImGuiNET;

namespace ToolsSolution.Rendering.Gui.Windows;

internal class TestWindow : GuiWindow
{
    public TestWindow(string title) : base(title)
    {
    }

    protected override void OnGui()
    {
        ImGui.Text("Hello World!");
        ImGui.Button("Test Button");
    }
}