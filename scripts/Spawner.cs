using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] private PackedScene enemy;
	private Node2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetNode<Node2D>("Player");
		spawnEnemy();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void spawnEnemy()
	{
		Enemy newEnemy = enemy.Instantiate<Enemy>();
		newEnemy.setPlayer(player);
		AddChild(newEnemy);
	}
}
