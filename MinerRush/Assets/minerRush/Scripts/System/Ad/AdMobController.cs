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
using GoogleMobileAds.Api;

namespace TinyStudio
{
    public class AdMobController : MonoBehaviour
    {
        //Components
        private BannerView bannerView;
        private bool bannerIsLoaded = false;
        private InterstitialAd interstitial;

        //Values
        private static AdMobController instance = null;
        public static AdMobController Instance { get { return instance; } }

        [Header("Banner id's")]
        public string bannerIdIOS = "ca-app-pub-3940256099942544/2934735716";
        public string bannerIdAndroid = "ca-app-pub-3940256099942544/6300978111";
        [Header("Interstitial id's")]
        public string interstitialIdIOS = "ca-app-pub-3940256099942544/4411468910";
        public string interstitialIdAndroid = "ca-app-pub-3940256099942544/1033173712";

        #region Standart system methods

        void Awake()
        {
            //Singleton object return
            if (instance == null)
            {
                DontDestroyOnLoad(this);
                instance = this;

                //Prepare ads
                MobileAds.Initialize(initStatus => { });
                RequestBanner();
                RequestInterstitial();
            }
            else
                Destroy(gameObject);
        }

        #endregion

        #region Banner

        private void RequestBanner()
        {
#if UNITY_ANDROID
            string adUnitId = bannerIdAndroid;
#else
            string adUnitId = bannerIdIOS;
#endif

            //Destroy old banner
            if (bannerView != null)
                bannerView.Destroy();

            //Create banner at down at screen
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            bannerView.OnAdLoaded += HandleOnAdBannerLoaded;
            bannerView.OnAdFailedToLoad += HandleOnAdBannerFailedToLoad;
            bannerView.OnAdClosed += HandleOnAdBannerClosed;
            bannerView.OnAdOpening += HandleAdBannerOpened;
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            bannerView.Hide();
        }

        public void ShowBanner()
        {
            if (bannerIsLoaded)
                bannerView.Show();
            else
                RequestBanner();
        }

        public void HideBanner()
        {
            if (bannerView != null)
                bannerView.Destroy();
            RequestBanner();
        }

        //Delegates

        public void HandleOnAdBannerLoaded(object sender, System.EventArgs args)
        {
            bannerIsLoaded = true;
        }

        public void HandleAdBannerOpened(object sender, System.EventArgs args)
        {
            if (GameObject.Find("BANNER(Clone)") != null)
                GameObject.Find("BANNER(Clone)").GetComponent<Canvas>().sortingOrder = 99998;
        }

        public void HandleOnAdBannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            bannerIsLoaded = false;
        }

        public void HandleOnAdBannerClosed(object sender, System.EventArgs args)
        {
            bannerIsLoaded = false;
            RequestBanner();
        }

        #endregion

        #region Interstitial

        public void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = interstitialIdAndroid;
#else
            string adUnitId = interstitialIdIOS;
#endif

            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);
            interstitial.OnAdOpening += HandleOnAdInterstitialOpened;
            interstitial.OnAdClosed += HandleOnAdInterstitialClosed;
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
        }

        public void ShowInterstitial()
        {
            if (interstitial.IsLoaded())
                interstitial.Show();
            else
                RequestInterstitial();
        }

        //Delegates

        public void HandleOnAdInterstitialOpened(object sender, System.EventArgs args)
        {
            //MusicController.Instance.Stop();
            if (GameObject.Find("768x1024(Clone)") != null)
                GameObject.Find("768x1024(Clone)").GetComponent<Canvas>().sortingOrder = 99999;
        }

        public void HandleOnAdInterstitialClosed(object sender, System.EventArgs args)
        {
            RequestBanner();
            //MusicController.Instance.Play();
        }

        #endregion
    }
}