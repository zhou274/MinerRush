
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    [System.Serializable]
    public class Skin
    {
        //Values
        private string idTitle = ""; //Use this title to save some info about skin

        public int id;
        public bool isAvailable;
        public int price;
        public Sprite sprite;

        #region Standart system methods

        void Awake()
        {
            LoadState();
        }

        #endregion

        #region Work with skin

        public void LoadState()
        {
            idTitle = "_skinId" + id.ToString() + "_";

            isAvailable = CustomPlayerPrefs.GetBool(idTitle + "isAvilable", id != 1 ? false : true);
        }

        /// <summary>
        /// Try to buy skin. And decrease coins value if is possible.
        /// </summary>
        /// <returns> Return buy is compleated</returns>
        public bool TryToBuy()
        {
            int coins = CustomPlayerPrefs.GetInt("coin");

            if (coins >= price)
            {
                coins -= price;

                CustomPlayerPrefs.SetInt("coin", coins);
                CustomPlayerPrefs.SetBool(idTitle + "isAvilable", true);
                LoadState();

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Select skin. Required to check skin available
        /// </summary>
        public void SelectThis()
        {
            CustomPlayerPrefs.SetInt("currentSkin", id);
        }

        /// <summary>
        /// Check current skin is selected now
        /// </summary>
        /// <returns>Is selected or not</returns>
        public bool IsSelected()
        {
            return CustomPlayerPrefs.GetInt("currentSkin") == id;
        }

        #endregion
    }
}