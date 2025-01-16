

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class SkinHolder : MonoBehaviour
    {
        //Values
        private static SkinHolder instance = null;
        public static SkinHolder Instance { get { return instance; } }

        public Skin[] skins;

        #region Standart system methods

        void Awake()
        {
            //Return singlton objects
            if (SkinHolder.instance == null)
            {
                DontDestroyOnLoad(this);
                SkinHolder.instance = this;
            }
            else
                Destroy(this.gameObject);
        }

        #endregion

        /// <summary>
        /// Call this for refresh skin states
        /// </summary>
        public void LoadStates()
        {
            for (int i = 0; i < skins.Length; i++)
                skins[i].LoadState();
        }
    }
}