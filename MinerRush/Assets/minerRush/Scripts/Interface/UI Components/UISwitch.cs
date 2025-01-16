

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TinyStudio
{
    public class UISwitch : MonoBehaviour
    {
        //Components
        private Text text;
        private Image pin;
        private Animation pinAnimation;

        //Values
        private bool isInitialisationState = true;

        [System.Serializable]
        public enum Mode { On, Off };
        private Mode mode = Mode.On;

        private float positionPinDefault = 23.17f;
        private float positionPin = 25.0f;

        public float speed = 5.0f;
        public string value = "";
        public UnityEvent valueChanged;

        #region Standart system methods

        void Start()
        {
            GetComponents();
            LoadState();
        }

        private void FixedUpdate()
        {
            MovePin();
        }

        private void GetComponents()
        {
            text = transform.Find("Text").GetComponent<Text>();
            pin = transform.Find("Pin").GetComponent<Image>();
            pinAnimation = pin.gameObject.GetComponent<Animation>();

            positionPinDefault = pin.GetComponent<RectTransform>().localPosition.x;
        }

        #endregion

        #region Work with state

        private void LoadState()
        {
            mode = CustomPlayerPrefs.GetBool(value, true) ? Mode.On : Mode.Off;
            RunPinAnimation(mode);
            ChangeStateTo(mode);
            MoveInstant();
        }

        private void SaveState()
        {
            CustomPlayerPrefs.SetBool(value, mode == Mode.On ? true : false);
        }

        public void ChangeState()
        {
            ChangeStateTo(mode == Mode.On ? Mode.Off : Mode.On);
        }

        private void ChangeStateTo(Mode newMode)
        {
            if (mode != newMode || isInitialisationState)
            {
                mode = newMode;
                RunPinAnimation(mode);
                switch (mode)
                {
                    case Mode.On:
                        positionPin = positionPinDefault;
                        break;
                    case Mode.Off:
                        positionPin = -positionPinDefault;
                        break;
                }

                SaveState();
                if (!isInitialisationState)
                    if (valueChanged != null)
                        valueChanged.Invoke();

                isInitialisationState = false;
            }
        }

        #endregion

        #region Animation

        private void RunPinAnimation(Mode withMode)
        {
            switch (withMode)
            {
                case Mode.On:
                    pinAnimation.clip = pinAnimation.GetClip("SwitchPin_AnimationFadeOn");
                    break;
                case Mode.Off:
                    pinAnimation.clip = pinAnimation.GetClip("SwitchPin_AnimationFadeOff");
                    break;
            }
            pinAnimation.Play();
        }

        private void MovePin()
        {
            pin.transform.localPosition = Vector3.MoveTowards(pin.transform.localPosition,
                new Vector3(positionPin, 0.0f, 0.0f),
                Time.deltaTime * speed);
        }

        private void MoveInstant()
        {
            pin.transform.localPosition = new Vector3(positionPin, 0.0f, 0.0f);
        }

        #endregion
    }
}