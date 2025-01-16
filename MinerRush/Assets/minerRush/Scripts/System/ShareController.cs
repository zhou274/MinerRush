

using UnityEngine;

namespace TinyStudio
{
    public class ShareController : MonoBehaviour
    {

        //Values
        private string subject = "";
        public string appLink = "http://bit.ly/taptapstudio"; //Recomend to use short link

        #region Standart system methods

        private void Start()
        {
            //Find components
            subject = Application.productName;
        }

        #endregion

        /// <summary>
        /// Call to share current predication.
        /// Call this method only at MainScene with worked HoroscopeController.
        /// </summary>
        public void Share()
        {
            string textToShare = "Beat my  score: " + CustomPlayerPrefs.GetInt("highScore").ToString() + appLink;
            new NativeShare().SetSubject(subject).SetText(textToShare).Share();
        }
    }
}