using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mal
{
    public class FadeTransition : MonoBehaviour
    {
        public Animator animator;

        private int numToLoad;
        public void FadeToScene(int currentScen)
        {
            numToLoad = currentScen;
            animator.SetTrigger("FadeOut");
        }
        public void OnFadeComplate()
        {
            SceneManager.LoadScene(numToLoad);
        }
    }
}

