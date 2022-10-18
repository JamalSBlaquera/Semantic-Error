using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    public abstract class StateData : ScriptableObject
    {
        public abstract void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo); 
        public abstract void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
        public abstract void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo);

        internal void OnEnter(CharacterStateBase characterStateBase1, object characterStateBase2, Animator animator1, object animator2, AnimatorStateInfo animatorStateInfo, object stateInfo)
        {
            throw new NotImplementedException();
        }
    }

}

