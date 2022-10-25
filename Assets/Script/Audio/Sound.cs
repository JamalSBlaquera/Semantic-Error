using UnityEngine.Audio;
using UnityEngine;

namespace Mal
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] public string name; 
        [SerializeField] public AudioClip clip;
        
        [Range(0f,1f)]
        [SerializeField] public float volume;
        [Range(.1f, 3f)]
        [SerializeField] public float pitch;
        [SerializeField] public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}

