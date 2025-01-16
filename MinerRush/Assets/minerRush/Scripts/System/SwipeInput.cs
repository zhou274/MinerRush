

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TinyStudio
{
    public class SwipeInput : MonoBehaviour
    {
        private Vector2 firstTouch;

        public bool isOn = false;
        public bool isKeyboardInputIfDidntFindTouchSwipe = false;
        private bool isSwipeCompleate = true;

        public float swipeDistance = 95f;
        public float timeLimit = 0.3f;
        private float currentTimeLimit;

        enum SwipeType
        {
            Up, Down, Left, Right
        }
        public enum TypeInput
        {
            Find, Touch, Mouse, Keyboard
        }
        public TypeInput typeInput = TypeInput.Find;

        [Header("Output")]
        public UnityEvent swipeUpCompleate;
        public UnityEvent swipeDownCompleate;
        public UnityEvent swipeLeftCompleate;
        public UnityEvent swipeRightCompleate;

        #region Standart system methods

        void Update()
        {
            if (isOn)
            {
                //Touch Input
                if (typeInput == TypeInput.Find || typeInput == TypeInput.Touch)
                {
                    if (Input.touchCount > 0)
                    {
                        typeInput = TypeInput.Touch;

                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                            SaveFirstTouch(Input.touches[0].position);
                        if (touch.phase == TouchPhase.Moved)
                            CheckSwipeIsCompleate(Input.touches[0].position);
                    }
                }

                if (!isKeyboardInputIfDidntFindTouchSwipe)
                {
                    //Mouse input
                    if (typeInput == TypeInput.Find || typeInput == TypeInput.Mouse)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            typeInput = TypeInput.Mouse;
                            SaveFirstTouch(Input.mousePosition);
                        }
                        if (Input.GetMouseButtonUp(0))
                            isSwipeCompleate = true;
                        if (!isSwipeCompleate)
                            CheckSwipeIsCompleate(Input.mousePosition);
                    }
                }
                else
                {
                    //Keyboard input
                    if (typeInput == TypeInput.Find || typeInput == TypeInput.Keyboard)
                    {
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            typeInput = TypeInput.Keyboard;
                            SwipeCompleate(SwipeType.Up);
                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            typeInput = TypeInput.Keyboard;
                            SwipeCompleate(SwipeType.Down);
                        }
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            typeInput = TypeInput.Keyboard;
                            SwipeCompleate(SwipeType.Left);
                        }
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            typeInput = TypeInput.Keyboard;
                            SwipeCompleate(SwipeType.Right);
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            TryToCancelSwipeByTime();
        }

        #endregion

        private void SaveFirstTouch(Vector2 withPosition)
        {
            isSwipeCompleate = false;
            currentTimeLimit = timeLimit;
            firstTouch = withPosition;
        }

        private void CheckSwipeIsCompleate(Vector2 withPosition)
        {
            if (!isSwipeCompleate)
            {
                float diffX = firstTouch.x - withPosition.x;
                float diffY = firstTouch.y - withPosition.y;
                bool isXTypeSwipe = Mathf.Abs(diffX) > Mathf.Abs(diffY);

                if (Mathf.Abs(isXTypeSwipe ? diffX : diffY) > swipeDistance)
                {
                    isSwipeCompleate = true;
                    if (isXTypeSwipe)
                        SwipeCompleate(diffX > 0 ? SwipeType.Left : SwipeType.Right);
                    else
                        SwipeCompleate(diffY > 0 ? SwipeType.Down : SwipeType.Up);
                }
            }
        }

        private void TryToCancelSwipeByTime()
        {
            if (!isSwipeCompleate)
            {
                currentTimeLimit -= Time.fixedDeltaTime;
                if (currentTimeLimit <= 0f)
                    isSwipeCompleate = true;
            }
        }

        private void SwipeCompleate(SwipeType swipeType)
        {
            switch (swipeType)
            {
                case SwipeType.Up:
                    if (swipeUpCompleate != null)
                        swipeUpCompleate.Invoke();
                    break;
                case SwipeType.Down:
                    if (swipeDownCompleate != null)
                        swipeDownCompleate.Invoke();
                    break;
                case SwipeType.Left:
                    if (swipeLeftCompleate != null)
                        swipeLeftCompleate.Invoke();
                    break;
                case SwipeType.Right:
                    if (swipeRightCompleate != null)
                        swipeRightCompleate.Invoke();
                    break;
            }
        }
    }
}