

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class AnimationController : MonoBehaviour
    {
        //Objects
        public GameObject animationSceneOn;
        public GameObject animationSceneOff;

        //Values
        public bool animateSceneOn = true;
        public bool animateSceneOff = true;

        public bool isSetToAnimationHolder = false;

        #region Standart system methods

        void Start()
        {
            if (animateSceneOn)
                ShowSceneOn();
        }

        #endregion

        #region Work with animations

        private void ShowSceneOn()
        {
            SetObject(animationSceneOn);
        }

        private void ShowSceneOff()
        {
            if (isSetToAnimationHolder)
                SetObjectToCamera(animationSceneOff);
            else
                SetObject(animationSceneOff);
        }

        private void SetObject(GameObject objectToSet)
        {
            GameObject newObject = (GameObject)Instantiate(objectToSet);
            newObject.transform.localPosition = objectToSet.transform.localPosition;
        }

        private void SetObjectToCamera(GameObject objectToSet)
        {
            GameObject animationHolder = GameObject.Find("AnimationHolder");

            GameObject newObject = (GameObject)Instantiate(objectToSet);
            newObject.transform.SetParent(animationHolder.transform);
            newObject.transform.localPosition = objectToSet.transform.localPosition;
            newObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        #endregion

        #region Notifications

        public void SceneWillBeClosedSoon()
        {
            if (animateSceneOff)
                ShowSceneOff();
        }

        #endregion
    }
}