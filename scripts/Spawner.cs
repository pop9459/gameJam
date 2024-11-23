using Godot;
using System;

public partial class Spawner : Node2D
{
	private double spawnDelay = 1.5f;
	private double nextSpawn = 0f;
	private double totalElapsed = 0;
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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		totalElapsed += delta;
		if(totalElapsed > nextSpawn)
		{
			nextSpawn = totalElapsed + spawnDelay;
			spawnEnemy();
		}		
	}

	private void spawnEnemy()
	{
		Enemy newEnemy = enemy.Instantiate<Enemy>();
		newEnemy.setPlayer(player);
		
		//set random sprite
		int randomSpriteIndex = (int)GD.RandRange(0, sprites.Length-1);
		newEnemy.GetNode<Sprite2D>("Sprite2D").Texture = GD.Load<Texture2D>(sprites[randomSpriteIndex]);
		
		//set random color
		int randomColorInt = GD.RandRange(1, 7);
		//1 2 4
		int r = (randomColorInt & 1) != 0 ? 1 : 0;
		int g = (randomColorInt & 2) != 0 ? 1 : 0;
		int b = (randomColorInt & 4) != 0 ? 1 : 0;
		Color randomColor = new Color(r, g, b);
		newEnemy.GetNode<Sprite2D>("Sprite2D").SelfModulate = randomColor;
		AddChild(newEnemy);
	}
}
