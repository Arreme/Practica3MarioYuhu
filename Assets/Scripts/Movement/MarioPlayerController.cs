using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class MarioPlayerController : MonoBehaviour
{
    [Header("Animator Atributes")]
    [Range(0,1)]
    [SerializeField] private float _speed;
    [SerializeField] private bool _grounded;

    private Animator _animator;
    private CharacterController _controller;
    private float _vSpeed = 0;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _speed);
        _animator.SetBool("Grounded", _grounded);

        Vector3 movement = Vector3.zero;

        _vSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y += _vSpeed * Time.deltaTime;

        CollisionFlags cf = _controller.Move(movement);

        if ((cf & CollisionFlags.Below) != 0)
        {
            _grounded = true;
            _vSpeed = 0;
        } else
        {
            _grounded = false;
        }

        _animator.SetBool("Grounded",_grounded);

    }
}
