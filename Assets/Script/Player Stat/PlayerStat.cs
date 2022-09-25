using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mal
{
    public class PlayerStat : MonoBehaviour
    {
        private static PlayerStat instance;

        public static PlayerStat myInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlayerStat>();
                }
                return instance;
            }
        }

        private Image content;
        [SerializeField] private float lerpSpeed;
        /*    [SerializeField] private Text statValue;
        */
        private float currentFill;
        private float overflow;
        public float myMaxValue { get; set; }

        private float currentValue;

        public bool IsFull
        {
            get
            {
                return content.fillAmount == 1;
            }
        }
        public float myOverflow
        {
            get
            {
                float tmp = overflow;
                overflow = 0;
                return tmp;
            }
        }
        public float myCurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (value > myMaxValue)
                {
                    overflow = value - myMaxValue;
                    currentValue = myMaxValue;
                }
                else if (value < 0)
                {
                    currentValue = 0;
                }
                else
                {
                    currentValue = value;
                }
                currentFill = currentValue / myMaxValue;

                /* statValue.text = currentValue + "/" + myMaxValue;*/
            }
        }

        private void Start()
        {
            content = GetComponent<Image>();
        }
        private void Update()
        {
            if (currentFill != content.fillAmount)
            {
                content.fillAmount = Mathf.MoveTowards(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }

        public void Initialize(float currentValue, float maxValue)
        {
            myMaxValue = maxValue;
            myCurrentValue = currentValue;
        }
        public void ResetContent()
        {
            content.fillAmount = 0;
        }
    }
}

