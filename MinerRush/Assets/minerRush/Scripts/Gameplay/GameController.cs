

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class GameController : MonoBehaviour
    {
        //Components
        private ScenesController scenesController;
        private SoundPlayingInterface soundPlayingInterface;
        private MusicPlayInterface musicPlayInterface;
        private GameValues gameValues;

        private Text textStepWait;

        //Values
        [Header("Game values")]
        public bool isPlay = false;
        private bool isWaitToStart = true;
        private bool isWaitOnPause = false;

        public float timeAnimationWait = 0.25f;
        public float timeToStart = 0.65f;
        private float timeWait;
        private int showNumber = 4;
        private float timeToChangeWaitNumber;

        public int score = 0;

        [Header("Audio")]
        public bool increaseScoreSound = false;
        public bool increaseCoinsSound = true;

        #region Standart system methods

        void Start()
        {
            //Get components
            scenesController = GameObject.Find("ScenesController").GetComponent<ScenesController>();
            soundPlayingInterface = GameObject.Find("SoundPlayingInterface").GetComponent<SoundPlayingInterface>();
            musicPlayInterface = GameObject.Find("MusicPlayInterface").GetComponent<MusicPlayInterface>();
            gameValues = GameObject.Find("GameValues").GetComponent<GameValues>();

            textStepWait = GameObject.Find("Canvas").transform.Find("TextStepWait").GetComponent<Text>();

            //Accept some settings
            timeWait = timeAnimationWait + timeToStart;
            timeToChangeWaitNumber = timeAnimationWait;

            musicPlayInterface.PlayGameMusic();
        }

        private void Update()
        {
            TryToStartGame();
            TryToChangeTextWait();
        }

        #endregion

        #region Game process

        private void TryToStartGame()
        {
            if (isWaitToStart)
            {
                timeWait -= Time.deltaTime;

                if (timeWait < 0)
                    GameStart();
            }
        }

        private void GameStart()
        {
            isPlay = true;
            isWaitToStart = false;
            textStepWait.gameObject.SetActive(false);
        }

        public void GameOver()
        {
            isPlay = false;
            SaveScores();

            scenesController.ChangeSceneWithDelay("GameOverScene");
            musicPlayInterface.PlayMenuMusic();
        }

        #endregion

        #region Values

        /// <summary>
        /// Call when game is over. To save game process
        /// </summary>
        public void SaveScores()
        {
            int highScore = CustomPlayerPrefs.GetInt("highScore", 0);
            CustomPlayerPrefs.SetInt("scoreResult", score);
            if (score > highScore) CustomPlayerPrefs.SetInt("highScore", score);
            CustomPlayerPrefs.SetInt("score", score);
            CustomPlayerPrefs.Save();
        }

        public void IncreaseScoreBy(int value)
        {
            score += value;

            if (increaseScoreSound) soundPlayingInterface.PlaySoundPoint();
        }

        public void IncreaseCoindBy(int value)
        {
            int coins = CustomPlayerPrefs.GetInt("coin");
            coins += value;

            CustomPlayerPrefs.SetInt("coin", coins);

            if (increaseCoinsSound) soundPlayingInterface.PlaySoundCoin();
        }

        #endregion

        #region Interface

        /// <summary>
        /// Pre-game coultdown indicator
        /// </summary>
        private void TryToChangeTextWait()
        {
            if (isWaitToStart)
            {
                timeToChangeWaitNumber -= Time.deltaTime;

                if (timeToChangeWaitNumber < 0)
                {
                    timeToChangeWaitNumber = timeToStart / 3f;
                    showNumber--;

                    textStepWait.text = showNumber.ToString();
                }
            }
        }

        #endregion

        #region Pause

        public void PauseOn()
        {
            if (isPlay)
            {
                isPlay = false;
                isWaitOnPause = true;
            }
        }

        public void PauseOff()
        {
            if (isWaitOnPause)
            {
                isPlay = true;
                isWaitOnPause = false;
            }
        }

        #endregion
    }
}