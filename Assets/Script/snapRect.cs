using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mal
{
    public class snapRect : MonoBehaviour
    {
        InputManager inputManager;

        [SerializeField] public Transform content;
        public float[] position;

        public GameObject[] element;

        float distance;
        public Scrollbar scrollbar;
        float oldpos;

        // Start is called before the first frame update
        void Start()
        {

            inputManager = GetComponent<InputManager>();

            element = GameObject.FindGameObjectsWithTag("scollContent");

            distance = 1f / (content.childCount - 1);
            position = new float[content.childCount];

            for (int i = 0; i < content.childCount; i++)
            {
                position[i] = distance * i;

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (inputManager.mouseclick)
            {
                oldpos = scrollbar.value;
                for (int i = 0; i < content.childCount; i++)
                {

                    if (oldpos < position[i])
                    {

                        RectTransform child = element[i].transform.GetComponentInChildren<RectTransform>();
                        child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1920f);
                    }
                }

            }
            else
            {
                for (int i = 0; i < position.Length; i++)
                {
                    if (oldpos < position[i] + (distance / 2) && oldpos > position[i] - (distance / 2))
                    {

                        scrollbar.value = Mathf.Lerp(scrollbar.value, position[i], 0.3f);
                    }
                }
            }
        }
    }
}

