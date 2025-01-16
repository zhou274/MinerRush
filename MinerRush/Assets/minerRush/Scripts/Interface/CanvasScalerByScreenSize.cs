

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class CanvasScalerByScreenSize : MonoBehaviour
    {
        //Components
        private Camera mainCamera;
        private CanvasScaler canvasScaler;

        //Values
        public Vector2 resolution;

        [Header("Positions")]
        public bool isFixWidth = false;
        public float fixedWidth = 1125;
        public bool isFixHeight = true;
        public float fixedHeight = 2436;

        [Header("Percent")]
        public bool percentWidth = true;
        [Range(0, 200)]
        public float percentWidthValue = 100f;
        public bool percentHeight = true;
        [Range(0, 200)]
        public float percentHeightValue = 100f;

        #region Standart system methods

        void Awake()
        {
            //Get components
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvasScaler = GetComponent<CanvasScaler>();

            //Get values
            GetScreenSize();
            SetReferenceResolution();
        }

        private void Update()
        {
            GetScreenSize();
            SetReferenceResolution();
        }

        #endregion

        void GetScreenSize()
        {
            float width = mainCamera.pixelWidth;
            float height = mainCamera.pixelHeight;

            if (isFixWidth)
                width = fixedWidth;
            if (isFixHeight)
                height = fixedHeight;

            if (percentWidth)
                width = width / 100f * percentWidthValue;
            if (percentHeight)
                height = height / 100f * percentHeightValue;

            resolution = new Vector2(width, height);
        }

        void SetReferenceResolution()
        {
            canvasScaler.referenceResolution = resolution;
        }
    }
}