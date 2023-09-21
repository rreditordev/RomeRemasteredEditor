using Godot;
using System;
using UIUtilities;

public partial class ResourceAddBtn : Button
{
    [Export]
    public PanelInteractions PanelInteractions;
    [Export]
    public Map2d map;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                Model.Resource res = new Model.Resource("amber", 1, "Change Me");
                res._mapPosition = new Vector2I(int.Parse(PanelInteractions.xCoordInput.Text), int.Parse(PanelInteractions.yCoordInput.Text));
                SaveClasses.AddResourceToArray(res);
                map.DisplayResources();
                PanelInteractions.ResourceEditorContainer.Visible = true;
                PanelInteractions.resourceAddBtn.Visible = false;
                PanelInteractions.resourceHeaderBtn.Visible = true;
                PanelInteractions.resourceRemoveBtn.Visible = true;
                PanelInteractions.resourceIDInput.Text = res.ResourceID;
                PanelInteractions.regionTagInput.Text = res.RegionTag;
                PanelInteractions.abundanceDD.Selected = res.AbundanceLevel - 1;
            }
        }
    }

}
