using Godot;
using Model;
using System;
using UIUtilities;

public partial class SavePanelBtn : Button
{
    [ExportGroup("Script References")]
    [Export]
    public PanelInteractions panelInteractions;
    [Export]
    public Map2d map;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton mb) {
            if (mb.ButtonIndex == MouseButton.Left && !mb.Pressed) {
                SaveResource();
            }
        }
    }

    void SaveResource() {
        if (panelInteractions.ResourceEditorContainer.Visible) {
            Model.Resource saveRes = new Model.Resource(
                panelInteractions.resourceIDInput.Text,
                int.Parse(panelInteractions.abundanceDD.Text),
                panelInteractions.regionTagInput.Text
            );
            saveRes._mapPosition = new Vector2I( int.Parse(panelInteractions.xCoordInput.Text), int.Parse(panelInteractions.yCoordInput.Text));
            SaveClasses.SaveResourceInArray(saveRes);
            map.DisplayResources();
            RtwDataContext.Save();
        }
    }


}
