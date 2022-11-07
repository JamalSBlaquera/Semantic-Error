using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Mal {
    public class Player : Character
    {
        private static Player Instance;
        public static Player myInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<Player>();
                }
                return Instance;
            }
        }
        
        public Vector2 InputMove;

        [Header("Input")]
        public bool InputSprint;
        public bool InputJump;

        [Header("Character Stats")]
        [SerializeField]
        private bool isEmpty;
        public PlayerStat _stamina;
        private float initStamina = 100;

        [HideInInspector]
        public CameraManager cameraManager;

        /* public PlayerStat ManaStat { get => manaStat; set => manaStat = value; }
         public PlayerStat XpStat { get => xpStat; set => xpStat = value; }*/

        /*[SerializeField] private PlayerStat manaStat;
        [SerializeField] private PlayerStat xpStat;*/

        protected override void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
            myTransform = GetComponent<Transform>();
            base.Awake();
            AudioManager.Initialize();
        }

        protected override void Start()
        {
            _stamina.Initialize(initStamina, initStamina);
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
            HalderStamina();
            cameraManager.HandleAllCameraMovement();
            base.Update();
        }
        private void LateUpdate()
        {
            
        }
        private void HalderStamina()
        {
            if (InputSprint && InputMove != Vector2.zero && !isEmpty)
            {
                triggerSpeed = SprintSpeed;
                if (!_isSprintJump && _isSprinting)
                {
                    _stamina.myCurrentValue -= 0.5f;
                }
                
                if (_stamina.myCurrentValue <= 40)
                {
                    _stamina.content.color = Color.red;
                    triggerSpeed = 4;
                }
                else
                {
                    _stamina.content.color = new Color(0f, 0.9920001f, 1f, 1f);
                }
                if (_stamina.myCurrentValue == 0)
                {
                    triggerSpeed = WalkSpeed;
                    isEmpty = true;
                }
                
            } else
            {
                if (!InputSprint)
                {
                    isEmpty = false;
                }
                if (_stamina.myCurrentValue != _stamina.myMaxValue)
                {
                    if (_stamina.myCurrentValue <= 40)
                        _stamina.content.color = Color.red;
                    else
                        _stamina.content.color = new Color(0f, 0.9920001f, 1f, 1f);
                }
                _stamina.myCurrentValue += 0.5f;
                
            }
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