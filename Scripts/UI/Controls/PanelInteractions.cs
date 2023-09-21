using Godot;
using System;

public partial class PanelInteractions : Panel
{
    private bool drag = false;
    private Vector2 difference;
    private Vector2 origin;

    [ExportGroup("Script References")]
    [Export]
    public TileSelector tileSelector;

    [ExportGroup("Coordinates")]
    [Export]
    public LineEdit xCoordInput;
    [Export]
    public LineEdit yCoordInput;

    [ExportGroup("Header Buttons")]
    [Export]
    public Button resourceHeaderBtn;
    [Export]
    public Button resourceAddBtn;
    [Export]
    public Button resourceRemoveBtn;
    [Export]
    public Button landmarkHeaderBtn;
    [Export]
    public Button characterHeaderBtn;

    [ExportGroup("Resource UI")]
    [Export]
    public Control ResourceEditorContainer;
    [Export]
    public LineEdit resourceIDInput;
    [Export]
    public LineEdit regionTagInput;
    [Export]
    public OptionButton abundanceDD;

    //Catches any clicks or other events on the panel
    public override void _GuiInput(InputEvent @event) {
        if (Input.IsMouseButtonPressed(MouseButton.Left)) {
            Vector2 mousePos = GetGlobalMousePosition();
            difference = mousePos - Position;
            if (drag == false) {
                drag = true;
                origin = difference;
            }
        } else {
            drag = false;
        }

        if (drag == true) {
            Position = Position - origin + difference;
        }  
    }
}
