using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State", menuName = "Mal/Ability Data/HandlerMovement")]
    public class HandlerMovement : StateData
    {
        private static HandlerMovement Instance;
        public static HandlerMovement myInatance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<HandlerMovement>();
                }
                return Instance;
            }
        }

        private float currentHorizontalSpeed;
        public float SpeedLimit = 0f;

        //player 
        private float _animationBlendMove;
        private float _targetRotation;
        private float _rotationVelocity;

        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);

            HalderStamina();

            float Speed;
            if (SpeedLimit != 0f)
            {
                Speed = SpeedLimit;
            } else
            {
                Speed = Character.myInstance.triggerSpeed;
            }

            currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

            _animationBlendMove = Mathf.Lerp(_animationBlendMove, Speed, Time.deltaTime * Character.myInstance.SpeedChange);
            if (_animationBlendMove >= 2.1f)
            {
                Character.myInstance._isSprinting = true;
                Character.myInstance._isWalking = false;
            }
            else if (_animationBlendMove > 0.1f)
            {
                Character.myInstance._isWalking = true;
                Character.myInstance._isSprinting = false;
            }
            else
            {
                Character.myInstance._isWalking = false;
                Character.myInstance._isSprinting = false;

                _animationBlendMove = 0f;
            }

            Vector3 inputDeriction = new Vector3(Character.myInstance.InputMove.x, 0, Character.myInstance.InputMove.y).normalized;
            if (Character.myInstance.InputMove != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDeriction.x, inputDeriction.z) * Mathf.Rad2Deg + Character.myInstance._mainCamera.transform.eulerAngles.y;
                float moveRotation = Mathf.SmoothDampAngle(Character.myInstance.myTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, Character.myInstance.RotationSmoothTime);
                Character.myInstance.myTransform.rotation = Quaternion.Euler(0, moveRotation, 0);
            }

            Vector3 targetDerection = Quaternion.Euler(0, _targetRotation, 0) * Vector3.forward;
            _characterController.Move(targetDerection.normalized * Time.deltaTime * Speed +
                new Vector3(0, Character.myInstance._verticalVelocity, 0) * Time.deltaTime);

            if (Character.myInstance._hasAnimator)
            {
                animator.SetFloat("Speed", _animationBlendMove);
            }
        }
        private void HalderStamina()
        {
            Character n = Character.myInstance;
            if (n.InputSprint)
            {
                Character.myInstance.triggerSpeed = Character.myInstance.SprintSpeed;
                Character.myInstance._isSprinting = true;
                if (!Character.myInstance._isSprintJump)
                {
                    Character.myInstance._stamina.myCurrentValue -= 0.5f;
                }
                if (Character.myInstance._stamina.myCurrentValue == 0)
                {
                    Character.myInstance.triggerSpeed = Character.myInstance.WalkSpeed;
                    Character.myInstance.InputSprint = false;
                }
            }
            else
            {
                Character.myInstance._stamina.myCurrentValue += 0.5f;
            }
        }
    }
}

