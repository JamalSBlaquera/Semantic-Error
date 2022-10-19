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

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
            float speed = enemy.WalkSpeed;

            character.animationBlendMove(enemy.triggerSpeed);

            if (enemy.chasingPlayer)
            {
                ChasingPlayer(character, enemy, _characterController, speed, animator);
            }

            if (character._hasAnimator)
            {
                animator.SetFloat("Speed", character._animationBlendMovement);
            }
        }
        private void ChasingPlayer(Character character, Enemy enemy, CharacterController _characterController, float speed, Animator animator)
        {
            _characterController.transform.forward = (enemy.myTarget.position - enemy.transform.position).normalized;
            _characterController.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.myTarget.position, speed * Time.deltaTime);

            float distance = Vector3.Distance(Enemy.myInstance.myTarget.position, character.transform.position);

            if (distance <= stoppingDistance)
            {
                animator.SetBool("Attack", true);
            }
            else
            {
                speed = enemy.WalkSpeed;
                character.animationBlendMove(speed);
            }
            if (enemy.myTarget == null)
            {
                speed = 0;
                character.animationBlendMove(speed);
            }
        }        
    }
}


