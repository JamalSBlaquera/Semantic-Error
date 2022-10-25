using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/NPC/HandlerFollowState")]
    public class HandlerFollowState : StateData
    {
        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("SprintJump", false);
            animator.SetBool("FreeFall", false);
        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Enemy enemy = characterStateBase.GetEnemy(animator);

            if (!enemy.IsPatrolling)
            {
                ChasingPlayer(enemy, animator);
            }
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void ChasingPlayer(Enemy enemy, Animator animator)
        {
            enemy.IsPlayerNear = false;
            enemy.PlayerLastPosition = Vector3.zero;
            float distostop = Vector3.Distance(enemy.transform.position, enemy.PlayerPosition);
            float dis = Vector3.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (enemy.IsPlayerInRange && !enemy.IsCaughtPlayer && distostop >= enemy.Agent.stoppingDistance)
            {
                Debug.Log(false);
                enemy.triggerSpeed = enemy.WalkSpeed;
                enemy.Move(enemy.triggerSpeed);
                enemy.animationBlendMove(enemy.triggerSpeed);
                enemy.Agent.SetDestination(enemy.PlayerPosition);              
            } else
            {
                if (enemy.WaitTime <= 0 && !enemy.IsCaughtPlayer && dis >= enemy.ViewRadius)
                {
                    enemy.IsPatrolling = true;
                    enemy.IsPlayerNear = false;
                    enemy.triggerSpeed = enemy.WalkSpeed;
                    enemy.Move(enemy.triggerSpeed);
                    enemy.animationBlendMove(enemy.triggerSpeed);
                    enemy.Agent.SetDestination(enemy.PatrolPointLocation[enemy.CurrentWaypointIndex].position);
                    enemy.WaitTime = enemy.StartWaitTime;
                    enemy.TimeToRotate = enemy.StartTimeRotate;
                } else
                {
                    enemy.Stop();
                    enemy.animationBlendMove(enemy.Agent.speed);
                    enemy.WaitTime -= Time.deltaTime;
                }
            }
            if (distostop <= enemy.Agent.stoppingDistance)
            {
                Debug.Log(true);
                enemy.IsCaughtPlayer = true;
                enemy.IsPlayerInRange = false;
                enemy.Stop();
                enemy.animationBlendMove(enemy.Agent.speed);
            }
            if (enemy._hasAnimator)
            {
                animator.SetFloat("Speed", enemy._animationBlendMovement);
            }
        }        
    }
}


