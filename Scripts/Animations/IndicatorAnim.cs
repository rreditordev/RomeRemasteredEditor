using Godot;
using System;

public partial class IndicatorAnim : Sprite2D
{
	AnimationPlayer anim;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("./AnimationPlayer");
		anim.Play("Blink");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
