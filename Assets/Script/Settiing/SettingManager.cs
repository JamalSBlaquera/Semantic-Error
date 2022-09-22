using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public void onclick()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            GameObject[] Object = GameObject.FindGameObjectsWithTag("div");
            GameObject[] btn= GameObject.FindGameObjectsWithTag("divbtn");

            foreach (GameObject div in Object)
            {
                if (gameObject.name != div.name)
                {
                    div.SetActive(false);
                }                
            }
            foreach (GameObject divbtn in btn)
            {
                Image activeImg = divbtn.GetComponent<Image>();
                if (gameObject.name == divbtn.name)
                {
                    activeImg.enabled = true;
                }
                if (gameObject.name != divbtn.name)
                {
                    activeImg.enabled = false;
                }
            }
        }
    }
}
