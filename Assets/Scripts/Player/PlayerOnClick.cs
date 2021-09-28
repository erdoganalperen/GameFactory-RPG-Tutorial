using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerOnClick : MonoBehaviour
{
    public float maxSpeed;
    public float turnSpeed;
    public float attackRange = 2f;
    private Animator _animator;
    private CharacterController _characterController;
    private CollisionFlags _collisionFlags = CollisionFlags.None;

    private Vector3 _playerMove = Vector3.zero;
    private Vector3 _targetMovePoint;
    private Vector3 _targetAttackPoint;
    private float _currentSpeed;
    private float _playerToPointDistance;
    private float _gravity = 9.8f;
    private float height;

    private bool _canMove;
    private bool _canAttackMove;
    private bool _finishedMovement = true;
    private Vector3 NewMovePoint;
    private Vector3 NewAttackPoint;

    private GameObject enemy;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _currentSpeed = maxSpeed;
    }

    private void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();
        AttackMove();
    }

    private bool IsGrounded()
    {
        return _collisionFlags == CollisionFlags.Below;
    }

    private void AttackMove()
    {
        if (_canAttackMove)
        {
            _targetAttackPoint = enemy.gameObject.transform.position;
            NewAttackPoint = new Vector3(_targetAttackPoint.x, transform.position.y, _targetAttackPoint.z);
        }

        if (!_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(NewAttackPoint - transform.position),turnSpeed * 2*Time.deltaTime); 
        }
    }
    private void CalculateHeight()
    {
        if (IsGrounded())
        {
            height = 0;
        }
        else
        {
            height -= _gravity * Time.deltaTime;
        }
    }

    private void CheckIfFinishedMovement()
    {
        if (!_finishedMovement)
        {
            if (!_animator.IsInTransition(0) && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                _finishedMovement = true;
            }
        }
        else
        {
            MovePlayer();
            _playerMove.y = height * Time.deltaTime;
            _collisionFlags = _characterController.Move(_playerMove);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (_playerToPointDistance >= 1.0f)
                    {
                        _canMove = true;
                        _canAttackMove = false;
                        _targetMovePoint = hit.point;
                    }
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    enemy = hit.collider.gameObject.GetComponentInParent<EnemyWaypointTracker>().gameObject;
                    _canMove = true;
                    _canAttackMove = true;
                }
            }
        }

        if (_canMove)
        {
            _animator.SetFloat("Speed", 1);
            if (!_canAttackMove)
            {
                NewMovePoint = new Vector3(_targetMovePoint.x, transform.position.y, _targetMovePoint.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(NewMovePoint - transform.position),turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(NewAttackPoint - transform.position),turnSpeed * Time.deltaTime); 
            }
      
            _playerMove = transform.forward * (_currentSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, NewMovePoint) <= 0.6f && !_canAttackMove)
            {
                _canMove = false;
                _canAttackMove = false; 
            }else if (_canAttackMove)
            {
                if (Vector3.Distance(transform.position,NewAttackPoint)<=attackRange)
                {
                    _playerMove.Set(0f, 0f, 0f);
                    _animator.SetFloat("Speed", 0f);
                    _targetAttackPoint = Vector3.zero;
                    _animator.SetTrigger("AttackMove");
                    _canAttackMove = false;
                    _canMove = false;
                }
            }
        }
        else
        {
            _playerMove.Set(0f, 0f, 0f);
            _animator.SetFloat("Speed", 0f);
        }
    }

    public bool FinishedMovement
    {
        get { return _finishedMovement; }
        set { _finishedMovement = value; }
    }

    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

    public Vector3 TargetPosition
    {
        get { return _targetMovePoint; }
        set { _targetMovePoint = value; }
    }
}