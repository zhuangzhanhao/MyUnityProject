using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerItem : IPoolObject
{
    public int keyID;
    public UnityAction overCallBack;
    public UnityAction callBack;
    public int allTime;// ms
    public int maxAllTime;// 一开始计时时的总时间，用于重置
    public int intervalTime;
    public int maxIntervalTime;
    public bool isRunning;


    public void InitInfo(int keyID, int allTime, UnityAction overCallBack, int intervalTime = 0, UnityAction callBack = null)
    {
        this.keyID = keyID;
        this.maxAllTime = allTime;
        this.maxIntervalTime = intervalTime;
        this.intervalTime = intervalTime;
        this.overCallBack = overCallBack;
        this.callBack = callBack;
        this.allTime = allTime;
        this.isRunning = true;
    }

    public void ResetInfo()
    {
        overCallBack = null;
        callBack = null;
    }
    public void ResetTimer(){
        this.allTime = this.maxAllTime;
        this.intervalTime = this.maxIntervalTime;
        this.isRunning = true;
    }
}
