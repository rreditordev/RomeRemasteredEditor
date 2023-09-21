using Godot;
using Model;
using System;
using UIUtilities;

public partial class ResourceRemoveBtn : Button
{
    [Export]
    public PanelInteractions PanelInteractions;
    [Export]
    public Map2d map;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                var res = FilterClasses.FilterResource(
                    RtwDataContext.Campaign.GetResources(), 
                    int.Parse(PanelInteractions.xCoordInput.Text), 
                    int.Parse(PanelInteractions.yCoordInput.Text)
                );
                DeleteClasses.RemoveResourceFromArray(res);
                map.DisplayResources();
                PanelInteractions.ResourceEditorContainer.Visible = false;
                PanelInteractions.resourceAddBtn.Visible = true;
                PanelInteractions.resourceHeaderBtn.Visible = false;
                PanelInteractions.resourceRemoveBtn.Visible = false;
            }
        }
    }
}
