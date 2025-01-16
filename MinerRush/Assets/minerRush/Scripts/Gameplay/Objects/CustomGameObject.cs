

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class CustomGameObject : MonoBehaviour
    {
        //Components
        [HideInInspector]
        public GameValues gameValues;
        [HideInInspector]
        public GameController gameController;
        [HideInInspector]
        public SoundPlayingInterface soundPlayingInterface;

        //Values
        [HideInInspector]
        public float width;
        [HideInInspector]
        public float widthHalf;
        [HideInInspector]
        public float height;
        [HideInInspector]
        public float heightHalf;

        public enum BorderPoint
        {
            Top, Bottom, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight
        }

        #region Standart system methods

        public virtual void Start()
        {
            GetComponents();
            CalculateValues();
        }

        #endregion

        #region Components

        public void GetComponents()
        {
            soundPlayingInterface = GameObject.Find("SoundPlayingInterface").GetComponent<SoundPlayingInterface>();

            gameValues = GameObject.Find("GameValues").GetComponent<GameValues>();
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        #endregion

        #region Values

        public void CalculateValues()
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Vector3 size = renderer.bounds.size;

            width = size.x;
            height = size.y;
        }

        public Vector3 GetBorderPoint(BorderPoint borderPoint)
        {
            return GetBorderPoint(borderPoint, false);
        }

        public Vector3 GetBorderPointLocal(BorderPoint borderPoint)
        {
            return GetBorderPoint(borderPoint, true);
        }

        private Vector3 GetBorderPoint(BorderPoint borderPoint, bool isLocal)
        {
            Vector3 position = isLocal ? transform.localPosition : transform.position;

            switch (borderPoint)
            {
                case BorderPoint.Top:
                    return new Vector3(position.x, position.y + heightHalf, position.z);
                    break;
                case BorderPoint.Bottom:
                    return new Vector3(position.x, position.y - heightHalf, position.z);
                    break;
                case BorderPoint.Left:
                    return new Vector3(position.x - widthHalf, position.y, position.z);
                    break;
                case BorderPoint.Right:
                    return new Vector3(position.x + widthHalf, position.y, position.z);
                    break;
                case BorderPoint.TopLeft:
                    return new Vector3(position.x - widthHalf, position.y + heightHalf, position.z);
                    break;
                case BorderPoint.TopRight:
                    return new Vector3(position.x + widthHalf, position.y + heightHalf, position.z);
                    break;
                case BorderPoint.BottomLeft:
                    return new Vector3(position.x - widthHalf, position.y - heightHalf, position.z);
                    break;
                case BorderPoint.BottomRight:
                    return new Vector3(position.x + widthHalf, position.y - heightHalf, position.z);
                    break;
            }

            return position;
        }

        #endregion
    }
}