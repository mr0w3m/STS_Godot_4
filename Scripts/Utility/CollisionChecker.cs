using Godot;
using System;
using System.Diagnostics;

public partial class CollisionChecker : Area3D
{
	[Export] private int _targetLayer;


	public event Action<Node3D> Collided;
	public event Action<Node3D> ExitedCollider;


    private void OnCollided(Node3D body)
	{
		if (Collided != null)
		{
			Collided.Invoke(body);
		}
	}

	private void OnExitedCollider(Node3D body)
	{
		if (ExitedCollider != null)
		{
			ExitedCollider.Invoke(body);
		}
	}

	public override void _Ready()
	{
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}


	private void OnBodyEntered(Node3D body)
	{
		if (body != null)
		{
			//Debug.Print(body.Name + " Just entered the trigger zone");

			PhysicsBody3D pbody = (PhysicsBody3D)body;


            if (pbody!= null)
			{
				if ((pbody.CollisionLayer & _targetLayer) != 0) //bitwise check for layer? does it really work?
				{
					//Debug.Print("Collided with layer: " + _targetLayer);
					OnCollided(body);
                }
			}
        }
	}

	private void OnBodyExited(Node3D body)
	{
		if (body != null)
		{
            //Debug.Print(body.Name + " Just exited the trigger zone");

            PhysicsBody3D pbody = (PhysicsBody3D)body;


            if (pbody != null)
            {
                if ((pbody.CollisionLayer & _targetLayer) != 0) //bitwise check for layer? does it really work?
                {
                    OnExitedCollider(body);
                }
            }
        }
	}
}
