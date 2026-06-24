using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : SingleInstanceBase<ResMgr>
{
    public void LoadAssetAsync<T>(string path, UnityAction<T> callBack) where T : Object
    {
        // 启动协程需要通过继承Mono的类，需要提前导入Mono公共模块。
        MonoMgr.Instance.StartCoroutine(LoadAssetCoroutine<T>(path, callBack));
    }
    private IEnumerator LoadAssetCoroutine<T>(string path, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest rq = Resources.LoadAsync<T>(path);
        yield return rq;
        callBack(rq.asset as T);
    }

    public void LoadAssetSync<T>(string path, UnityAction<T> callBack) where T : Object
    {
        callBack(Resources.Load<T>(path));
    }
    public void UnloadAsset(Object obj)
    {
        Resources.UnloadAsset(obj);
    }
    public void UnloadAssetAsync()
    {
        Resources.UnloadUnusedAssets();
    }
}
