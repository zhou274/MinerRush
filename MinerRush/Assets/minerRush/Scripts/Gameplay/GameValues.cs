

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class GameValues : MonoBehaviour
    {
        //Values
        public GameValue speedPlayer;
        public float playerAngle = 45f;
        public float playerAtDistanceMax = 10f;

        public FromToInt setRockAfter;
        public int setRockAfterStart;
        public Counter setCoinAfter;

        public int worldWidth = 10;

        public float distanceToChangeValues = 10;

        //World borders
        [HideInInspector]
        public float worldBorderLeft;
        [HideInInspector]
        public float worldBorderRight;
        [HideInInspector]
        public float worldBorderTop;
        [HideInInspector]
        public float worldBorderBottom;

        #region Standart system methods

        private void Start()
        {
            speedPlayer.InitComponent();

            CalculateWorldBorders();
        }

        #endregion

        #region World Borders

        /// <summary>
        /// Calculation of world borders based on camera view.
        /// You can use this values in generator or some other methods where
        /// you need to check borders or limits of positions.
        /// </summary>
        public void CalculateWorldBorders()
        {
            Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));

            worldBorderLeft = -stageDimensions.x;
            worldBorderRight = stageDimensions.x;
            worldBorderTop = stageDimensions.y;
            worldBorderBottom = -stageDimensions.y;
        }

        public void ChangeGameValues()
        {
            speedPlayer.ChangeValue();
        }

        #endregion
    }
}