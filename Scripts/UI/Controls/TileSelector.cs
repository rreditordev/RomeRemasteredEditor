using Godot;
using Model;
using System;
using UIUtilities;

public partial class TileSelector : Node2D
{
    [ExportGroup("Instance References")]
    [Export]
    public PackedScene indicatorScene;
    [Export]
    public Panel panel;
    public PanelInteractions panelInteractions;
    [Export]
    public Node2D root;

    public Vector2 Coords;
    Node2D indicator;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        panelInteractions = panel as PanelInteractions;
    }

    public void CreateIndicator(Map2d map) {

        ClearAllUISections();
        Vector2 mousePos = GetGlobalMousePosition();
        Coords = UnitConversions.WorldToGameCoordinates(mousePos, map.gameHeight, map.gameWidth, map.Scale);//Get game coordinates
        var indicatorPos = UnitConversions.GameToWorldCoordinates(map.gameHeight, map.gameWidth, (int)Coords.X, (int)Coords.Y, map.Scale);//get center coordinate for that tile
        var inst = indicatorScene.Instantiate<Node2D>();
        inst.Position = indicatorPos;

        if (indicator != null) {
            indicator.Free();
        }
        indicator = inst;
        indicator.Name = "Indicator";
        root.AddChild(indicator);

        if(!panel.Visible) { 
            panel.Visible = true;
        }
        panelInteractions.xCoordInput.Text = Coords.X.ToString();
        panelInteractions.yCoordInput.Text = Coords.Y.ToString();
        PopulateResourceUI((int)Coords.X, (int)Coords.Y);
    }

    //Handle resource checks on clicked on tile
    void PopulateResourceUI(int x, int y) {
        var res = FilterClasses.FilterResource(RtwDataContext.Campaign.GetResources(), x, y);
        if(res != null) {
            panelInteractions.resourceHeaderBtn.Visible = true;
            panelInteractions.resourceRemoveBtn.Visible = true;
            panelInteractions.ResourceEditorContainer.Visible = true;
            panelInteractions.resourceIDInput.Text = res.ResourceID;
            panelInteractions.regionTagInput.Text = res.RegionTag;
            panelInteractions.abundanceDD.Selected = res.AbundanceLevel - 1;
        } else {
            panelInteractions.ResourceEditorContainer.Visible = false;
            panelInteractions.resourceAddBtn.Visible = true;
        }
    }

    void ClearAllUISections() {
        panelInteractions.ResourceEditorContainer.Visible = false;
        panelInteractions.characterHeaderBtn.Visible = false;
        panelInteractions.landmarkHeaderBtn.Visible = false;
        panelInteractions.resourceHeaderBtn.Visible = false;
        panelInteractions.resourceAddBtn.Visible = false;
        panelInteractions.resourceRemoveBtn.Visible = false;
    }

}
