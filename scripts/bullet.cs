using Godot;
using System;

public partial class bullet : Node2D
{
	private float speed = 200f;
	private Vector2 origin;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		origin = Position;
		var area2D = GetNode<Area2D>("Area2D");
        area2D.AreaEntered += OnAreaEntered;
	}
	private void OnAreaEntered(Area2D area)
    {
		if(area.GetParent().Name == "Enemy")
		{
			if(area.GetParent().GetNode<Sprite2D>("Sprite2D").SelfModulate == GetNode<Sprite2D>("BulletImg").SelfModulate)
			{
				area.GetParent().QueueFree();
				QueueFree();
			}
		}
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();
		Position += direction * speed * (float)delta;	
		if (Position.DistanceTo(origin) > 500)
		{
			QueueFree();
		}
	}
}
