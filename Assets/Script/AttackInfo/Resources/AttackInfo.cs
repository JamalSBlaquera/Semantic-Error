using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    public class AttackInfo : MonoBehaviour
    {
        public Character Attacker = null;
        public HandlerAttack attackAbility;
        public List<string> colliderNames = new List<string>();

        public bool mustCollide;
        public bool mustFaceAttater;
        public float lethalRange;
        public int maxHits;
        public int currentHits;
        public bool isRegistered;
        public bool isFinished;


        public void ResetInfo(HandlerAttack attack, Character attacker)
        {
            isRegistered = false;
            isFinished = false;
            attackAbility = attack;
            Attacker = attacker;
        }

        public void Register(HandlerAttack attack)
        {
            isRegistered = true;

            attackAbility = attack;
            colliderNames = attack.ColliderNames;
            mustCollide = attack.MustCollide;
            mustFaceAttater = attack.MustFaceAttater;
            lethalRange = attack.LethalRange;
            maxHits = attack.MaxHits;
            currentHits = 0;

        }
    }
}

