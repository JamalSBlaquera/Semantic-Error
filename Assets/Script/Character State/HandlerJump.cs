using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    [CreateAssetMenu(fileName = "New State Jump", menuName = "Mal/Ability Data/HanlderJump")]
    public class HandlerJump : StateData
    {
        //TimeOut
        
        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);

            HanlderJumpAndSprintJump(character, animator);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private void HanlderJumpAndSprintJump(Character character, Animator animator)
        {
            if (character.isGrounded)
            {
                character._fallingTimeoutDelta = character.FallingTimeout;

                if (character._verticalVelocity < 0.0f) character._verticalVelocity = -2f;

                if (character.InputJump && character._jumpingTimeoutDelta <= 0.0f && !character._isSprinting)
                {
                    if (character._hasAnimator)
                    {
                        animator.SetBool("Jump", true);
                    }
                    character.InputSprint = false;
                }

                if (character.InputJump && character._jumpingTimeoutDelta <= 0.0f && character._isSprinting)
                {
                    if (character._hasAnimator)
                        character._isSprintJump = true;
                    animator.SetBool("SprintJump", true);
                    character._stamina.myCurrentValue += 0.5f;
                }
                else
                {
                    character._isSprintJump = false;
                }


                if (character._jumpingTimeoutDelta >= 0.0f) character._jumpingTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                character._jumpingTimeoutDelta = character.JumpingTimeout;

                if (character._fallingTimeoutDelta >= 0.0f)
                {
                    character._fallingTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    if (character._hasAnimator)
                    {
                        animator.SetBool("FreeFall", true);
                    }
                }
                character.InputJump = false;
            }
        }
    }
}