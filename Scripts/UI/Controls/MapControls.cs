using Godot;
using System;
public partial class MapControls : Camera2D
{
    //[Export]
    //public float alterSpeed;

    [Export]
    public float maxZoom = 250;
    [Export]
    public float minZoom = 15;

    [Export]
    public Map2d map;

    private bool drag = false;
    private Vector2 difference;
    private Vector2 origin;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        //right-Click and drag
        if (Input.IsMouseButtonPressed(MouseButton.Right) /*&& !eventSystem.IsPointerOverGameObject()*/) {
            Vector2 mousePos = GetGlobalMousePosition();
            difference = mousePos - Position;
            if (drag == false) {
                drag = true;
                origin = mousePos;
            }
        } else {
            drag = false;
        }
        if (drag == true) {
            Position = origin - difference;
        }


        //WASD
        //if (Input.IsAnythingPressed()) {
        //    Vector2 camPos = new Vector2(Position.X, Position.Y);
        //    if (Input.IsKeyPressed(Key.A)) {
        //        Position = new Vector2(camPos.X - (1 * alterSpeed), camPos.Y);
        //    }
        //    if (Input.IsKeyPressed(Key.D)) {
        //        Position = new Vector2(camPos.X + (1 * alterSpeed), camPos.Y);
        //    }
        //    if (Input.IsKeyPressed(Key.W)) {
        //        Position = new Vector2(camPos.X, camPos.Y - (1 * alterSpeed));
        //    }
        //    if (Input.IsKeyPressed(Key.S)) {
        //        Position = new Vector2(camPos.X, camPos.Y + (1 * alterSpeed));
        //    }
        //}


        if (Position.X < -(map.widthScale)) {
            Position = new Vector2(-(map.widthScale), Position.Y);
        }
        if (Position.Y < -(map.heightScale)) {
            Position = new Vector2(Position.X, -(map.heightScale));
        }
        if (Position.X > (map.widthScale)) {
            Position = new Vector2(map.widthScale, Position.Y);
        }
        if (Position.Y > (map.heightScale)) {
            Position = new Vector2(Position.X, (map.heightScale));
        }
        //Zoom
        //If over a UI object abort the Physics Raycast
        //if (!eventSystem.IsPointerOverGameObject()) {
        float speedRatio = 75 / Zoom.X + 10;
        if (Input.IsActionJustPressed("scrollWheelDown") && Zoom.X > minZoom) {
            if(Zoom.X - speedRatio > minZoom)
                Zoom = new Vector2(Zoom.X - speedRatio, Zoom.Y - speedRatio);
            else
                Zoom = new Vector2(minZoom, minZoom);
        }  else if (Input.IsActionJustPressed("scrollWheelUp") && Zoom.X < maxZoom) {
            if (Zoom.Y + speedRatio < maxZoom)
                Zoom = new Vector2(Zoom.X + speedRatio, Zoom.Y + speedRatio);
            else
                Zoom = new Vector2(maxZoom, maxZoom);
        }
            
        //}
    }
}
