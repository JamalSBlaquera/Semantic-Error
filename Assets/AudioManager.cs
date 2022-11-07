using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Mal {
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private Sound[] sounds;

        public static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.delayTimer = s.delayTimer;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            FindObjectOfType<AudioManager>().Play("BgMusic");
        }
        public enum Sounds
        {
            Walk,
            Run,
            Sprint,
            Attack,
            Land,
            BgMusic
        }
             
        private static Dictionary<Sounds, float> soundTimerDistionary;

        public static void Initialize()
        {
            soundTimerDistionary = new Dictionary<Sounds, float>();
            soundTimerDistionary[Sounds.Walk] = 0f;
            soundTimerDistionary[Sounds.Run] = 0f;
        }

        public void Play (string name)
        {
            Sound s = Array.Find(sounds, sound => sound.names.ToString() == name);
            if (s == null)
            {
                Debug.Log("Sound: " + name + " not found!");
                return;
            }
            if (canPlaySound(s.names, s.delayTimer))
            {
                s.source.Play();
            }
        }
        private static bool canPlaySound(Sounds s, float delayTimer)
        {
            switch(s)
            {
                default:
                    return true;
                case Sounds.Walk:
                    if (soundTimerDistionary.ContainsKey(s))
                    {
                        float lastTimePlayed = soundTimerDistionary[s];
                        if (lastTimePlayed + delayTimer < Time.time)
                        {
                            soundTimerDistionary[s] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                case Sounds.Run:
                    if (soundTimerDistionary.ContainsKey(s))
                    {
                        float lastTimePlayed = soundTimerDistionary[s];
                        if (lastTimePlayed + delayTimer < Time.time)
                        {
                            soundTimerDistionary[s] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
            }
        }
    }
}

