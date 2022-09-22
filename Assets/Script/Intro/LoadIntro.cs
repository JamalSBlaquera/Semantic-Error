using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LoadIntro : MonoBehaviour
{
    Animator animator;
    PlayableDirector playableDirector;
    private InputManager inputManager;
    

    public void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void Update()
    {
        startload();
    }


    public void startload()
    {
        if(inputManager.start)
        {
            playableDirector.Play();
            animator.enabled = true;
        }
    }
    public void stopload()
    {
        
    }
}
