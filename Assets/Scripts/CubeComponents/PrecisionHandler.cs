using System;
using UnityEngine;

public class PrecisionHandler : MonoBehaviour, ICubeEventComponent
{
    private CollisionHandler _collision;
    private Color _colorDefault;
    private Rigidbody _rigidbodyCube;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _collision = GetComponent<CollisionHandler>();
        _rigidbodyCube = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();

        SubscribeOnEvents();
    }

    private void ResetMaterial()
    {
        _meshRenderer.material.color = _colorDefault;
    }

    public void Cube_OnCompressedCube()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnFellGround()
    {
        throw new NotImplementedException();
    }

    public void Cube_OnHitCube()
    {
        var cubeTarget = _collision.CubeTarget;
        _colorDefault = _meshRenderer.material.color;

        if (transform.position.x >= cubeTarget.transform.position.x - cubeTarget.transform.position.x * 20 / 100 &&
            transform.position.x <= cubeTarget.transform.position.x + cubeTarget.transform.position.x * 20 / 100)
        {
            _rigidbodyCube.freezeRotation = true;
            _meshRenderer.material.color = new Color(_colorDefault.r / 2, 1f, _colorDefault.b / 2, 0.1f); // green
        }
        else
        {
            _rigidbodyCube.freezeRotation = false;
            _meshRenderer.material.color = new Color(1f, _colorDefault.g / 2, _colorDefault.b / 2, 0.1f); // red
        }

        Invoke(nameof(ResetMaterial), 0.1f);

        _collision.OnHitCube -= Cube_OnHitCube;
    }

    public void Cube_OnJumped()
    {
        throw new NotImplementedException();
    }

    public void SubscribeOnEvents()
    {
        _collision.OnHitCube += Cube_OnHitCube;
    }
}