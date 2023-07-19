using OpenTK.Windowing.Desktop;
using ToolsSolution.Rendering;
using ToolsSolution.Rendering.Gui;
using ToolsSolution.Rendering.Gui.Windows;

public class Program
{
    static void Main()
    {
        UIWindowRenderer.AddWindow(new TestWindow("test window"));
        
        MainWindow.Get();
    }
}