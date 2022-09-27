using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mal
{
    [CreateAssetMenu(fileName = "New State HandlerAttack", menuName = "Mal/Ability Data/HandlerAttack")]
    public class HandlerAttack : StateData
    {
        [Header("Combo")]
        public bool debug;
        public float StartAttactTime;
        public float EndAttackTime;
        public List<string> ColliderNames = new List<string>();
        public bool MustCollide;
        public bool MustFaceAttater;
        public float LethalRange;
        public int MaxHits;

        public List<AttackInfo> finishedAttack = new List<AttackInfo>();

        public float ComboStartTime;
        public float ComboEndTime;
        public List<string> colliderNames { get => ColliderNames; set => ColliderNames = value; }

        public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
            AttackInfo info = obj.GetComponent<AttackInfo>();

            info.ResetInfo(this, characterStateBase.GetCharacter(animator));

            if (!AttackManager.Instance.CurrentAttacks.Contains(info))
                AttackManager.Instance.CurrentAttacks.Add(info);
        }
        public override void UpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            HandlerCheckCombo(characterStateBase, animator, stateInfo, layerIndex);
            RegisterAttack(characterStateBase, animator, stateInfo);
            DeregisterAttack(characterStateBase, animator, stateInfo);
            
        }

        
        private void RegisterAttack(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttactTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null) 
                        continue;

                    if (!info.isRegistered && info.attackAbility == this)
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " registered: " + stateInfo.normalizedTime);
                        }
                        info.Register(this);
                    }
                }
            }
        }
        
        private void DeregisterAttack(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
                {
                    if (info == null)
                        continue;

                    if (!info.isFinished && info.attackAbility == this)
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " deregistered: " + stateInfo.normalizedTime);
                        }
                        info.isFinished = true;
                        Destroy(info.gameObject );
                    }
                        
                }
            }
        }
        public void ClearAttack()
        {
            finishedAttack.Clear();

            foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
            {
                if (info == null || info.isFinished)
                {
                    finishedAttack.Add(info);
                }
            }
            foreach (AttackInfo info in finishedAttack)
            {
                if (AttackManager.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentAttacks.Remove(info);
                }
            }
        }

        private void HandlerCheckCombo(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Character character = characterStateBase.GetCharacter(animator);
            if (stateInfo.normalizedTime >= ComboStartTime)
            {
                if(stateInfo.normalizedTime <= ComboEndTime)
                {
                    if (character.InputAttack)
                    {
                        animator.SetBool("Attack", true);
                    }
                }
            }
        }
        public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
            animator.SetBool("Attack", false);
        }
    }
}

