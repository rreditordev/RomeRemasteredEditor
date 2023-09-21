using Godot;
using System;

public partial class ClosePlanelBtn : Button
{
	[Export]
	public Panel panel;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                panel.Visible = false;
            }
        }
    }
}
