using OpenTK.Graphics.ES11;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using ToolsSolution.Logging;
using ToolsSolution.Rendering.Gui;

namespace ToolsSolution.Rendering;

public class MainWindow : GameWindow
{  
    public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
        
    }

    protected override void OnLoad()
    {
        Log.Message("Starting up...");
        base.OnLoad();

        base.Resize += UIWindowRenderer.Resize;

        UpdateFrame += Update;
        RenderFrame += Render;
        base.Resize += Resize;
        base.TextInput += TextInput;
        
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        UIWindowRenderer.Init();
        
        Log.Succes("Started up!");
    }



    private void Render(FrameEventArgs frameEventArgs)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        UIWindowRenderer.Render(frameEventArgs);
        SwapBuffers();
    }

    private void Update(FrameEventArgs frameEventArgs)
    {
        
    }

    private void Resize(ResizeEventArgs obj)
    {
        Log.Message("Resizing window...");
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        Log.Succes(String.Format("New size: {0}x{1}", ClientSize.X, ClientSize.Y));
    }

    private void TextInput(TextInputEventArgs args)
    {
        UIWindowRenderer.TextInput(args);
    }
    
    private static MainWindow? _instance = null;

    public static GameWindow Get()
    {
        if(_instance == null)
        {
            _instance = new MainWindow(GameWindowSettings.Default, NativeWindowSettings.Default);
            _instance.Run();
        }
        return _instance;
    }
}