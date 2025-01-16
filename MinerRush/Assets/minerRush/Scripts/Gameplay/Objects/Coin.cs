
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class Coin : CustomGameObject
    {
        //Componenets
        public GameObject particleCoinCollect;

        #region Standart system methods

        public override void Start()
        {
            base.Start();
        }

        #endregion

        /// <summary>
        /// Call this when you collect coin
        /// </summary>
        public void Collect()
        {
            gameController.IncreaseCoindBy(1);
            CreateParticleCoinCollect();
            Destroy(gameObject);
        }

        public void CreateParticleCoinCollect()
        {
            GameObject particle = GameObject.Instantiate(particleCoinCollect);
            particle.transform.position = transform.position;
            particle.transform.localScale = Vector3.one;
        }
    }
}