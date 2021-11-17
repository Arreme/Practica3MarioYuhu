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
    private bool _grounded;

    private Animator _animator;
    private CharacterController _controller;
    private float _vSpeed = 0;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _speedJump;

    [SerializeField] private Camera _cam;
    [SerializeField] private KeyCode _forwardKey;
    [SerializeField] private KeyCode _backKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        _animator.SetFloat("Speed", _speed);

        if (Input.GetKeyDown(_jumpKey) && _grounded)
        {
            Jump();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        Vector3 l_forward = _cam.transform.forward;
        l_forward.y = 0.0f;
        l_forward.Normalize();
        Vector3 l_right = _cam.transform.right;
        l_right.y = 0.0f;
        l_right.Normalize();

        if (Input.GetKey(_forwardKey))
        {
            movement += l_forward;
        }
        if (Input.GetKey(_backKey))
        {
            movement -= l_forward;
        }
        if (Input.GetKey(_rightKey))
        {
            movement -= l_right;
        }
        if (Input.GetKey(_leftKey))
        {
            movement += l_right;
        }
        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= Time.deltaTime * ((Input.GetKeyDown(KeyCode.LeftShift)) ? _runSpeed : _moveSpeed);
        }
        transform.rotation = Quaternion.LookRotation(movement,Vector3.up);
        _vSpeed += Physics.gravity.y * Time.deltaTime;
        movement.y += _vSpeed * Time.deltaTime;
        

        CollisionFlags cf = _controller.Move(movement);

        if ((cf & CollisionFlags.Below) != 0)
        {
            _grounded = true;
            _vSpeed = -1.0f;
        } else
        {
            _grounded = false;
            if ((cf & CollisionFlags.Above) != 0 && _vSpeed > 0)
            {
                _vSpeed = 0;
            }
            
        }
        _animator.SetBool("Grounded",_grounded);

    }

    private void Jump()
    {
        _vSpeed = _speedJump;
        _animator.SetTrigger("FirstJump");
    }
}
