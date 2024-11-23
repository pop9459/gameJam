using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] private PackedScene enemy;
	private Node2D player;
	private string[] sprites = new string[] { "res://assets/monster.png", 
											"res://assets/monsterWater.png", 
											"res://assets/monsterBear.png", 
											"res://assets/monsterFire.png" };
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
		
		//set random sprite
		int randomSpriteIndex = (int)GD.RandRange(0, sprites.Length-1);
		newEnemy.GetNode<Sprite2D>("Sprite2D").Texture = GD.Load<Texture2D>(sprites[randomSpriteIndex]);
		//set random color
		Color randomColor = new Color((float)GD.RandRange(0, 1), (float)GD.RandRange(0, 1), (float)GD.RandRange(0, 1));
		newEnemy.GetNode<Sprite2D>("Sprite2D").SelfModulate = randomColor;
		AddChild(newEnemy);
	}
}
