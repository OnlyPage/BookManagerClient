using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Cutil 
{
    public static T ToEnum<T>(this string value, bool ignoreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    public static string ImportJson(string path, bool isDecode64 = false)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        string jsonString = textAsset.text;
        if (isDecode64)
        {
            byte[] byteArray = System.Convert.FromBase64String(jsonString);

            jsonString = Encoding.UTF8.GetString(byteArray);
        }
        return jsonString;
    }

    public static T ImportJsonObjest<T>(string jsonString)
    {
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

    public static RectTransform Top(this RectTransform rt, float y)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -y);
        return rt;
    }

}
