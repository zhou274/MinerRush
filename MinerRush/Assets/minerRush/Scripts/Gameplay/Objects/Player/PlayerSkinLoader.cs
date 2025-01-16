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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    public class PlayerSkinLoader : MonoBehaviour
    {
        //Components
        private SkinHolder skinHolder;

        private SpriteRenderer spriteRenderer;

        #region Standart system methods

        void Awake()
        {
            skinHolder = GameObject.Find("SkinHolder").GetComponent<SkinHolder>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            SetCurrentSkin();
        }

        #endregion

        /// <summary>
        /// Load selected in store skin
        /// </summary>
        private void SetCurrentSkin()
        {
            int currentSkinID = CustomPlayerPrefs.GetInt("currentSkin", 1) - 1;
            Skin skin = skinHolder.skins[currentSkinID];

            spriteRenderer.sprite = skin.sprite;
        }
    }
}