using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerMgr : SingleInstanceBase<TimerMgr>
{
    private int TIMER_KEY = 0;
    private Dictionary<int, TimerItem> _timerDic = new Dictionary<int, TimerItem>();
    private Coroutine timer;
    private const float intervalTime = 0.1f;
    private List<TimerItem> removeTimerItems = new List<TimerItem>();
    private TimerMgr()
    {
        Start();
    }
    
    // 开启计时器管理器
    public void Start()
    {
        timer = MonoMgr.Instance.StartCoroutine(StartTiming());
    }

    // 关闭计时器管理器
    public void Stop()
    {
        MonoMgr.Instance.StopCoroutine(timer);
    }

    private IEnumerator StartTiming()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);
            foreach(var item in _timerDic.Values)
            {
                // 判断计时器是否已经启动
                if(item.isRunning == false)
                    continue;
                // 是否有间隔时间执行的需求
                if(item.callBack != null)
                {
                    item.intervalTime -= (int)(intervalTime * 1000);
                    if (item.intervalTime <= 0)
                    {
                        item.callBack.Invoke();
                        // 重置间隔时间
                        item.intervalTime = item.maxIntervalTime;
                    }
                }
                // 总时间结束之后执行的
                item.allTime -= (int)(intervalTime * 1000);
                if(item.allTime <= 0)
                {
                    item.overCallBack.Invoke();
                    // 结束计时过后可以通过待移除列表移除
                    removeTimerItems.Add(item);
                }
            }

            // 移除所有的待移除计时器
            for(int i = 0; i < removeTimerItems.Count; i++)
            {
                // 在字典中移除
                _timerDic.Remove(removeTimerItems[i].keyID);
                // 在缓存池模块中添加 没有继承Mono的对象的存取
                PoolMgr.Instance.PushObj(removeTimerItems[i]);
            }
            removeTimerItems.Clear();
        }
    }
    
    // 创建单个计时器
    public int CreateTimerItem(int allTime, UnityAction overCallBack, int intervalTime = 0, UnityAction callBack = null)
    {
        int keyID = ++TIMER_KEY;
        TimerItem item = PoolMgr.Instance.GetObj<TimerItem>();
        item.InitInfo(keyID, allTime, overCallBack, intervalTime, callBack);
        _timerDic.Add(keyID, item);
        return keyID;
    }
    // 移除单个计时器
    public void RemoveTimer(int keyID)
    {
        TimerItem item;
        if(_timerDic.TryGetValue(keyID, out item))
        {
            PoolMgr.Instance.PushObj(item);
            _timerDic.Remove(keyID);
        }
    }
    // 重置单个计时器
    public void ResetTimer(int keyID)
    {
        TimerItem item;
        if(_timerDic.TryGetValue(keyID, out item))
        {
            item.ResetTimer();
        }
    }
    // 开启单个计时器
    public void StartTimer(int keyID)
    {
        TimerItem item;
        if(_timerDic.TryGetValue(keyID, out item))
        {
            item.isRunning = true;
        }
    }
    // 停止单个计时器
    public void StopTimer(int keyID)
    {
        TimerItem item;
        if(_timerDic.TryGetValue(keyID, out item))
        {
            item.isRunning = false;
        }
    }
}
