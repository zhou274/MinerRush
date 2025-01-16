

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class SoundPlayingInterface : MonoBehaviour
    {
        //Objects
        private SoundsController soundsController;

        #region Standart system methods

        // Start is called before the first frame update
        void Start()
        {
            //Get objects
            soundsController = SoundsController.Instance;
        }

        #endregion

        #region Playing sounds

        public void PlaySoundButtonClick()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.ButtonClick);
        }

        public void PlaySoundSwitch()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.Switch);
        }

        public void PlaySoundCoin()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.Coin);
        }

        public void PlaySoundPoint()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.Point);
        }

        public void PlaySoundSmash1()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.Smash1);
        }

        public void PlaySoundSmash2()
        {
            if (soundsController != null)
                soundsController.Play(SoundsController.SoundName.Smash2);
        }

        #endregion
    }
}