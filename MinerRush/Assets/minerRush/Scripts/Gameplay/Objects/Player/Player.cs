using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

namespace TinyStudio
{
    public class Player : CustomGameObject
    {
        //Components
        private Rigidbody2D rigidbody2D;
        private ParticleSystem particleSystemExplosion;
        private ParticleSystem particleSystemBlockExplosion;

        //Values
        public bool isPressed = false;

        private float borderLeft;
        private float borderRight;

        private float moveToX = 0f;
        private float rotationAngle = 0f;

        public GameObject GameOverPanel;

        public string clickid;
        private StarkAdManager starkAdManager;
        #region Standart system methods

        public override void Start()
        {
            base.Start();

            rigidbody2D = GetComponent<Rigidbody2D>();
            particleSystemExplosion = transform.Find("ParticleExplosion").GetComponent<ParticleSystem>();
            particleSystemBlockExplosion = transform.Find("ParticleBlockExplosion").GetComponent<ParticleSystem>();

            borderLeft = gameValues.worldBorderLeft;
            borderRight = gameValues.worldBorderRight;
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    isPressed = true;
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    MakeInput(touch.position);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    rotationAngle = 0f;
                    isPressed = false;
                }

                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isPressed = true;
            }
            if (isPressed)
            {
                MakeInput(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                rotationAngle = 0f;
                isPressed = false;
            }
        }

        void FixedUpdate()
        {
            TryToMove();
        }

        public void Kill()
        {
            if (gameController.isPlay)
            {
                CustomPlayerPrefs.SetBool("_isGameWin", false);

                particleSystemExplosion.Play();
                gameController.GameOver();
            }
        }
        public void End()
        {
            Time.timeScale = 1;
            CustomPlayerPrefs.SetBool("_isGameWin", false);
            particleSystemExplosion.Play();
            gameController.GameOver();
            
        }
        void Win()
        {
            if (gameController.isPlay)
            {
                CustomPlayerPrefs.SetBool("_isGameWin", true);

                gameController.GameOver();
            }
        }

        #endregion

        #region Collision

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                GameObject collisionGameObject = collision.gameObject;
                if (collisionGameObject != null)
                {
                    switch (collision.transform.tag)
                    {
                        case "Block":
                            Block collisedBlock = collision.gameObject.GetComponent<Block>();
                            switch (collisedBlock.type)
                            {
                                case Block.Type.Coin:
                                    collisedBlock.Dig();
                                    break;
                                case Block.Type.Normal:
                                    collisedBlock.Dig();
                                    particleSystemBlockExplosion.Play();
                                    break;
                                case Block.Type.Rock:
                                    Destroy(collision.gameObject);
                                    Time.timeScale = 0;
                                    GameOverPanel.SetActive(true);
                                    //Kill();
                                    ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        #region Logic
        public void Continue()
        {
            ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    Time.timeScale = 1;
                    GameOverPanel.SetActive(false);



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
        private void TryToMove()
        {
            if (gameController.isPlay)
            {
                rigidbody2D.velocity = new Vector2(0, -gameValues.speedPlayer.value);

                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(moveToX, transform.position.y, transform.position.z),
                    Time.fixedDeltaTime * gameValues.speedPlayer.value);

                transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            }
            else
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }

        public void MakeInput(Vector3 atPosition)
        {
            if (gameController.isPlay)
            {
                Vector3 positionInputAtWorld = Camera.main.ScreenToWorldPoint(atPosition);
                float distance = transform.position.x - positionInputAtWorld.x;

                rotationAngle = -(gameValues.playerAngle / gameValues.playerAtDistanceMax * distance);
                moveToX = positionInputAtWorld.x;
            }
        }


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
        #endregion
        /// <summary>
        /// 播放插屏广告
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="errorCallBack"></param>
        /// <param name="closeCallBack"></param>
        public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
        {
            starkAdManager = StarkSDK.API.GetStarkAdManager();
            if (starkAdManager != null)
            {
                var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
                mInterstitialAd.Load();
                mInterstitialAd.Show();
            }
        }
    }
}