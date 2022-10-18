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
		public Transform myTarget;
		private bool isEnter;

		[Header("Enemy View")]
		public float viewRadius;
		public float viewAngle = 90;
		public LayerMask playerMask;
		public LayerMask obstacleMask;
		public Collider[] playerInRange;


		protected override void Update()
        {
			obstacleAndPlayerDetector();
			base.Update();
		}
		private bool chasingPlayer;
		public void ChasingPlayer(CharacterController _characterController, float speed)
        {
			if (chasingPlayer)
            {
				_characterController.transform.forward = (Enemy.myInstance.myTarget.position - this.transform.position).normalized;
				_characterController.transform.position = Vector3.MoveTowards(this.transform.position, myTarget.position, speed * Time.deltaTime);
            }
        }

		private void obstacleAndPlayerDetector()
		{
            playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

			for (int i = 0; i < playerInRange.Length; i++)
			{
				Transform targetPlayer = playerInRange[i].transform;
				Vector3 DirToPlayer = (targetPlayer.position - this.transform.position).normalized;
				if (Vector3.Angle(transform.forward, DirToPlayer) < viewAngle / 2)
				{
					myTarget = playerInRange[i].transform;
					float disToPlayer = Vector3.Distance(this.transform.position, targetPlayer.position);

					if (!Physics.Raycast(this.transform.position, DirToPlayer, disToPlayer, obstacleMask))
					{
						chasingPlayer = true;

					} else
                    {
						chasingPlayer = false;
					}
				}
				if (Vector3.Distance(transform.position, targetPlayer.position) > viewRadius)
                {
					myTarget = null;
					chasingPlayer = false;
				}
			}
		}        
	}
}

