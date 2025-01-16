

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class PauseController : MonoBehaviour
    {
        //Components
        private GameObject canvasPause;
        private GameController gameController;

        #region Standart system methods

        void Start()
        {
            //Get components
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            canvasPause = GameObject.Find("CanvasPause").gameObject;
            canvasPause.SetActive(false);
        }

        #endregion

        #region Pause logic

        public void PauseBegin()
        {
            PauseCanvasShow();
            Time.timeScale = 0;
            gameController.PauseOn();
        }

        public void PauseEnd()
        {
            PauseCanvasHide();
            Time.timeScale = 1;
            gameController.PauseOff();
        }

        public void SetTimeScaleDefault()
        {
            Time.timeScale = 1;
        }

        #endregion

        #region Interface

        public void PauseCanvasShow()
        {
            canvasPause.SetActive(true);
        }

        public void PauseCanvasHide()
        {
            canvasPause.SetActive(false);
        }

        #endregion
    }
}