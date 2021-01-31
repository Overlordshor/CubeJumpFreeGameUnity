using System;
using UnityEngine;

public class Cube : MonoBehaviour, ICubeEventComponent
{
    private Rigidbody _rigidbodyCube;

    private CollisionHandler _collision;

    public float ForceJump { get; private set; }

    [HideInInspector] public bool IsJumped { get; private set; } = false;
    [HideInInspector] public bool IsGround { get; private set; } = true;

    private void Start()
    {
        _collision = GetComponent<CollisionHandler>();
        _rigidbodyCube = gameObject.GetComponent<Rigidbody>();

        SubscribeOnEvents();
    }

    public void Jump(float pushtime)
    {
        if (IsGround)
        {
            ForceJump = GetForces(pushtime);

            _rigidbodyCube.AddRelativeForce(transform.right * -ForceJump);
            _rigidbodyCube.AddRelativeForce(transform.up * ForceJump * 2.5f);
        }
    }

    public float GetForces(float pushTime)
    {
        if (pushTime < 0.07f)
        {
            pushTime = 0.07f;
        }
        float force;
        force = 370f * pushTime;
        if (force > 300f)
        {
            force = 300f;
        }

        return force;
    }

    public void Cube_OnCompressedCube()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnHitCube()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnJumped()
    {
        IsJumped = true;
        IsGround = false;
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void Cube_OnFellGround()
    {
    }

    public void SubscribeOnEvents()
    {
        _collision.OnJumped += Cube_OnJumped;
    }
}