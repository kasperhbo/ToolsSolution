// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

namespace ToolsSolution.Rendering.Gui;

internal static class UIDManager
{
    private static int _sUidcounter;
    public static readonly List<int> TakenUids = new();

    internal static int GetUID()
    {
        _sUidcounter++;
        //Recursive call
        if (TakenUids.Contains(_sUidcounter))
        {
            return GetUID();
        }
 
        TakenUids.Add(_sUidcounter);
        return _sUidcounter;
    }
}