

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class EndGameController : MonoBehaviour
    {
        //Components
        private ScenesController scenesController;

        //Objects
        private Image indicatorResult;

        private GameObject buttonNextLevel;

        //Values
        private bool isWin = true;

        public int id;

        public Sprite imageWin;
        public Sprite imageLose;

        #region Standart system methods

        void Start()
        {
            //Get components
            scenesController = GameObject.Find("ScenesController").GetComponent<ScenesController>();

            //Get objects
            indicatorResult = GameObject.Find("Canvas").transform.Find("IndicatorResult").GetComponent<Image>();

            buttonNextLevel = GameObject.Find("Canvas").transform.Find("ButtonsHolderTop").transform.Find("ButtonNextLevel").gameObject;

            //Prepare scene
            LoadResult();
            TryToUnlockLevel();

            PrepareInterface();
        }

        #endregion

        #region State

        private void LoadResult()
        {
            id = CustomPlayerPrefs.GetInt("_loadedId");

            isWin = CustomPlayerPrefs.GetBool("_isGameWin");
        }

        private void PrepareInterface()
        {
            indicatorResult.sprite = isWin ? imageWin : imageLose;
            if (!isWin)
            {
                buttonNextLevel.SetActive(false);
            }
        }

        private void TryToUnlockLevel()
        {
            if (isWin)
            {
                int availableLevels = CustomPlayerPrefs.GetInt("_availableLevels", 1);
                if (id == availableLevels)
                {
                    availableLevels++;

                    CustomPlayerPrefs.SetInt("_availableLevels", availableLevels);
                }
            }
        }

        public void LoadNextLevel()
        {
            id++;
            CustomPlayerPrefs.SetInt("_loadedId", id);

        }

        #endregion
    }
}