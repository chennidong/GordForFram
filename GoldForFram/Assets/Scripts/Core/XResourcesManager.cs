using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

class XResourcesManager : MonoBehaviour
{
    private static XResourcesManager _resources;

    public static XResourcesManager instance
    {
        get
        {
            if (_resources==null)
            {
                GameObject obj = new GameObject("ResourcesManager");
                DontDestroyOnLoad(obj);
                _resources = obj.AddComponent<XResourcesManager>();
            }
            return _resources;
        }
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 加载GameObject对象实例
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject LoadGameObject(string path,Transform parent=null)
    {
        GameObject temp = Load<GameObject>(path);
        GameObject obj = Instantiate(temp, parent, false);
        return obj;
    }
    
    /// <summary>
    /// 异步加载对象
    /// </summary>
    /// <param name="path"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    public void LoadAsync<T>(string path,UnityAction<T> action) where T : Object
    {
        StartCoroutine(LoadAsyncs(path,action));
    }
    private IEnumerator LoadAsyncs<T>(string path, UnityAction<T> action) where T : Object
    {
        ResourceRequest _request = Resources.LoadAsync<T>(path);
        Debug.Log(_request.progress);
        yield return _request;
        action(_request.asset as T);
    }
    /// <summary>
    /// 加载图片
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Texture LoadTexture(string path)
    {
        return Load<Texture>(path);
    }
    /// <summary>
    /// 加载图集
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public SpriteAtlas LoadSpriteAtlas(string path)
    {
        return Load<SpriteAtlas>(path);
    }
}