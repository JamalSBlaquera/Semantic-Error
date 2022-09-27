using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mal {
    public class Player : Character
    {
        private static Character Instace;
        [HideInInspector]
        public CameraManager cameraManager;

        [SerializeField] private PlayerStat ManaStat;

        [SerializeField] private PlayerStat XPstat;
        [SerializeField] private Text LevelText;
        public static Character myInstance
        {
            get
            {
                if (Instace == null)
                {
                    Instace = FindObjectOfType<Character>();
                }
                return Instace;
            }
        }

        public PlayerStat myXPstat { get => XPstat; set => XPstat = value; }
        public PlayerStat myManaStat { get => ManaStat; set => ManaStat = value; }

        /* public PlayerStat ManaStat { get => manaStat; set => manaStat = value; }
         public PlayerStat XpStat { get => xpStat; set => xpStat = value; }*/

        /*[SerializeField] private PlayerStat manaStat;
        [SerializeField] private PlayerStat xpStat;*/

        protected override void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
            myTransform = GetComponent<Transform>();
            base.Awake();
        }

        protected override void Start()
        {
            /*myHealth.Initialize(100, Mathf.Floor(20 * myLevel * Mathf.Pow(myLevel, 0.01f)));
            ManaStat.Initialize(20, Mathf.Floor(20 * myLevel * Mathf.Pow(myLevel, 0.01f)));
            XPstat.Initialize(0, Mathf.Floor(100 * myLevel * Mathf.Pow(myLevel, 0.5f)));*/
            base.Start();
        }
        protected override void Update()
        {
            InputJump = InputManager.myInstance.jump;
            InputMove = InputManager.myInstance.move;
            InputSprint = InputManager.myInstance.sprint;
            InputAttack = InputManager.myInstance.attack;
            PlayerMovement();
            PlayerAttack();
        }
        private void LateUpdate()
        {
            cameraManager.HandleAllCameraMovement();
        }
        private void PlayerMovement()
        {
            triggerSpeed = WalkSpeed;
            if (InputMove == Vector2.zero) triggerSpeed = 0.0f;
        } 
        private void PlayerAttack()
        {
            
        }
    }
}


/*public void GainXP(int xp)
        {
            XPstat.myCurrentValue += xp;

            if (XPstat.myCurrentValue == XPstat.myMaxValue)
            {
                myHealth.myCurrentValue += myHealth.myMaxValue;
                ManaStat.myCurrentValue += ManaStat.myMaxValue;
            }

            *//*CombatTextManager.MyInstance.CreateText(transform.position, xp.ToString(), CTtype.XP);*//*

            if (XPstat.myCurrentValue >= XPstat.myMaxValue)
            {
                StartCoroutine(Ding());
            }
        }*/
/*private IEnumerator Ding()
{
    while (!XPstat.IsFull)
    {
        yield return null;
    }

    myLevel++;
    LevelText.text = myLevel.ToString();
    XPstat.myMaxValue = 100 * myLevel * Mathf.Pow(myLevel, 0.5f);
    XPstat.myMaxValue = Mathf.Floor(XPstat.myMaxValue);
    XPstat.myCurrentValue = XPstat.myOverflow;
    XPstat.ResetContent();

    myHealth.myMaxValue = 100 * myLevel * Mathf.Pow(myLevel, 0.1f);
    myHealth.myMaxValue = Mathf.Floor(myHealth.myMaxValue);
    myHealth.myCurrentValue = myHealth.myMaxValue;

    ManaStat.myMaxValue = 20 * myLevel * Mathf.Pow(myLevel, 0.01f);
    ManaStat.myMaxValue = Mathf.Floor(ManaStat.myMaxValue);
    ManaStat.myCurrentValue = ManaStat.myMaxValue;




    if (XPstat.myCurrentValue >= XPstat.myMaxValue)
    {
        StartCoroutine(Ding());
    }
}*/
/*private void Update()
  {
      if (InputManager.add)
      {
          HealthStat.myCurrentValue += 10;
      }
      if (InputManager.sub)
      {
          HealthStat.myCurrentValue -= 10;
      }
  }*/