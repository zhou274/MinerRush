

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class LeaderboardController : MonoBehaviour
    {
        //Components
        private Transform content;
        private GameCenter gameCenter;

        //Values
        public bool nonMobileRemoveLeaderbord = true;
        public bool alwaysShowInEditor = true;

        private bool isGeneratedCells = false;
        private bool isRemovedLeaderboard = false;

        private CellLeaderboard[] cells;
        public GameObject cell;
        private int cellsCount;

        #region Standart system methods

        void Awake()
        {
            content = GameObject.Find("Canvas").transform.Find("TableScoreGlobal").Find("Scroll View").Find("Viewport").Find("Content");
            gameCenter = GameObject.Find("GameCenter").GetComponent<GameCenter>();

            bool tryToRemoveLeaderboard = true;
            #if UNITY_EDITOR
            if (alwaysShowInEditor)
            {
                tryToRemoveLeaderboard = false;
            }
            #endif
            if (tryToRemoveLeaderboard)
            {
                #if !(UNITY_IOS || UNITY_ANDROID)
                if (nonMobileRemoveLeaderbord)
                    isRemovedLeaderboard = true;
                #endif
            }
        }

        private void Update()
        {
            TryToGenerateCells();
        }

        #endregion

        #region Table view

        private void TryToGenerateCells()
        {
            if (isRemovedLeaderboard)
            {
                isGeneratedCells = true;

                GameObject tableScoreGlobal = GameObject.Find("Canvas").transform.Find("TableScoreGlobal").gameObject;
                tableScoreGlobal.SetActive(false);
            }
            else
            {
                if (!isGeneratedCells)
                {
                    if (gameCenter.dataIsReady)
                    {
                        isGeneratedCells = true;

                        GenerateCells();
                        AdjustContentSize();
                    }
                }
            }
        }

        private void GenerateCells()
        {
            cellsCount = gameCenter.data.Length;
            cells = new CellLeaderboard[cellsCount];

            float positionY = 0f;
            float positionYShift = 0f;

            for (int i = 0; i < cellsCount; i++)
            {
                GameObject newCell = GameObject.Instantiate(cell);
                newCell.transform.SetParent(content);

                if (positionY == 0f)
                {
                    positionY = -(newCell.GetComponent<CellLeaderboard>().height / 2);
                    positionYShift = -newCell.GetComponent<CellLeaderboard>().height;
                }
                newCell.GetComponent<RectTransform>().localPosition = new Vector3(0f, positionY, 0f);
                newCell.GetComponent<RectTransform>().localScale = Vector3.one;

                LeaderboardPlayer playerData = gameCenter.data[i];
                newCell.GetComponent<CellLeaderboard>().SetTextPosition(playerData.position.ToString(), playerData.name, playerData.score);

                if (i == cellsCount - 1)
                    newCell.GetComponent<CellLeaderboard>().HideSeparator();
                cells[i] = newCell.GetComponent<CellLeaderboard>();

                positionY += positionYShift;
            }
        }

        private void AdjustContentSize()
        {
            float cellHeight = cells[0].GetComponent<CellLeaderboard>().height;
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, cellHeight * cellsCount);
        }

        #endregion
    }
}