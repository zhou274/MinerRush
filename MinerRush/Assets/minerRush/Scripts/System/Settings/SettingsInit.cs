

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class SettingsInit : MonoBehaviour
    {
        //Values
        public int targetFrameRate = 300;
        public bool cleanAllValues = false;

        #region Standart system methods

        void Awake()
        {
            ClearAll();
            RecogniseDeviceType();
            SetTargetFrameRate();
        }

        #endregion

        #region Settings initialise

        private void RecogniseDeviceType()
        {
            float width = Screen.safeArea.width;
            float height = Screen.safeArea.height;
            float proportion = height > width ? height / width : width / height;

            if (proportion <= 1.5)
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPad);
            else if (proportion <= 1.8)
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPhoneClassic);
            else
                CustomPlayerPrefs.SetInt("_deviceType", (int)SettingsGlobal.DeviceType.iPhoneModern);

            CustomPlayerPrefs.Save();
        }

        private void SetTargetFrameRate()
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.vSyncCount = 1;
        }

        private void ClearAll()
        {
            if (cleanAllValues)
                PlayerPrefs.DeleteAll();
        }

        #endregion
    }
}