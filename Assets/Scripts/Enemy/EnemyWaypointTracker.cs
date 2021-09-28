using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyWaypointTracker : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] walkPoints;
    [Header("Movement Settings")] 
    public float turnSpeed = 5f;
    public float patrolTime = 10f;
    public float walkDistance = 8f;
    [Header("Attack Settings")] 
    public float attackDistance=1.4f;
    public float attackRate=1f;
    private Transform _playerTarget;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private float currentAttackTime;
    private Vector3 nextDestination;
    private int index;
    //hp
    private EnemyHealth _enemyHealth;
    private void Awake()
    {
        _playerTarget = GameObject.FindWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        index = Random.Range(0, walkPoints.Length);
        if (walkPoints.Length>0)
        {
            InvokeRepeating("Patrol",Random.Range(0,patrolTime),patrolTime);
        }
    }
    void Start()
    {
        _navMeshAgent.avoidancePriority = Random.Range(1, 51);
    }
    void Update()
    {
        if (_enemyHealth.currentHealth>0)
        {
            MoveAndAttack();
        }
        else
        {
            _animator.SetBool("Death",true);
            _navMeshAgent.enabled = false;
            if (!_animator.IsInTransition(0)&&_animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime>.95f)
            {
                Destroy(this.gameObject,1f);
            }
        }
    }

    private void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, _playerTarget.position);
        if (distance>walkDistance)
        {
            if (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.speed = 2f;
                _animator.SetBool("Walk",true);
                nextDestination = walkPoints[index].position;
                _navMeshAgent.SetDestination(nextDestination);
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.speed = 0;
                _animator.SetBool("Walk",false);
                nextDestination = walkPoints[index].position;
                _navMeshAgent.SetDestination(nextDestination);
            }
        }
        else
        {
            if (distance>attackDistance +.15f && _playerTarget.GetComponent<PlayerHealth>().currentHealth>0)
            {
                if (!_animator.IsInTransition(0) && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    _animator.ResetTrigger("Attack");
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.speed = 3f;
                    _animator.SetBool("Walk",true);
                    _navMeshAgent.SetDestination(_playerTarget.position);
                }
            }else if (distance<=attackDistance && _playerTarget.GetComponent<PlayerHealth>().currentHealth>0)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("Walk",false);
                _navMeshAgent.speed = 0f;
                Vector3 targetPosition = new Vector3(_playerTarget.position.x,transform.position.y,_playerTarget.position.z);
                transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition-transform.position),turnSpeed*Time.deltaTime);
                if (currentAttackTime >= attackRate)
                {
                    _animator.SetTrigger("Attack");
                    currentAttackTime = 0;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                }

            }
        }
    }
    void Patrol()
    {
        index = index == walkPoints.Length - 1 ? 0 : index + 1;
    }
}
