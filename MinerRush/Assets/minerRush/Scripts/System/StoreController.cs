using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

namespace TinyStudio
{
    public class StoreController : MonoBehaviour
    {
        //Components
        private SkinHolder skinHolder; //This is singletone object initialised in LaunchScene
        private Canvas canvas;
        private AllertMessageController canvasAllert;

        //Values
        public GameObject skinIndicator;
        private SkinStoreIndicator currentIndicator;

        public string clickid;
        private StarkAdManager starkAdManager;
        public enum Allert
        {
            PurchaseSuccess, PurchaseFailed
        }

        private SkinStoreIndicator[] indicators;

        #region Standart system methods

        void Start()
        {
            //Get components
            skinHolder = GameObject.Find("SkinHolder").GetComponent<SkinHolder>();
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            canvasAllert = GetComponent<AllertMessageController>();

            //Prepare scene
            canvasAllert.Hide();
            skinHolder.LoadStates();

            CreateIndicators();
            GetCurrentIndicator();
        }

        #endregion

        #region Work with indicators

        public void CreateIndicators()
        {
            indicators = new SkinStoreIndicator[skinHolder.skins.Length];

            for (int i = 0; i < skinHolder.skins.Length; i++)
            {
                GameObject indicator = GameObject.Instantiate(skinIndicator);
                indicator.transform.SetParent(canvas.transform);
                indicator.GetComponent<RectTransform>().localScale = Vector3.one;
                indicator.GetComponent<SkinStoreIndicator>().LoadSkin(skinHolder.skins[i]);

                indicators[i] = indicator.GetComponent<SkinStoreIndicator>();

            }
        }

        public void RefreshIndicators()
        {
            for (int i = 0; i < indicators.Length; i++)
                indicators[i].RefreshState();
        }

        #endregion

        #region Interface

        public void SkinNext()
        {
            ChangePickedSkinToPositive(true);
        }

        public void SkinPrevious()
        {
            ChangePickedSkinToPositive(false);
        }

        private void ChangePickedSkinToPositive(bool positive)
        {
            int idPicked = CustomPlayerPrefs.GetInt("storeSkin_Picked", 1);
            idPicked += positive ? 1 : -1;

            if (idPicked <= 0)
                idPicked = skinHolder.skins.Length;
            if (idPicked > skinHolder.skins.Length)
                idPicked = 1;

            CustomPlayerPrefs.SetInt("storeSkin_Picked", idPicked);
            GetCurrentIndicator();
        }
        public void AddCoins(int value)
        {
            ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    int coins = CustomPlayerPrefs.GetInt("coin");
                    coins += value;

                    CustomPlayerPrefs.SetInt("coin", coins);


                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
            

        }
        private void GetCurrentIndicator()
        {
            int idPicked = CustomPlayerPrefs.GetInt("storeSkin_Picked", 1);
            currentIndicator = indicators[idPicked - 1];
        }

        public void SkinSelect()
        {
            currentIndicator.Select();
        }

        #endregion
        public void getClickid()
        {
            var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
            if (launchOpt.Query != null)
            {
                foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                    if (kv.Value != null)
                    {
                        Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                        if (kv.Key.ToString() == "clickid")
                        {
                            clickid = kv.Value.ToString();
                        }
                    }
                    else
                    {
                        Debug.Log(kv.Key + "<-参数-> " + "null ");
                    }
            }
        }

        public void apiSend(string eventname, string clickid)
        {
            TTRequest.InnerOptions options = new TTRequest.InnerOptions();
            options.Header["content-type"] = "application/json";
            options.Method = "POST";

            JsonData data1 = new JsonData();

            data1["event_type"] = eventname;
            data1["context"] = new JsonData();
            data1["context"]["ad"] = new JsonData();
            data1["context"]["ad"]["callback"] = clickid;

            Debug.Log("<-data1-> " + data1.ToJson());

            options.Data = data1.ToJson();

            TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
               response => { Debug.Log(response); },
               response => { Debug.Log(response); });
        }


        /// <summary>
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="closeCallBack"></param>
        /// <param name="errorCallBack"></param>
        public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
            }
        }
        #region Allerts

        public void AllertShow(Allert allert)
        {
            switch (allert)
            {
                case Allert.PurchaseFailed:
                    canvasAllert.ShowWithTitle("失败", "没有足够的金币");
                    break;
                case Allert.PurchaseSuccess:
                    canvasAllert.ShowWithTitle("成功", "你获得一款新皮肤");
                    break;
            }
        }

        public void AllertHide()
        {
            canvasAllert.Hide();
        }

        #endregion
    }
}