using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SingleInstanceBase<T> where T : class
{
    private static readonly object lockObj = new object();
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        // 不用加new（）的约束，而是用反射来调用私有构造函数
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                        _instance = info.Invoke(null) as T;
                    }
                }
            }
            return _instance;
        }
    }
}
