using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class Block : CustomGameObject
    {
        //Components
        private SpriteRenderer spriteRenderer;

        //Values
        public enum Type
        {
            Normal, Rock, Coin
        }
        [HideInInspector]
        public Type type;

        public Sprite[] sprites;
        public Sprite spriteRock;
        public Sprite spriteCoin;

        [Range(0f, 1.0f)]
        public float pbSizePercentage = 0.85f;

        public GameObject targetToRemove;
        public float removeWhenDistanceMoreThan = 10f;

        private bool isPossibleToDig = true;

        #region System default methods

        void Start()
        {
            base.Start();
        }

        public void InitSettingsWithType(Type newType)
        {
            type = newType;

            SetRandomSprite();
        }

        void Update()
        {
            TryToRemoveBlock();
        }

        #endregion

        #region Settings

        void SetRandomSprite()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            switch (type)
            {
                case Type.Coin:
                    spriteRenderer.sprite = spriteCoin;
                    break;
                case Type.Normal:
                    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
                    break;
                case Type.Rock:
                    spriteRenderer.sprite = spriteRock;
                    break;
            }
        }

        #endregion

        #region Cleaner

        void TryToRemoveBlock()
        {
            if (targetToRemove != null)
            {
                if (targetToRemove.transform.position.y + removeWhenDistanceMoreThan < transform.position.y)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Dig()
        {
            if (gameController.isPlay)
            {
                if (isPossibleToDig)
                {
                    isPossibleToDig = false;

                    if (type == Type.Coin)
                    {
                        gameController.IncreaseCoindBy(1);
                    }
                    else if (type == Type.Normal)
                    {
                        gameController.IncreaseScoreBy(1);
                    }

                    Destroy(gameObject);
                }
            }
        }

        #endregion
    }
}