using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/NPC/HandlerEnemyPatrolling")]
    public class HandlerEnemyPatrolling : StateData
    {
        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);
            Enemy enemy = characterStateBase.GetEnemy(animator);

            if (enemy.IsPatrolling)
            {
                enemyPatroling(enemy, animator);
            }
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void enemyPatroling(Enemy enemy, Animator animator)
        {
            if (enemy.IsPlayerNear)
            {
                if (enemy.TimeToRotate <= 0)
                {
                    enemy.triggerSpeed = enemy.WalkSpeed;
                    enemy.Move(enemy.triggerSpeed);
                    LookingPlayer(enemy.PlayerLastPosition, enemy, animator);
                    
                } else
                {
                    
                    enemy.Stop();
                    enemy.animationBlendMove(enemy.Agent.speed);
                    if (enemy._hasAnimator)
                    {
                        animator.SetFloat("Speed", enemy._animationBlendMovement);
                    }
                    enemy.TimeToRotate -= Time.deltaTime;
                }
            } else
            {
                Transform waypoint = enemy.PatrolPointLocation[enemy.CurrentWaypointIndex];
                enemy.IsPlayerNear = false;
                enemy.PlayerLastPosition = Vector3.zero;
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(waypoint.position - enemy.transform.position), enemy.RotationSpeed * Time.deltaTime);
                enemy.Agent.SetDestination(waypoint.position);
                enemy.animationBlendMove(enemy.Agent.speed);
                if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
                {
                    if (enemy.WaitTime <= 0)
                    {
                        Debug.Log(true);
                        NextPoint(enemy);
                        enemy.triggerSpeed = enemy.WalkSpeed;
                        enemy.Move(enemy.triggerSpeed);
                        enemy.animationBlendMove(enemy.Agent.speed);
                        enemy.WaitTime = enemy.StartWaitTime;
                    } else
                    {
                        Debug.Log(false);
                        enemy.Stop();
                        enemy.animationBlendMove(enemy.Agent.speed);
                        enemy.WaitTime -= Time.deltaTime;
                    }
                }
                if (enemy._hasAnimator)
                {
                    animator.SetFloat("Speed", enemy._animationBlendMovement);
                }
            }
        }
        public void LookingPlayer(Vector3 player, Enemy enemy, Animator animator)
        {
            enemy.animationBlendMove(enemy.triggerSpeed);
            enemy.Agent.SetDestination(player);
            if (Vector3.Distance(enemy.transform.position, player) <= 0.3)
            {
                if (enemy.WaitTime <= 0)
                {
                    enemy.IsPlayerNear = false;
                    enemy.triggerSpeed = enemy.WalkSpeed;
                    enemy.Move(enemy.triggerSpeed);
                    enemy.Agent.SetDestination(enemy.PatrolPointLocation[enemy.CurrentWaypointIndex].position);
                    enemy.WaitTime = enemy.StartWaitTime;
                    enemy.TimeToRotate = enemy.StartTimeRotate;
                }
                else
                {
                    enemy.Stop();
                    enemy.animationBlendMove(enemy.Agent.speed);
                    enemy.WaitTime -= Time.deltaTime;
                }
            }
            if (enemy._hasAnimator)
            {
                animator.SetFloat("Speed", enemy._animationBlendMovement);
            }
        }
        public void NextPoint(Enemy enemy)
        {
            enemy.CurrentWaypointIndex = (enemy.CurrentWaypointIndex + 1) % enemy.PatrolPointLocation.Length;
            enemy.Agent.SetDestination(enemy.PatrolPointLocation[enemy.CurrentWaypointIndex].position);
        }
    }
}

/*
            float speed = enemy.WalkSpeed;
            if (enemy.IsPatrolling)
            {
                if (enemy.m_TimeToRotate <= 0)
                {
                    // rotate enemy
                    LookingPlayer(enemy.playerLastPosition);
                } else
                {
                    speed = 0;
                    enemy.m_TimeToRotate -= Time.deltaTime;
                }
            } else
            {
               
                _characterController.transform.forward = (enemy.waypoints.position - enemy.transform.position).normalized;
                Vector3 directionForward = enemy.transform.TransformDirection(Vector3.forward);
                _characterController.Move(directionForward * speed * Time.deltaTime);

                
                distance = Vector3.Distance(enemy.waypoints.position, enemy.transform.position);

                if (distance <= 2)
                {
                    Debug.Log(speed);

                    speed = 0;
                    enemy.animationBlendMove(speed);
                }

                if (enemy._hasAnimator)
                {
                    animator.SetFloat("Speed", enemy._animationBlendMovement);
                }
            }*/