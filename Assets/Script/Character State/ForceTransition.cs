using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State ForceTransition", menuName = "Mal/Ability Data/ForceTransition")]
    public class ForceTransition : StateData
    {
        [Range(0.01f, 1f)]
        public float TrasitionTiming;

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= TrasitionTiming)
            {
                animator.SetBool("ForceTransition", true);
            }
        }
       
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool("ForceTransition", false);
        }
    }
}

