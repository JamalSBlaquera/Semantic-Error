using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mal
{
    public class Enemy : Character
    {
        private static Enemy instance;
        public static Enemy myInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Enemy>();
                }
                return instance;
            }
        }

        public bool IsPlayerNear { get => _isPlayerNear; set => _isPlayerNear = value; }
        public Vector3 PlayerLastPosition { get => _playerLastPosition; set => _playerLastPosition = value; }
        public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }

        public bool IsCaughtPlayer { get => _isCaughtPlayer; set => _isCaughtPlayer = value; }
        public NavMeshAgent Agent { get => agent; set => agent = value; }
        public float WaitTime { get => _WaitTime; set => _WaitTime = value; }
        public int CurrentWaypointIndex { get => _currentWaypointIndex; set => _currentWaypointIndex = value; }
        public bool IsPlayerInRange { get => _isPlayerInRange; set => _isPlayerInRange = value; }
        public float TimeToRotate { get => _TimeToRotate; set => _TimeToRotate = value; }
        public Transform[] PatrolPointLocation { get => patrolPointLocation; set => patrolPointLocation = value; }
        public float StartWaitTime { get => startWaitTime; set => startWaitTime = value; }
        public float StartTimeRotate { get => startTimeRotate; set => startTimeRotate = value; }
        public bool IsPatrolling { get => _isPatrolling; set => _isPatrolling = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public float ViewRadius { get => viewRadius; set => viewRadius = value; }        
       
        [Header("AI Setting")]
        [SerializeField]
        private float viewRadius;
        [SerializeField]
        private float angleView;
        [SerializeField]
        NavMeshAgent agent;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField]
        private float startWaitTime = 4;
        [SerializeField]
        private float startTimeRotate = 2;
        [SerializeField]
        LayerMask playerMask;
        [SerializeField]
        LayerMask obstacleMask;
        [SerializeField]
        Collider[] playerInRange;

        [SerializeField]
        private Transform[] patrolPointLocation;
        int _currentWaypointIndex;

        bool _isPlayerInRange;
        bool _isPatrolling;
        bool _isPlayerNear;
        bool _isCaughtPlayer;
        //timer     
        public float _WaitTime;
        public float _TimeToRotate;


        Vector3 _playerPosition;
        Vector3 _playerLastPosition = Vector3.zero;

        protected override void Awake()
        {
            _isPlayerInRange = false;
            _isPatrolling = true;
            _isPlayerNear = false;
            _isCaughtPlayer = false;
            _TimeToRotate = startTimeRotate;
            _WaitTime = 0;
            _currentWaypointIndex = 1;
            _playerPosition = Vector3.zero;
            base.Awake();
        }

        protected override void Update()
        {
            EnviromentView();
            base.Update();
        }
        void EnviromentView()
        {
            playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            for (int i = 0; i < playerInRange.Length; i++)
            {
                Transform player = playerInRange[i].transform;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                Vector3 toPlayer = (player.position - transform.position).normalized;

                float checkDistance = Vector3.Distance(transform.position, player.position);

                if (Vector3.Angle(transform.forward, toPlayer) < angleView / 2)
                {
                    if (!Physics.Raycast(transform.position, toPlayer, checkDistance, obstacleMask))
                    {
                        _isPlayerInRange = true;
                        _isPatrolling = false;
                        _isCaughtPlayer = false;
                    } else
                    {
                        _isPlayerInRange = false;
                    }
                } else
                {
                    _isPatrolling = false;
                    animationBlendMove(0);
                    _animator.SetFloat("Speed", _animationBlendMovement);
                }
                if (checkDistance > viewRadius)
                {
                    _isPlayerInRange = false;
                } 
                if (_isPlayerInRange)
                {
                    _playerPosition = player.transform.position;
                }
            }
        }
        public void Move(float speed)
        {
            agent.isStopped = false;
            agent.speed = speed;
        }
        public void Stop()
        {
            agent.isStopped = true;
            agent.speed = 0;
        }
    }
}

