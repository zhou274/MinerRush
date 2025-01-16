

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    [System.Serializable]
    public class Counter
    {
        //Values
        public int value;
        private int valueDefault;
        private bool isValueDefaultSet = false;

        #region Work with value

        /// <summary>
        /// Perfom all processed of one dereasing step. Restore if it's reach limit.
        /// </summary>
        /// <returns>It's reach limit.</returns>
        public bool PerfomAndCheck()
        {
            Decrease();
            bool isReady = IsReady();
            if (isReady)
                Restore();
            return isReady;
        }

        /// <summary>
        /// Decrease value.
        /// </summary>
        public void Decrease()
        {
            TryToSetDefault();
            value--;
        }

        /// <summary>
        /// Check is reach 0.
        /// </summary>
        /// <returns>Is reach limit</returns>
        public bool IsReady()
        {
            return value <= 0;
        }

        /// <summary>
        /// Restore value to default.
        /// </summary>
        public void Restore()
        {
            value = valueDefault;
        }

        #endregion

        #region State

        /// <summary>
        /// If it's possible set delault value.
        /// </summary>
        private void TryToSetDefault()
        {
            if (!isValueDefaultSet)
            {
                isValueDefaultSet = true;
                valueDefault = value;
            }
        }

        #endregion
    }
}