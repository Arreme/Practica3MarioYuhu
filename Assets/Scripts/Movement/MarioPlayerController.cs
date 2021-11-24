using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class MarioPlayerController : MonoBehaviour
{
    [Header("Animator Atributes")]
    [Range(0,1)]
    [SerializeField] private float _animSpeed;
    private bool _grounded;

    private Animator _animator;
    private CharacterController _controller;

    [Header("Rotation")]
    private Vector3 _currRotation;
    private Vector3 _rSmoothV;
    [SerializeField] private float _rSmoothTime;

    [Header("Jump")]
    [SerializeField] private KeyCode _jumpKey;
    private float _vSpeed = 0;
    private float _startingGravity;
    [SerializeField] private float _height;
    [SerializeField] private float _timeToMaxHeight;
    private float _gravity;
    private float _jumpSpeed;
    [SerializeField] private float _jumpCooldown;
    private float _currentJumpTime;
    private int _nJump;

    [Header("Movement")]
    [SerializeField] private Camera _cam;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] float smoothMoveTime = 0.5f;
    private Vector3 _speed;
    private Vector3 smoothV;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _startingGravity = -2 * _height / (_timeToMaxHeight * _timeToMaxHeight);
        _gravity = _startingGravity;
        _jumpSpeed = 2 * _height / _timeToMaxHeight;
        _nJump = 0;
        _currentJumpTime = 0;
    }

    private void Update()
    {

        _animator.SetFloat("Speed", _animSpeed);
        _currentJumpTime -= Time.deltaTime;
        if (Input.GetKeyDown(_jumpKey) && _grounded)
        {
            Jump();
        }
        
    }

    void FixedUpdate()
    {
        _currRotation = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 inputDir = new Vector3(input.x, 0, input.y).normalized;
        Vector3 worldInputDir = _cam.transform.TransformDirection(inputDir);

        Vector3 _lookTargetDirection = new Vector3(worldInputDir.x, 0, worldInputDir.z).normalized;
        _currRotation = Vector3.SmoothDamp(_currRotation, _lookTargetDirection, ref _rSmoothV, _rSmoothTime);
        if (_currRotation != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_currRotation, Vector3.up);


        float currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? _runSpeed : _moveSpeed;
        Vector3 targetVelocity = worldInputDir * currentSpeed;
        _speed = Vector3.SmoothDamp(_speed, targetVelocity, ref smoothV, smoothMoveTime);

        _vSpeed += _gravity * Time.deltaTime;
        _animSpeed = new Vector2(_speed.x, _speed.z).magnitude;
        _speed = new Vector3(_speed.x, _vSpeed, _speed.z);
        var cf = _controller.Move(_speed * Time.deltaTime);
        bool onGround = (cf & CollisionFlags.Below) != 0;
        bool onContactCeiling = (cf & CollisionFlags.Above) != 0;
        if (onGround)
        {
            _grounded = true;
            _vSpeed = -1.0f;
        } 
        else
        {
            _grounded = false;
            if ((cf & CollisionFlags.Above) != 0 && _vSpeed > 0)
            {
                _vSpeed = 0;
            }
        }
        if (onContactCeiling && _vSpeed > 0.0f) _vSpeed = 0.0f;
        if (_vSpeed < 0.0f)
        {
            _gravity = _startingGravity * 2f;
        }

        _animator.SetBool("Grounded",_grounded);

    }

    private void Jump()
    {
        if (_currentJumpTime > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            _nJump++;
            if (_nJump == 1)
            {
                _vSpeed = _jumpSpeed * 1.05f;
                _gravity = _startingGravity;
                _animator.SetTrigger("SecondJump");
                _currentJumpTime = _jumpCooldown;
            } else
            {
                _vSpeed = _jumpSpeed * 1.15f;
                _gravity = _startingGravity;
                _animator.SetTrigger("TripleJump");
                _currentJumpTime = -1;
                _nJump = 0;
            }
        } else
        {
            _vSpeed = _jumpSpeed;
            _gravity = _startingGravity;
            _animator.SetTrigger("FirstJump");
            _currentJumpTime = _jumpCooldown;
        }
        
    }
}
