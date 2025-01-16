

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class DestroyAfterTime : MonoBehaviour
    {
        //Values
        public bool isOn = true;
        public float time = 0.4f;

        #region Standart system methods

        void Update()
        {
            ChangeTimer();
        }

        #endregion

        #region Work with timer

        /// <summary>
        /// Change timer for destroy current gameObject
        /// </summary>
        private void ChangeTimer()
        {
            time -= Time.deltaTime;

            if (time <= 0f)
                Destroy(gameObject);
        }

        #endregion
    }
}