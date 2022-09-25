using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal {
    public abstract class StateData : ScriptableObject
    {
        public float Duration;

        public abstract void UpdateAbility(CharacterStateBase characterStateBase, Animator animator);
    }

}

