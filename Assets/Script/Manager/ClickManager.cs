using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void onclick()
    {
        if (gameObject.activeSelf == false)
        {
            animator.enabled = true;
            gameObject.SetActive(true);

            GameObject[] Object = GameObject.FindGameObjectsWithTag("page");

            foreach (GameObject page in Object)
            {                
                if (gameObject.name != page.name)
                {
                    page.SetActive(false);
                } 
            }
        }
    }
    
}
