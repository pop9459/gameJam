using Godot;
using System;

public partial class Enemy : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Node2D player;
	public void setPlayer(Node2D player)
	{
		this.player = player;
	}
	public override void _EnterTree()
	{

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (player == null) return;
		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		GlobalPosition += direction * (float)delta * 100; // Adjust the speed as needed
	}
}
