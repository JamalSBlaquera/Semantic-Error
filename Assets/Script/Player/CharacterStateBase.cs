using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Mal {
    public class CharacterStateBase : StateMachineBehaviour
    {
        public List<StateData> ListAbilityData = new List<StateData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData data in ListAbilityData)
            {
                data.OnEnter(this, animator, stateInfo, layerIndex);
            }
        }
        public void UpdateAll(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData data in ListAbilityData)
            {
                data.UpdateAbility(characterStateBase, animator, stateInfo, layerIndex);
            }
        }
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StateData data in ListAbilityData)
            {
                data.OnExit(this, animator, stateInfo);
            }
        }

        private CharacterController characterController;
        public CharacterController GetCharacterController( Animator animator)
        {
            if (characterController == null)
            {
                characterController = animator.GetComponentInParent<CharacterController>();
            }
            return characterController;
        }

        private Character character;
        public Character GetCharacter(Animator animator)
        {
            if (character == null)
            {
                character = animator.GetComponentInParent<Character>();
            }
            return character;
        }
        private Player player;
        public Player GetPlayer(Animator animator)
        {
            if (player == null)
            {
                player = animator.GetComponentInParent<Player>();
            }
            return player;
        }
        private Enemy enemy;
        public Enemy GetEnemy(Animator animator)
        {
            if (enemy == null)
            {
                enemy = animator.GetComponentInParent<Enemy>();
            }
            return enemy;
        }
    }
}
