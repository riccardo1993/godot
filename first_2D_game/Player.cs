using Godot;
using System;

public class Player : Area2D
{
	/// <summary>
	/// How fast the player will move (pixels/sec).
	/// </summary>
	[Export]
	public int Speed = 400;

	/// <summary>
	/// Size of the game window.
	/// </summary>
	public Vector2 ScreenSize;

	///<inheritdoc/>
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	///<inheritdoc/>
	public override void _Process(float delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("move_right"))
		{
			velocity.x += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.x -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.y -= 1;
		}

		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}

		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		);

	}
}
