using ImGuiNET;
using SshApp.UIWindows;
using ToolsSolution.Apps;
using ToolsSolution.Logging;
using ToolsSolution.Rendering.Gui;

namespace SshApp;

public class SshApplication : IApp
{
    public string Name { get; set; }="SSH Application";
    public List<GuiWindow> WindowsToRemove { get; set; }=new();
    public List<GuiWindow> Windows { get; set; }=new();
    
    internal static ConnectionManagerWindow ConnectionManagerWindow { get; set; }=new();
    
    public SshApplication()
    {
        Windows.Add(new TestWindow("test window"));
        Windows.Add(ConnectionManagerWindow);
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