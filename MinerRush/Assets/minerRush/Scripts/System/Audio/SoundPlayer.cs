

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class SoundPlayer : MonoBehaviour
    {
        //Components
        private AudioSource audioSource;

        //Values
        private double timeToRemove = 999.9f;
        private bool tryToRemove = false;

        #region Standart system methods

        private void Awake()
        {
            DontDestroyOnLoad(this);

            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (tryToRemove)
                TryToRemove();
        }

        #endregion

        public void LoadAndPlay(AudioClip newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();

            timeToRemove = newClip.length;
            tryToRemove = true;
        }

        public void TryToRemove()
        {
            timeToRemove -= Time.deltaTime;
            if (timeToRemove < 0)
                Destroy(gameObject);
        }
    }
}