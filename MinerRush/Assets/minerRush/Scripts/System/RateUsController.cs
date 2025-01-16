

using UnityEngine;

namespace TinyStudio
{
    public class RateUsController : MonoBehaviour
    {
        //Values
        public string link = "http://bit.ly/taptapstudio"; //Recomend to use short link

        public void Open()
        {
            Application.OpenURL(link);
        }
    }
}