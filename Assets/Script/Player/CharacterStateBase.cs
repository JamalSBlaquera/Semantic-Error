using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Mal {
    public class CharacterStateBase : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();

        public void UpdateAll(CharacterStateBase characterStateBase, Animator animator)
        {
            foreach(StateData data in ListAbilityData)
            {
                data.UpdateAbility(characterStateBase, animator);
            }
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator);
        }

        private CharacterController characterController;
        public CharacterController GetCharacterController(Animator animator)
        {
            if (characterController == null)
            {
                characterController = animator.GetComponentInParent<CharacterController>();
            }
            return characterController;
        }
    }
}
