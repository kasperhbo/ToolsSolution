using ImGuiNET;
using ToolsSolution.Apps;
using ToolsSolution.Logging;
using ToolsSolution.Rendering.Gui;

namespace SshApp;

public class SSHAPP : IApp
{
    public string Name { get; set; }="SSH Application";
    public List<GuiWindow> WindowsToRemove { get; set; }=new();
    public List<GuiWindow> Windows { get; set; }=new();
    
    public SSHAPP()
    {
        Windows.Add(new TestWindow("test window"));
    }

    private class TestWindow : GuiWindow
    {
        public TestWindow(string title) : base(title)
        {
        }

        protected override void OnGui()
        {
            ImGui.Begin("test window");
            ImGui.End();
        }
    }
}