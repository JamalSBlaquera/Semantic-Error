using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/NPC/HandlerFollowState")]
    public class HandlerFollowState : StateData
    {
        private float currentHorizontalSpeed;
        private float _animationBlendMove;

        [Header("Steering")]
        public float stoppingDistance;

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("SprintJump", false);
            animator.SetBool("FreeFall", false);
        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);
            Enemy enemy = characterStateBase.GetEnemy(animator);

            HanlderMovement(character, enemy, _characterController, animator);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void HanlderMovement(Character character, Enemy enemy, CharacterController _characterController, Animator animator)
        {
            currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

            character.animationBlendMove(character.triggerSpeed);

            if (character.CompareTag("NPC"))
            {
                enemy.ChasingPlayer(_characterController, character.triggerSpeed);
                if (enemy.myTarget != null)
                {
                    _characterController.transform.forward = (Enemy.myInstance.myTarget.position - character.transform.position).normalized;
/*                    _characterController.transform.position = Vector3.MoveTowards(character.transform.position, Enemy.myInstance.myTarget.position, character.triggerSpeed * Time.deltaTime);
*/
                    float distance = Vector3.Distance(Enemy.myInstance.myTarget.position, character.transform.position);

                    if (distance <= stoppingDistance)
                    {
                        character.triggerSpeed = 0;
                        animator.SetBool("Attack", true);
                    }
                    else
                    {
                        character.triggerSpeed = 2;
                    }
                } else
                {
                    character.triggerSpeed = 0;
                    character.animationBlendMove(character.triggerSpeed);
                }
            }
            if (character._hasAnimator)
            {
                animator.SetFloat("Speed", character._animationBlendMovement);
            }
        }
    }
}


