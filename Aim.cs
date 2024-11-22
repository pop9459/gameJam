using Godot;
using System;

public partial class Aim : Node2D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private float angle = 0f; // Current angle in radians
	private float radius = 25f; // Distance from parent
	private float speed = 2f; // Speed of rotation
	private Node2D gun;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gun = GetNode<Node2D>("Gun");
	}

	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();

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
