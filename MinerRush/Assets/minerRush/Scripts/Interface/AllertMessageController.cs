
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class AllertMessageController : MonoBehaviour
    {
        //Components
        private GameObject canvas;
        private TextMeshProUGUI textTitle;
        private TextMeshProUGUI textContext;

        #region Standart system methods

        void Awake()
        {
            //Get components
            canvas = GameObject.Find("CanvasAllert");
            textTitle = canvas.transform.Find("Panel").Find("TextTitle").GetComponent<TextMeshProUGUI>();
            textContext = canvas.transform.Find("Panel").Find("TextContext").GetComponent<TextMeshProUGUI>();
        }

        #endregion

        #region State

        /// <summary>
        /// Call to show allert message
        /// </summary>
        /// <param name="title"> Title</param>
        /// <param name="context"> Context</param>
        public void ShowWithTitle(string title, string context)
        {
            textTitle.text = title;
            textContext.text = context;

            canvas.SetActive(true);
        }

        /// <summary>
        /// Hide allert
        /// </summary>
        public void Hide()
        {
            canvas.SetActive(false);
        }

        #endregion
    }
}