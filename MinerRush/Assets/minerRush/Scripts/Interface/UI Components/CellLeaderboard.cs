

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class CellLeaderboard : MonoBehaviour
    {
        //Components
        private Text textPosition;
        private Text textName;
        private Text textValue;
        private Transform separator;

        //Values
        [HideInInspector]
        public float height;

        #region Standart system methods

        private void Awake()
        {
            //Get components
            textPosition = transform.Find("TextPosition").GetComponent<Text>();
            textName = transform.Find("TextName").GetComponent<Text>();
            textValue = transform.Find("TextValue").GetComponent<Text>();
            separator = transform.Find("Separator");

            //Get values
            height = GetComponent<RectTransform>().rect.height;
        }

        #endregion

        public void SetTextPosition(string position, string name, string value)
        {
            textPosition.text = position;
            textName.text = name;
            textValue.text = value;
        }

        public void HideSeparator()
        {
            separator.gameObject.SetActive(false);
        }
    }
}