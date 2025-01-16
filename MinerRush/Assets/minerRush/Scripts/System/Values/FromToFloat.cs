

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    /// <summary>
    /// Class who store two values and can speed up some dev process.
    /// </summary>
    [System.Serializable]
    public class FromToFloat
    {
        //Values
        public float from;
        public float to;

        #region Initialisators

        /// <summary>
        /// Initialise value
        /// </summary>
        /// <param name="newFrom">From value</param>
        /// <param name="newTo">To value</param>
        public FromToFloat(float newFrom, float newTo)
        {
            from = newFrom;
            to = newTo;
        }

        /// <summary>
        /// Init value with zero
        /// </summary>
        public FromToFloat()
        {
            from = 0;
            to = 0;
        }

        #endregion

        #region Getters

        /// <summary>
        /// Get random values fron that bith values from and to
        /// </summary>
        /// <returns>Random value</returns>
        public float GetRandom()
        {
            return Random.Range(from, to);
        }

        #endregion
    }
}