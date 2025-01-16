/* 
 *       _______             _____ __            ___     
 *      /_  __(_)___  __  __/ ___// /___  ______/ (_)___ 
 *       / / / / __ \/ / / /\__ \/ __/ / / / __  / / __ \
 *      / / / / / / / /_/ /___/ / /_/ /_/ / /_/ / / /_/ /
 *     /_/ /_/_/ /_/\__, //____/\__/\__,_/\__,_/_/\____/ 
 *                 /____/                                
 *
 *      Created by Alice Vinnik in 2022.
 * 
 *      If you want to reskin, customization or development contact me. Im available to hire.
 *      Website: https://alicevinnik.wixsite.com/tinystudio
 *      Email: tinystudio.main@gmail.com
 *      
 *      Thanks for buying my codes.
 *      Have a nice day!
 *   
 */

using UnityEngine;

namespace TinyStudio
{
    public class AdsInterface : MonoBehaviour
    {
        //Components
        AdMobController adMobController;

        //Values
        [Header("Rules")]
        public bool showBannerAtStart = false;
        public bool showInterstitialAtStart = false;

        #region Standart system methods

        private void Start()
        {
            //Try to show ads
            if (CustomPlayerPrefs.GetBool("ads_removed"))
            {
                adMobController = AdMobController.Instance;

                LaunchStartSceneOptions();
            }
        }

        #endregion

        #region Advertation showing process

        /// <summary>
        /// Show ads by default settings
        /// </summary>
        private void LaunchStartSceneOptions()
        {
            adMobController.HideBanner();
            if (showBannerAtStart)
                adMobController.ShowBanner();
            if (showInterstitialAtStart)
                adMobController.ShowInterstitial();
        }

        #endregion
    }
}