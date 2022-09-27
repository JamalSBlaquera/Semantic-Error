using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State MoveForward", menuName = "Mal/Ability Data/HandlerMoveForward")]
    public class HandlerMoveForward : StateData
    {
        //player 
        private float _rotationVelocity;
        private float _targetRotation;

        public float addMoveForward;

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterController _characterController = characterStateBase.GetCharacterController(animator);
            Character character = characterStateBase.GetCharacter(animator);

            MoveForward(character, _characterController);
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        private void MoveForward(Character character, CharacterController characterController)
        {
            Vector3 directionForward = character.myTransform.TransformDirection(Vector3.forward);

            characterController.Move(directionForward * Time.deltaTime * addMoveForward);

        }
    }
}

