
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class CameraController : MonoBehaviour
    {
        //Components
        private GameObject animationHolder;

        //Values
        public GameObject target;
        private Vector2 targetDefaultPosition;

        public bool followByX = false;
        public bool followByY = true;

        public float speed = 10f;

        #region Standart system methods

        private void Start()
        {
            //Get component
            animationHolder = GameObject.Find("AnimationHolder");

            //Save default position of target
            targetDefaultPosition = new Vector2(target.transform.position.x, target.transform.position.y);
        }

        void FixedUpdate()
        {
            MoveToTarget();
            AttachAnimationHolder();
        }

        #endregion

        /// <summary>
        /// Move to target include default target position shift
        /// </summary>
        private void MoveToTarget()
        {
            //If option selected
            if (followByX || followByY)
            {
                //Calculate positions
                float positionX = 0f;
                if (followByX)
                {
                    positionX = targetDefaultPosition.x * -1;
                    positionX += target.transform.position.x;
                }
                float positionY = 0f;
                if (followByY)
                {
                    positionY = targetDefaultPosition.y * -1;
                    positionY += target.transform.position.y;
                }

                Vector3 moveTo = new Vector3(positionX, positionY, transform.position.z);

                //Set position
                transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.fixedDeltaTime * speed);
            }
        }

        /// <summary>
        /// Required for some global animations wjen camera move position during the game.
        /// </summary>
        private void AttachAnimationHolder()
        {
            animationHolder.transform.position = transform.position;
        }
    }
}