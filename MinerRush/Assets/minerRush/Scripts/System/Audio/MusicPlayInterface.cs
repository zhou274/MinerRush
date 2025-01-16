

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class MusicPlayInterface : MonoBehaviour
    {
        #region State

        public void UpdateState()
        {
            MusicController.Instance.Play();
        }

        public void PlayGameMusic()
        {
            MusicController.Instance.PlayGameMusic();
        }

        public void PlayMenuMusic()
        {
            MusicController.Instance.PlayMenuMusic();
        }

        #endregion
    }
}