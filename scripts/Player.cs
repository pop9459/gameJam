using Godot;
using System;

public partial class Player : Node2D
{
	public float angle = 0f; // Current angle in radians
	private float radius = 25f; // Distance from parent
	private Node2D gun;
	public override void _Ready()
	{
		gun = GetNode<Node2D>("Gun");
	}

	public override void _Process(double delta)
	{
		aim();
		
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
	}
}
