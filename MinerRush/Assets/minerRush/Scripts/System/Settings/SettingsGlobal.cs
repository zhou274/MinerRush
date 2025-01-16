

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public static class SettingsGlobal
    {
        public enum DeviceType
        {
            iPad, iPhoneClassic, iPhoneModern
        }

        #region Getters

        public static DeviceType GetCurrentDeviceType()
        {
            return (DeviceType)CustomPlayerPrefs.GetInt("_deviceType");
        }

        #endregion
    }
}