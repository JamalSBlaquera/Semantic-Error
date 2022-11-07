using UnityEngine.Audio;
using UnityEngine;

namespace Mal
{
    [System.Serializable]
    public class Sound : PropertyAttribute
    {
        [SerializeField] public AudioManager.Sounds names;

        [SerializeField] public AudioClip clip;
        [SerializeField] public float delayTimer;
        [Range(0f,1f)]
        [SerializeField] public float volume;
        [Range(.1f, 3f)]
        [SerializeField] public float pitch;
        [SerializeField] public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}

