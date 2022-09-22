using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] protected float waitTime;
    [SerializeField] public int index;

    void Start()
    {
        StartCoroutine(LoadScenes(index));
    }


    IEnumerator LoadScenes(int level)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(level);
    }  
}
