

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    [RequireComponent(typeof(AudioSource))]
    class SoundsController : MonoBehaviour
    {
        //Components
        private static SoundsController instance = null;
        public static SoundsController Instance { get { return instance; } }

        public GameObject soundPlayer;

        public AudioClip[] clipsToPlay;

        //Possible values
        [System.Serializable]
        public enum SoundName
        {
            ButtonClick = 0,
            Switch = 1,
            Coin = 2,
            Point = 3,
            Smash1 = 4,
            Smash2 = 5
        }

        #region Standart system methods

        void Awake()
        {
            //Singleton object return
            if (instance == null)
            {
                DontDestroyOnLoad(this);
                instance = this;
            }
            else
                Destroy(gameObject);
        }

        #endregion

        public void Play(SoundName soundName)
        {
            if (PlayerPrefs.GetInt("sound", 1) == 1)
            {
                GameObject newPlayer = Instantiate(soundPlayer);
                newPlayer.GetComponent<SoundPlayer>().LoadAndPlay(clipsToPlay[(int)soundName]);
            }
        }
    }
}