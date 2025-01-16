

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    [System.Serializable]
    public class GameValue
    {
        //Values
        public float value = 2f;
        public float max = 4f;
        [Range(0f, 100f)]
        public float changeByPercent = 2f;
        private bool isIncrease;
        private bool isActive = true;

        #region Work with values

        /// <summary>
        /// Call this for initialise some value setting. Call before use
        /// </summary>
        public void InitComponent()
        {
            //Check is increase type
            isIncrease = value < max;
        }

        /// <summary>
        /// Change value.
        /// Value will be automatically increase or decrease based by calculation of value and valueMax.
        /// </summary>
        public void ChangeValue()
        {
            if (isActive)
            {
                float changeBy = (value / 100) * changeByPercent;

                value += isIncrease ? changeBy : -changeBy;
                if (isIncrease ? value >= max : value <= max)
                    StopActivity();
            }
        }

        /// <summary>
        /// Stop activity. When you call this you lock all future increasing of values.
        /// </summary>
        private void StopActivity()
        {
            isActive = false;
            value = max;
        }

        #endregion
    }
}