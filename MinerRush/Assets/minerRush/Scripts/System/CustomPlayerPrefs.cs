

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyStudio
{
    /// <summary>
    /// This class is extended PlayerPrefs class.
    /// It's has a methods for save and load arrays and other values.
    /// </summary>
    public class CustomPlayerPrefs : PlayerPrefs
    {
        //Values
        static private string idArrayLenght = "_customArrays_Length";
        static private string idArrayObjectID = "__customArrays_objectID";

        #region Boolean

        static public void SetBool(string key, bool value)
        {
            SetInt(key, value == true ? 1 : 0);
        }

        static public bool GetBool(string key)
        {
            return GetInt(key) == 1 ? true : false;
        }

        static public bool GetBool(string key, bool defaultValue)
        {
            return GetInt(key, defaultValue ? 1 : 0) == 1 ? true : false;
        }

        static public void SetBoolArray(string key, bool[] values)
        {
            SetInt(key + idArrayLenght, values.Length);

            for (int index = 0; index < values.Length; index++)
                SetInt(key + idArrayObjectID + index, values[index] == true ? 1 : 0);
        }

        static public bool[] GetBoolArray(string key)
        {
            int length = GetInt(key + idArrayLenght, 0);

            bool[] values = new bool[length];
            for (int index = 0; index < length; index++)
                values[index] = GetInt(key + idArrayObjectID + index) == 1 ? true : false;

            return values;
        }

        #endregion

        #region Float

        static public void SetFloatArray(string key, float[] values)
        {
            SetInt(key + idArrayLenght, values.Length);

            for (int index = 0; index < values.Length; index++)
                SetFloat(key + idArrayObjectID + index, values[index]);
        }

        static public float[] GetFloatArray(string key)
        {
            int length = GetInt(key + idArrayLenght, 0);

            float[] values = new float[length];
            for (int index = 0; index < length; index++)
                values[index] = GetFloat(key + idArrayObjectID + index);

            return values;
        }

        #endregion

        #region Int

        static public void SetIntArray(string key, int[] values)
        {
            SetInt(key + idArrayLenght, values.Length);

            for (int index = 0; index < values.Length; index++)
                SetInt(key + idArrayObjectID + index, values[index]);
        }

        static public int[] GetIntArray(string key)
        {
            int length = GetInt(key + idArrayLenght, 0);

            int[] values = new int[length];
            for (int index = 0; index < length; index++)
                values[index] = GetInt(key + idArrayObjectID + index);

            return values;
        }

        #endregion

        #region String

        public void SetStringArray(string key, string[] values)
        {
            SetInt(key + idArrayLenght, values.Length);

            for (int index = 0; index < values.Length; index++)
                SetString(key + idArrayObjectID + index, values[index]);
        }

        public string[] GetStringArray(string key)
        {
            int length = GetInt(key + idArrayLenght, 0);

            string[] values = new string[length];
            for (int index = 0; index < length; index++)
                values[index] = GetString(key + idArrayObjectID + index);

            return values;
        }

        #endregion
    }
}