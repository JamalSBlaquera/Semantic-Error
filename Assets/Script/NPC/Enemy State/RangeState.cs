using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    public class RangeState : MonoBehaviour
    {
        private static RangeState Instance;
        public static RangeState myInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<RangeState>();
                }
                return Instance;
            }
        }

        public bool IsEnter { get => isEnter; set => isEnter = value; }

        Enemy parent;
        public Vector2 Npc_move;
        public Vector3 rotateToPlayer;

        public float x;
        public float z;

        public bool isPlayer;
        public float RangeRadius;
        private bool Self;
        public bool Attack = false;

        [Header("Stopping Distance")]
        [SerializeField] private bool isEnter;
        [SerializeField] private float centerOffsite;
        [SerializeField] private float stoppingRadius;
        [SerializeField] private LayerMask layerMask;

        private void Awake()
        {
            parent = GetComponentInParent<Enemy>();
        }
        private void Update()
        {
            /*gameObject.transform.forward = (myTarget.transform.position - transform.position).normalized;*/
        }
        private void FixedUpdate()
        {
           /* HanlderTargetRotationCheck();*/
        }
      /*  private void HanlderTargetRotationCheck()
        {

            Npc_move = Vector2.zero;
            
            if (parent.myTarget != null)
            {
               *//* if (rotateToPlayer.x < 0.1f)
                {
                    Npc_move.x = -1;
                }
                else
                {
                    Npc_move.x = 1;
                }
                if (rotateToPlayer.z < 0.1f)
                {
                    Npc_move.y = -1;
                }
                else
                {
                    Npc_move.y = 1;
                }*//*
                RaycastHit hit;
                Vector3 rayPositionForward = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                rotateToPlayer = (parent.myTarget.transform.position - transform.position);
                Self = false;

                if (Physics.Raycast(rayPositionForward, rotateToPlayer, out hit))
                {
                    foreach (Collider c in parent.RagdollParts)
                    {
                        if (c.gameObject == hit.collider.gameObject)
                        {
                            Self = true;
                            break;
                        }
                    }
                }
            }
            Vector3 spherePosition = new Vector3(transform.position.x, centerOffsite, transform.position.z);
            if (Physics.CheckSphere(spherePosition, stoppingRadius, layerMask, QueryTriggerInteraction.Ignore))
            {
                isEnter = false;
                Attack = true;
            } else
            {
                Attack = false;
                isEnter = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isPlayer) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Vector3 rayPositionForward = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Gizmos.DrawRay(rayPositionForward, rotateToPlayer);


            Gizmos.DrawSphere(
                new Vector3(transform.position.x, centerOffsite, transform.position.z),
                stoppingRadius);
        }*/
        /*private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                parent.myTarget = collision.transform;
                isEnter = true;
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                parent.myTarget = null;
            }
        }*/
    }
}

