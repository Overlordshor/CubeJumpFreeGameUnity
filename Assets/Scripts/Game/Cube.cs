using System;
using UnityEngine;

public class Cube : MonoBehaviour, ICubeEventComponent
{
    private Rigidbody _rigidbodyCube;
    public Game Game;

    private CollisionHandler _collision;

    public float ForceJump { get; private set; }

    [HideInInspector] public bool IsJumped { get; private set; } = false;
    [HideInInspector] public bool IsPlayerControl { get; private set; } = true;
    [HideInInspector] public bool IsGround { get; private set; } = true;

    private void Awake()
    {
        Game = FindObjectOfType<Game>();
    }

    private void Start()
    {
        _collision = GetComponent<CollisionHandler>();
        _rigidbodyCube = gameObject.GetComponent<Rigidbody>();

        SubscribeOnEvent();
    }

    public void Jump(float pushtime)
    {
        if (IsGround)
        {
            ForceJump = GetForces(pushtime);

            _rigidbodyCube.AddRelativeForce(transform.right * -ForceJump);
            _rigidbodyCube.AddRelativeForce(transform.up * ForceJump * 2.5f);

            IsGround = false;
            IsJumped = true;
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
        IsPlayerControl = false;
        _collision.OnJumped -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        IsJumped = true;
        IsGround = false;
        _collision.OnJumped -= Cube_OnJumped;
    }

    public void Cube_OnFellGround()
    {
        IsPlayerControl = false;
        IsGround = true;
        _collision.OnJumped -= Cube_OnFellGround;
    }

    public void SubscribeOnEvent()
    {
        _collision.OnJumped += Cube_OnJumped;
        _collision.OnJumped += Cube_OnFellGround;
        _collision.OnJumped += Cube_OnHitCube;
    }
}