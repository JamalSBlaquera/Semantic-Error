using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/NPC/HandlerIdle")]
    public class HandlerIdle : StateData
    {
        Enemy enemy;
        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);
            Enemy enemy = characterStateBase.GetEnemy(animator);

            if (enemy.myTarget != null)
            {
                character.triggerSpeed = character.WalkSpeed;
            }
            else
            {
                character.triggerSpeed = 0;
            }

            float Speed = character.triggerSpeed;

            character.animationBlendMove(Speed);
            
            Vector3 current = character.transform.position;
            _characterController.Move(current.normalized * Time.deltaTime * Speed + new Vector3(0, character._verticalVelocity, 0) * Time.deltaTime);

            if (character._hasAnimator)
            {
                animator.SetFloat("Speed", character._animationBlendMovement);
            }
        }
    }
}
