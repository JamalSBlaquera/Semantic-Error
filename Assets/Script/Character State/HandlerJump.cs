using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    [CreateAssetMenu(fileName = "New State Jump", menuName = "Mal/Ability Data/HanlderJump")]
    public class HandlerJump : StateData
    {
        public float JumpStartTime;
        public float JumpEndTime;

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);
            Player player = characterStateBase.GetPlayer(animator); 
            HanlderJumpAndSprintJump(character, player, animator, stateInfo);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
        }
        private void HanlderJumpAndSprintJump(Character character, Player player, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (character.isGrounded)
            {
                character._fallingTimeoutDelta = character.FallingTimeout;

                if (character._verticalVelocity < 0.0f) character._verticalVelocity = -2f;

                if (player.InputJump && character._jumpingTimeoutDelta <= 0.0f && !character._isSprinting)
                {
                    animator.SetBool("Jump", true);
                    player.InputSprint = false;
                }
                if (player.InputJump && character._jumpingTimeoutDelta <= 0.0f && character._isSprinting)
                {
                    
                    character._isSprintJump = true;
                    animator.SetBool("SprintJump", true);
                    player._stamina.myCurrentValue += 0.5f;
                }
                else
                {
                    character._isSprintJump = false;
                }

                if (character._jumpingTimeoutDelta >= 0.0f) character._jumpingTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (player.InputJump)
                {
                    animator.SetBool("DoubleJump", true);
                } else {
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
                }
                player.InputJump = false;
            }
        }
    }
}