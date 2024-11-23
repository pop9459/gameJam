using Godot;
using System;

public partial class Player : Node2D
{
	private float angle = 0f; // Current angle in radians
	private float radius = 25f; // Distance from parent
	private int health = 3; // Player health
	private int maxHealth = 3; // Player max health
	private Node2D gun;
	private double shootDelay = 1f;
	private double nextShoot = 0f;
	private double totalElapsed = 0;
	[Export] private PackedScene bullet;
	private bool redEnabled = false;
	private bool greenEnabled = false;
	private bool blueEnabled = false;
	[Export] private Node2D redIndicator;
	[Export] private Node2D greenIndicator;
	[Export] private Node2D blueIndicator;
	[Export] private Node2D mixIndicator;
	[Export] private Color selectedColor = new Color(1, 1, 1);
	[Export] private Node2D[] hearts; // A container holding the heart icons
	public override void _Ready()
	{
		gun = GetNode<Node2D>("Gun");
		var area2D = GetNode<Area2D>("Hitbox");
		area2D.AreaEntered += OnAreaEntered;
		UpdateHeartsUI();
	}
	private void OnAreaEntered(Area2D area)
	{
		TakeDamage(1);
		area.GetParent().QueueFree();
		UpdateHeartsUI();
	}
	public override void _Process(double delta)
	{
		totalElapsed += delta;

		aim();
		selectColor();
		if (Input.IsActionJustPressed("shoot"))
		{
			shoot();
		}	
	}
	private void selectColor()
	{
		redEnabled = Input.IsActionPressed("select1");
		greenEnabled = Input.IsActionPressed("select2");
		blueEnabled = Input.IsActionPressed("select3");
		
		selectedColor.R = redEnabled ? 1 : 0;
		selectedColor.G = greenEnabled ? 1 : 0;
		selectedColor.B = blueEnabled ? 1 : 0;

		updateIndicators();
	}
	private void updateIndicators()
	{
		redIndicator.SelfModulate = redEnabled ? new Color(1, 0, 0) : new Color(1, 0.75f, 0.75f);
		greenIndicator.SelfModulate = greenEnabled ? new Color(0, 1, 0) : new Color(0.75f, 1, 0.75f);
		blueIndicator.SelfModulate = blueEnabled ? new Color(0, 0, 1) : new Color(0.75f, 0.75f, 1);
		mixIndicator.SelfModulate = selectedColor;
	}
	private void shoot()
	{
		if (totalElapsed > nextShoot)
		{
			nextShoot = totalElapsed + shootDelay;
			Node2D newBullet = bullet.Instantiate<Node2D>();
			newBullet.Position = gun.GlobalPosition;
			newBullet.Rotation = angle;
			newBullet.GetNode<Sprite2D>("BulletImg").SelfModulate = selectedColor;
			newBullet.Set("color", selectedColor);
			AddChild(newBullet);
		}
	}

	private void aim()
	{
		Vector2 mousePosition = GetGlobalMousePosition();

		// Calculate the direction to the mouse
		Vector2 directionToMouse = (mousePosition - GlobalPosition).Normalized();

		// Increment the angle over time
		angle = directionToMouse.Angle();	

		// Keep the angle within a valid range (optional)
		angle = Mathf.Wrap(angle, 0, 2 * Mathf.Pi);

		// Calculate new position
		Vector2 newPosition = new Vector2(
			Mathf.Cos(angle) * radius,
			Mathf.Sin(angle) * radius
		);

		// Apply the new position relative to the parent
		gun.Position = newPosition;
		gun.Rotation = angle;
		if (directionToMouse.X < 0)
		{
			gun.Scale = new Vector2(gun.Scale.X, -Mathf.Abs(gun.Scale.Y));
		}
		else
		{
			gun.Scale = new Vector2(gun.Scale.X, Mathf.Abs(gun.Scale.Y));
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		GD.Print($"Player Health: {health}");
		UpdateHeartsUI();

		if (health <= 0)
		{
			GD.Print("Player died!");
			Die();
		}
	}

	public void UpdateHeartsUI()
	{
		for (int i = 0; i < hearts.Length; i++)
		{
			if (i < health)
			{
				hearts[i].Visible = true;
			}
			else
			{
				hearts[i].Visible = false;
			}
		}
	}

	public void Die()
	{
		GetTree().ChangeSceneToFile("res://scenes/GameOver.tscn");
	}
}
