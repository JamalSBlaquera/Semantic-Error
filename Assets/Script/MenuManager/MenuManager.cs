using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private InputManager inputManager;

    public void onclicks()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            GameObject[] menuObject = GameObject.FindGameObjectsWithTag("menuObject");

            foreach (GameObject obj in menuObject)
            {
                Debug.Log(true);
                if (gameObject.name != obj.name)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
