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

        public bool PlayerNearby { get => playerNearby; set => playerNearby = value; }

        public Transform myTarget;
        private bool isEnter;
        
        
        [Header("Enemy View")]
        public bool chasingPlayer;
        public float viewRadius;
        public float viewAngle = 90;
        public LayerMask playerMask;
        public LayerMask obstacleMask;
        public Collider[] playerInRange;
        [Header("Enemy Patrol")]
        public bool IsPatrolling;
        private bool playerNearby;
        public Transform waypoints;                  
        int currentWaypointIndex;

        public Vector3 playerLastPosition = Vector3.zero;    
        public Vector3 m_PlayerPosition;

        [Header("Timer")]
        public float startWaitTime = 4;
        public float timeToRotate = 2;
        public float m_WaitTime;
        public float m_TimeToRotate;

        protected override void Start()
        {
            IsPatrolling = true;
            currentWaypointIndex = 0;
            base.Start();
        }
        protected override void Update()
        {
            enemyEnviroView();
            base.Update();
		}

        private void enemyEnviroView()
        {
            playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            for (int i = 0; i < playerInRange.Length; i++)
            {
                Transform targetPlayer = playerInRange[i].transform;
                Vector3 DirToPlayer = (targetPlayer.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, DirToPlayer) < viewAngle / 2)
                {
                    myTarget = playerInRange[i].transform;
                    float disToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

                    if (!Physics.Raycast(transform.position, DirToPlayer, disToPlayer, obstacleMask))
                    {
                        // see the player on enemy range view
                        chasingPlayer = true;
                        IsPatrolling = false;
                    } else
                    {
                        // if the player is behind by the object and lose the player
                        chasingPlayer = false;
                    }
                } else
                {
                    myTarget = null;
                }
                if (Vector3.Distance(transform.position, targetPlayer.position) > viewRadius)
                {
                    chasingPlayer = false;
                }
                if (chasingPlayer)
                {
                    m_PlayerPosition = targetPlayer.transform.position;
                }
            }
        }
        
        private void SetDestination(Vector3 Waypoint) {
            transform.position = Vector3.MoveTowards(transform.position, Waypoint, 2 * Time.deltaTime);
        } 

        
        /*public void NextPoint()
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            SetDestination(waypoints[currentWaypointIndex].position);
        }*/
    }
}

