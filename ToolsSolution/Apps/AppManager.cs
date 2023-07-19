// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

namespace ToolsSolution.Apps;

public static class AppManager
{
    private static List<IApp> _apps = new();

    public static IApp? CurrentApp { get; set; } = null;

    public static void AddApp(IApp app)
    {
        _apps.Add(app);
        CurrentApp = app;
    }
}