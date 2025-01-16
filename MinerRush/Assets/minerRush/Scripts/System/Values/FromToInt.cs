

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    /// <summary>
    /// Class who store two values and can speed up some dev process.
    /// </summary>
    [System.Serializable]
    public class FromToInt
    {
        //Values
        public int from;
        public int to;

        #region Initialisators

        /// <summary>
        /// Initialise value
        /// </summary>
        /// <param name="newFrom">From value</param>
        /// <param name="newTo">To value</param>
        public FromToInt(int newFrom, int newTo)
        {
            from = newFrom;
            to = newTo;
        }

        /// <summary>
        /// Init value with zero
        /// </summary>
        public FromToInt()
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
        public int GetRandom()
        {
            return Random.Range(from, to);
        }

        #endregion
    }
}