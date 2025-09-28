using Godot;
using System;
using System.Diagnostics;

public partial class StarProjectile_Mini : RigidBody3D
{
    [Export] private CollisionChecker _collisionChecker;
    [Export] private int _damage;
    [Export] private AudioStream _starHitClip;
    [Export] private AudioStream _starHitEnemyClip;


    private float _initialVelocity = 10f;
    private float _acceleration = 5f;

    private Vector3 _targetDirection;

    public override void _Ready()
    {
        _collisionChecker.Collided += HitTarget;
    }

    public void Initialize()
    {
        _targetDirection = this.Transform.Basis.Z.Normalized();

        this.LinearVelocity = _targetDirection * _initialVelocity;
    }

    public override void _PhysicsProcess(double delta)
    {
        //move forwards locally
        this.ApplyCentralForce(_targetDirection * _acceleration * (float)delta);
    }

    private void HitTarget(Node3D body)
    {
        foreach(Node n in body.GetChildren())
        {
            Health hp = n as Health;
            if (hp != null)
            {
                hp.LoseHP(_damage);
                Debug.Print("Had HP!");
                AudioControllerS.instance.PlayClip(_starHitEnemyClip, 1, 0.3f);
            }
            else
            {
                AudioControllerS.instance.PlayClip(_starHitClip, 1, 0.3f);
            }
        }



        //currently destroys on hit but we can expand this to do damage by passing through a node and get componenting it for a health class or enemy class
        this.QueueFree();
    }
}
