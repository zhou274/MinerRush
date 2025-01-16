

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class IndicatorController : MonoBehaviour
    {
        //Components
        private GameController gameController;

        private Text textScore;
        private TextMeshProUGUI textScoreResult;
        private TextMeshProUGUI textScoreResultBest;
        private TextMeshProUGUI textCoin;

        //Values
        private string defaultStringScore;
        private string defaultStringScoreResult;
        private string defaultStringScoreResultBest;
        private string defaultStringCoin;

        #region System default methods

        void Awake()
        {
            //Get components
            GameObject gameControllerGameObject = GameObject.Find("GameController");
            if (gameControllerGameObject != null)
                gameController = gameControllerGameObject.GetComponent<GameController>();

            GetIndicators();
        }

        private void Update()
        {
            UpdateIndicators();
        }

        #endregion

        #region Get components

        private void GetIndicators()
        {
            if (GameObject.Find("TextScore"))
            {
                textScore = GameObject.Find("TextScore").GetComponent<Text>();
                defaultStringScore = textScore.text;
            }
            if (GameObject.Find("TextScoreResult"))
            {
                textScoreResult = GameObject.Find("TextScoreResult").GetComponent<TextMeshProUGUI>();
                defaultStringScoreResult = textScoreResult.text;
            }
            if (GameObject.Find("TextScoreResultBest"))
            {
                textScoreResultBest = GameObject.Find("TextScoreResultBest").GetComponent<TextMeshProUGUI>();
                defaultStringScoreResultBest = textScoreResultBest.text;
            }
            if (GameObject.Find("TextCoin"))
            {
                textCoin = GameObject.Find("TextCoin").GetComponent<TextMeshProUGUI>();
                defaultStringCoin = textCoin.text;
            }
        }

        #endregion

        #region Update values

        private void UpdateIndicators()
        {
            if (textScore != null)
                UpdateTextScore();
            if (textScoreResult != null)
                UpdateTextResultScore();
            if (textScoreResultBest != null)
                UpdateTextResultBestScore();
            if (textCoin != null)
                UpdateTextCoin();
        }

        private void UpdateTextScore()
        {
            if (gameController != null)
            {
                textScore.text = defaultStringScore + gameController.score.ToString();
            }
        }

        private void UpdateTextResultScore()
        {
            int value = CustomPlayerPrefs.GetInt("scoreResult", 0);

            if (value == 0)
                textScoreResult.text = "";
            else
                textScoreResult.text = defaultStringScoreResult + value.ToString();
        }

        private void UpdateTextResultBestScore()
        {
            textScoreResultBest.text = defaultStringScoreResultBest + CustomPlayerPrefs.GetInt("highScore", 0).ToString();
        }

        private void UpdateTextCoin()
        {
            textCoin.text = defaultStringCoin + CustomPlayerPrefs.GetInt("coin", 0).ToString();
        }

        #endregion
    }
}