using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State Move", menuName = "Mal/Ability Data/HandlerMovementNPC")]
    public class HandlerMovementNPC : StateData
    {
        private float currentHorizontalSpeed;
        public float SpeedLimit = 0f;
        Vector3 inputDeriction;
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

            HanlderMovement(character, _characterController, animator);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void HanlderMovement(Character character, CharacterController _characterController, Animator animator)
        {

            /*if (RangeState.myInstance.myTarget != null)
            {
                inputDeriction = (RangeState.myInstance.myTarget.transform.position - character.myTransform.position).normalized;
                Debug.Log(inputDeriction);
                character.myTransform.position = Vector3.MoveTowards(character.myTransform.position, RangeState.myInstance.myTarget.position, Speed * Time.deltaTime);
            }*/
        }
        private void HandlerRotation(Character character)
        {
            _targetRotation = Mathf.Atan2(inputDeriction.x, inputDeriction.z);
            float moveRotation = Mathf.SmoothDampAngle(character.myTransform.eulerAngles.y, _targetRotation, ref _rotationVelocity, character.RotationSmoothTime);
            character.myTransform.rotation = Quaternion.Euler(0, moveRotation, 0);
        }
    }
}

