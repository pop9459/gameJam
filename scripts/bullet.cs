using Godot;
using System;

public partial class bullet : Sprite2D
{
	private float speed = 200f;
	private Vector2 origin;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		origin = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = new Vector2(speed * (float)delta, 0);
		Position += velocity;
		if (Position.DistanceTo(origin) > 500)
		{
			QueueFree();
		}
	}
}
