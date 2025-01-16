

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class Generator : MonoBehaviour
    {
        //Components
        private GameController gameController;
        private GameValues gameValues;

        //Objects
        private GameObject player;

        //Values
        public GameObject block;
        public GameObject finishLine;

        public int generateRockAfterLines = 0;
        public int generateCoinAfterLines = 0;

        public GameObject generatorTarget;
        public float generatorDistance = 10f;
        public float generatorPositionY = 0f;

        public float blockSize;
        public float blockScaler = 1.15f;

        public int setRockAfter = 999;
        public float distanceToChangeValues = 0;

        #region Standart system methods

        void Start()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            gameValues = GameObject.Find("GameValues").GetComponent<GameValues>();

            player = GameObject.Find("Player");

            setRockAfter = gameValues.setRockAfterStart;
            distanceToChangeValues = player.transform.position.y - gameValues.distanceToChangeValues;

            CalculateSizes();

            SetFinishLine();
        }

        void FixedUpdate()
        {
            TryToGenerateLine();
            CheckPlayerReachDistanceToChangeValues();
        }

        #endregion

        #region Values

        private void CalculateSizes()
        {
            blockSize = (Mathf.Abs(gameValues.worldBorderLeft) + Mathf.Abs(gameValues.worldBorderRight)) / (float)gameValues.worldWidth;
        }

        private void CheckPlayerReachDistanceToChangeValues()
        {
            if (player.transform.position.y < distanceToChangeValues)
            {
                distanceToChangeValues -= gameValues.distanceToChangeValues;
                gameValues.ChangeGameValues();
            }
        }

        #endregion

        #region Generator logic

        private void TryToGenerateLine()
        {
            if (generatorTarget.transform.position.y - generatorDistance < generatorPositionY)
            {
                SetLine();

                generatorPositionY -= blockSize;
            }
        }

        #endregion

        #region Setters

        private void SetLine()
        {
            bool isRockLine = false;
            setRockAfter -= 1;
            if (setRockAfter <= 0)
            {
                setRockAfter = gameValues.setRockAfter.GetRandom();
                isRockLine = true;
            }
            bool isCoinLine = false;
            if (gameValues.setCoinAfter.PerfomAndCheck())
            {
                isCoinLine = true;
            }

            int indexRock = isRockLine ? GetRandomIndex(gameValues.worldWidth, false, -1) : -1;
            int indexCoin = isCoinLine ? GetRandomIndex(gameValues.worldWidth, true, indexRock) : -1;

            float positionX = gameValues.worldBorderLeft + blockSize / 2;
            for (int i = 0; i < gameValues.worldWidth; i++)
            {
                Block.Type newType = Block.Type.Normal;
                if (indexRock == i) newType = Block.Type.Rock;
                if (indexCoin == i) newType = Block.Type.Coin;

                SetBlock(newType, positionX);

                positionX += blockSize;
            }
        }

        int GetRandomIndex(int indexMax, bool withExeption, int exeptIndex)
        {
            int indexToReturn = Random.Range(0, indexMax);

            if (withExeption)
            {
                while (indexToReturn == exeptIndex)
                {
                    indexToReturn = Random.Range(0, indexMax);
                }
            }

            return indexToReturn;
        }

        private void SetBlock(Block.Type newType, float positionX)
        {
            //Crease block
            GameObject newBlock = GameObject.Instantiate(block);
            newBlock.transform.position = new Vector3(positionX, generatorPositionY, 0f);

            //Init block settings
            Block blockComponent = newBlock.GetComponent<Block>();
            blockComponent.InitSettingsWithType(newType);
            blockComponent.targetToRemove = player;

            //Sizes
            float rendererSize = blockSize * blockScaler;
            float pbSize = rendererSize * blockComponent.pbSizePercentage;
            newBlock.GetComponent<SpriteRenderer>().size = new Vector2(rendererSize, rendererSize);
            newBlock.GetComponent<BoxCollider2D>().size = new Vector2(pbSize, pbSize);
        }

        private void SetFinishLine()
        {
            //GameObject newFinishLine = GameObject.Instantiate(finishLine);
            //newFinishLine.transform.position = new Vector3(0f, -levelStats.lenght, 0f);
        }

        #endregion
    }
}