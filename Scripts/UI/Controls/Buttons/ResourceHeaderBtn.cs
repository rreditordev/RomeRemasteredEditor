using Godot;
using System;

public partial class ResourceHeaderBtn : Button
{
	[Export]
	public Control ResourceEditor;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                if(ResourceEditor.Visible)
                    ResourceEditor.Visible = false;
                else
                    ResourceEditor.Visible = true;
            }
        }
    }
}
