

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    [RequireComponent(typeof(AudioSource))]
    class MusicController : MonoBehaviour
    {
        //Components
        private static MusicController instance = null;
        public static MusicController Instance { get { return instance; } }

        //Values
        public AudioClip musicMenu;
        public AudioClip musicGame;

        #region Standart system methods

        void Awake()
        {
            //Return singlton objects
            if (MusicController.instance == null)
            {
                DontDestroyOnLoad(this);
                MusicController.instance = this;
                Play();
            }
            else
                Destroy(this.gameObject);
        }

        #endregion

        public void Play()
        {
            if (CustomPlayerPrefs.GetBool("music", true))
                GetComponent<AudioSource>().Play();
            else
                GetComponent<AudioSource>().Pause();
        }

        public void PlayGameMusic()
        {
            if (CustomPlayerPrefs.GetBool("music", true))
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = musicGame;
                GetComponent<AudioSource>().Play();
            }
            else
                GetComponent<AudioSource>().Pause();
        }

        public void PlayMenuMusic()
        {
            if (CustomPlayerPrefs.GetBool("music", true))
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = musicMenu;
                GetComponent<AudioSource>().Play();
            }
            else
                GetComponent<AudioSource>().Pause();
        }
    }
}