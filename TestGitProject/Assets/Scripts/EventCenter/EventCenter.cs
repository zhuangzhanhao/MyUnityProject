using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 仅用于继承
/// </summary>
public abstract class DataBase{}
public class Data<T> : DataBase
{
    public UnityAction<T> actions;
    public Data(UnityAction<T> fun)
    {
        actions += fun;
    }
}

public class Data : DataBase
{
    public UnityAction actions;
    public Data(UnityAction fun)
    {
        actions += fun;
    }
}

public enum E_Event
{
    EventOne,
    EventTwo,
    EventThree,
}

public class EventCenter : SingleInstanceBase<EventCenter>
{
    private Dictionary<E_Event, DataBase> eventDic = new Dictionary<E_Event, DataBase>();

    private EventCenter(){}


    public void EventTrigger<T>(E_Event eventName, T data)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as Data<T>).actions?.Invoke(data);
        }
    }
    public void EventTrigger(E_Event eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as Data).actions?.Invoke();
        }
    }

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="eventName">主题对象</param>
    /// <param name="fun">触发事件</param>
    public void AddEventListener<T>(E_Event eventName, UnityAction<T> fun)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new Data<T>(fun));
        }
        else
        {
            (eventDic[eventName] as Data<T>).actions += fun;
        }
    }
    /// <summary>
    /// 重载，无参
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="fun"></param>
    public void AddEventListener(E_Event eventName, UnityAction fun)
    {
        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new Data(fun));
        }
        else
        {
            (eventDic[eventName] as Data).actions += fun;
        }
    }

    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName">主题对象</param>
    /// <param name="fun">移除事件</param>
    public void RemoveEventListener<T>(E_Event eventName, UnityAction<T> fun)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as Data<T>).actions -= fun;
        }
    }
    /// <summary>
    /// 重载，无参
    /// </summary>
    /// <param name="eventName">主题对象</param>
    /// <param name="fun">移除事件</param>
    public void RemoveEventListener(E_Event eventName, UnityAction fun)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as Data).actions -= fun;
        }
    }

    /// <summary>
    /// 移除所有主题对象
    /// </summary>
    public void ClearAllSubjects()
    {
        eventDic.Clear();
    }

    /// <summary>
    /// 移除某一个主题对象的所有监听事件
    /// </summary>
    /// <param name="eventName">主题对象名</param>
    public void ClearAllEvents(E_Event eventName)
    {
        if (eventDic.ContainsKey(eventName))
        {
            eventDic.Remove(eventName);
        }
    }
}
