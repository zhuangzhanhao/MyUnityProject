using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorResMgr : SingleInstanceBase<EditorResMgr>
{
    // 根路径
    private string rootPath = "Assets/Editor/ArtRes/";
    private EditorResMgr(){}

    /// <summary>
    /// 加载单个资源
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadEditorRes<T>(string path) where T : UnityEngine.Object
    {
        // 预设体、材质球、纹理等等
        string suffixName = "";
        if(typeof(T) == typeof(GameObject))
            suffixName = ".prefab";
        T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path + suffixName);
        return res;
    }

    /// <summary>
    /// 加载图集资源
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Sprite LoadSprite(string path, string spriteName)
    {
        UnityEngine.Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
        foreach(var item in sprites)
        {
            if(item.name == spriteName)
                return item as Sprite;
        }
        Debug.LogError("名为" + spriteName + "的图片不在该图集中");
        return null;
    }
}
