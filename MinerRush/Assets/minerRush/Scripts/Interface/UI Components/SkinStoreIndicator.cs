
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinyStudio
{
    public class SkinStoreIndicator : MonoBehaviour
    {
        //Components
        private StoreController storeController;
        private SkinHolder skinHolder;

        private Image imageSkin;
        private Image imageLock;
        private Image imageCoin;
        private TextMeshProUGUI textStatus;
        private Text textPrice;

        private Animation animationImageLock;
        private Animation animationImageCoin;
        private Animation animationTextStatus;
        private Animation animationTextPrice;

        //Values
        private bool isStateInitialisation = true;

        private RectTransform rectTransform;
        public Vector3 positionToMove;

        public float shiftByX = 1f;
        public float shiftByY = 0.5f;

        public float moveSpeed = 10f;

        enum State
        {
            Selected, Wait, NonPurchased
        }
        private State state;
        private Skin skin;

        #region System default methods

        void Awake()
        {
            GetComponents();
        }

        private void Start()
        {
            RefreshState();
        }

        private void FixedUpdate()
        {
            CalculatePosition();
            MoveToPosition();
        }

        #endregion

        #region Work with state

        private void GetComponents()
        {
            skinHolder = GameObject.Find("SkinHolder").GetComponent<SkinHolder>();
            storeController = GameObject.Find("StoreController").GetComponent<StoreController>();

            imageSkin = transform.Find("ImageSkin").GetComponent<Image>();
            imageLock = transform.Find("ImageLock").GetComponent<Image>();
            textStatus = transform.Find("TextStatus").GetComponent<TextMeshProUGUI>();
            imageCoin = transform.Find("PriceValue").Find("ImageCoin").GetComponent<Image>();
            textPrice = transform.Find("PriceValue").Find("TextPrice").GetComponent<Text>();

            animationImageLock = imageLock.GetComponent<Animation>();
            animationImageCoin = imageCoin.GetComponent<Animation>();
            animationTextStatus = textStatus.GetComponent<Animation>();
            animationTextPrice = textPrice.GetComponent<Animation>();

            rectTransform = GetComponent<RectTransform>();
        }

        public void LoadSkin(Skin skin)
        {
            this.skin = skin;

            textPrice.text = skin.price.ToString();
            imageSkin.GetComponent<Image>().sprite = skin.sprite;
        }

        public void Select()
        {
            if (skin.isAvailable)
            {
                skin.SelectThis();
            }
            else
            {
                bool purchaseIsSuccess = skin.TryToBuy();

                if (purchaseIsSuccess)
                {
                    skinHolder.LoadStates();
                    skin.SelectThis();

                    storeController.AllertShow(StoreController.Allert.PurchaseSuccess);
                }
                else
                {
                    storeController.AllertShow(StoreController.Allert.PurchaseFailed);
                }
            }

            RefreshIndicators();
        }

        #endregion

        #region Work with interface

        //State
        private void RefreshIndicators()
        {
            storeController.RefreshIndicators();
        }

        public void RefreshState()
        {
            State newState = GetCurrentState();

            if (state != newState || isStateInitialisation)
            {
                isStateInitialisation = false;

                state = newState;
                PrepareState();
            }
        }

        private State GetCurrentState()
        {
            if (skin.isAvailable)
            {
                if (skin.IsSelected())
                    return State.Selected;
                else
                    return State.Wait;
            }
            else
                return State.NonPurchased;
        }

        private void PrepareState()
        {
            switch (state)
            {
                case State.NonPurchased:
                    imageLock.gameObject.SetActive(true);
                    textStatus.text = "未解锁";
                    textPrice.gameObject.SetActive(true);
                    imageCoin.gameObject.SetActive(true);
                    break;
                case State.Selected:
                    imageLock.gameObject.SetActive(false);
                    textStatus.text = "当前选择";
                    textPrice.gameObject.SetActive(false);
                    imageCoin.gameObject.SetActive(false);
                    break;
                case State.Wait:
                    imageLock.gameObject.SetActive(false);
                    textStatus.text = "";
                    textPrice.gameObject.SetActive(false);
                    imageCoin.gameObject.SetActive(false);
                    break;
            }
        }

        //Positions
        private void CalculatePosition()
        {
            int idPicked = CustomPlayerPrefs.GetInt("storeSkin_Picked", 1);
            int id = skin.id;

            int diff = id - idPicked;
            int diffAbsolute = Mathf.Abs(diff);

            float positionX = shiftByX * diff;
            float positionY = shiftByY * diffAbsolute;

            positionToMove = new Vector3(positionX, positionY, 0f);
        }

        private void MoveToPosition()
        {
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, positionToMove, Time.fixedDeltaTime * moveSpeed);
        }

        #endregion
    }
}