// Created by: Kasper de Bruin
// On: 2023 - 07 - 19
// 
// This is under the MIT License

using ToolsSolution.Rendering.Gui;

namespace ToolsSolution.Apps;

public interface IApp
{
    public string Name { get; set; }
    public List<GuiWindow> WindowsToRemove {get; set;}
    public List<GuiWindow> Windows {get; set;}
    
}