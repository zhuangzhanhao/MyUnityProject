using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingletonAutoMono<MonoMgr>
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    private event UnityAction lateUpdateEvent;

    private void Update()
    {
        updateEvent?.Invoke();
    }
    private void FixedUpdate()
    {
        fixedUpdateEvent?.Invoke();
    }
    private void LateUpdate()
    {
        lateUpdateEvent?.Invoke();
    }

    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    public void AddFixedUpdateListener(UnityAction fun)
    {
        fixedUpdateEvent += fun;
    }
    public void AddLateUpdateListener(UnityAction fun)
    {
        lateUpdateEvent += fun;
    }
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
    public void RemoveFixedUpdateListener(UnityAction fun)
    {
        fixedUpdateEvent -= fun;
    }
    public void RemoveLateUpdateListener(UnityAction fun)
    {
        lateUpdateEvent -= fun;
    }
}
