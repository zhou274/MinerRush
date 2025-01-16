

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class ImageDeviceManagerUI : MonoBehaviour
    {
        //Components
        private Image image;

        //Values
        public Sprite spriteIPhoneClassic;
        public Sprite spriteIPhoneModern;
        public Sprite spriteIPad;

        #region Standart system methods

        void Start()
        {
            //Get components
            image = GetComponent<Image>();

            //Set values
            SetCorrentImage();
        }

        #endregion

        #region Work with object

        private void SetCorrentImage()
        {
            SettingsGlobal.DeviceType deviceType = SettingsGlobal.GetCurrentDeviceType();

            switch (deviceType)
            {
                case SettingsGlobal.DeviceType.iPhoneClassic:
                    image.sprite = spriteIPhoneClassic;
                    break;
                case SettingsGlobal.DeviceType.iPhoneModern:
                    image.sprite = spriteIPhoneModern;
                    break;
                case SettingsGlobal.DeviceType.iPad:
                    image.sprite = spriteIPad;
                    break;
            }
        }

        #endregion
    }
}