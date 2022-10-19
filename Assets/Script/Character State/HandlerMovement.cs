using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/HandlerMovement")]
    public class HandlerMovement : StateData
    {
        private float currentHorizontalSpeed;
        Vector3 inputDeriction;
        Vector3 targetDerection;
        float moveRotation;
        //player 
        private float _animationBlendMove;
        private float _rotationVelocity;
        private float _targetRotation;

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
            Player player = characterStateBase.GetPlayer(animator);
            Enemy enemy = characterStateBase.GetEnemy(animator);

            HanlderMovement(character, player, enemy, _characterController, animator);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        private void HanlderMovement(Character character, Player player, Enemy enemy, CharacterController _characterController, Animator animator)
        {
            float Speed = character.triggerSpeed;
            
            currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

            _animationBlendMove = Mathf.Lerp(_animationBlendMove, Speed, Time.deltaTime * character.SpeedChange);
            if (_animationBlendMove >= 2.1f)
            {
                character._isSprinting = true;
                character._isWalking = false;
            }
            else if (_animationBlendMove > 0.1f)
            {
                character._isWalking = true;
                character._isSprinting = false;
            }
            else
            {
                character._isWalking = false;
                character._isSprinting = false;

                _animationBlendMove = 0f;
            }            

            inputDeriction = new Vector3(player.InputMove.x, 0, player.InputMove.y).normalized;
            if (character.CompareTag("Player"))
            {
                if (player.InputMove != Vector2.zero)
                {
                    HandlerRotation(character);
                    targetDerection = Quaternion.Euler(0, moveRotation, 0) * Vector3.forward;
                }
                _characterController.Move(targetDerection.normalized * Time.deltaTime * Speed +
                     new Vector3(0, character._verticalVelocity, 0) * Time.deltaTime);
            }

            if (character._hasAnimator)
            {
                animator.SetFloat("Speed", _animationBlendMove);
            }
        } 
        private void HandlerRotation(Character character)
        {
            _targetRotation = Mathf.Atan2(inputDeriction.x, inputDeriction.z) * Mathf.Rad2Deg + character._mainCamera.transform.eulerAngles.y;
            moveRotation = Mathf.SmoothDampAngle(character.myTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, character.RotationSmoothTime);
            character.myTransform.rotation = Quaternion.Euler(0, moveRotation, 0).normalized;
        }
    }
}

