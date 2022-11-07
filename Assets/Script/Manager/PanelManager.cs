using UnityEngine;
using System.Collections;

namespace Mal
{
    public class PanelManager : MonoBehaviour
    {
        private Animator animator;

        [SerializeField]
        private GameObject panelCamera;
        [SerializeField]
        private GameObject mainControl;
        [SerializeField]
        private bool playerCamera = true;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SwitchStateCM()
        {
            if (playerCamera)
            {
                mainControl.SetActive(false);
                StartCoroutine(PauseGame());
                panelCamera.SetActive(true);
                animator.Play("PanelCamera");
            }
            else
            {
                Time.timeScale = 1f;
                mainControl.SetActive(true);
                panelCamera.SetActive(false);
                animator.Play("PLayerCamera");
            }
            playerCamera = !playerCamera;
        }
        IEnumerator ReseumeGame()
        {
            yield return new WaitForSeconds(2f);
            Time.timeScale = 1f;
        }

        IEnumerator PauseGame()
        {
           yield return new WaitForSeconds(2f);
            Time.timeScale = 0f;
        }

    }
}

