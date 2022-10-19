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

            enemyPatroling(enemy, animator, _characterController);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void enemyPatroling(Enemy enemy, Animator animator, CharacterController _characterController)
        {
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

                float distance = Vector3.Distance(enemy.waypoints.position, enemy.transform.position);

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
            }
            
        }
        public void LookingPlayer(Vector3 player)
        {

        }
    }
}
