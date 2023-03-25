/*--------------------------------------
// 文件名称: UiModule
// 创建者: CND
// 创建时间: 2023年03月19日 星期日 22:00
//======================================
// 功能描述:
//
//
//--------------------------------------*/

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 要控制的ui组件对象从这获取
/// </summary>
public class UiModule
{
    private Dictionary<string, Object> _uiDic = new Dictionary<string, Object>();

    public void AddObj(Object obj)
    {
        _uiDic.Add(obj.name,obj);
    }

    /// <summary>
    /// 根据名字获取对应的组件对象
    /// </summary>
    /// <param name="name">名字</param>
    /// <typeparam name="T">组件类型</typeparam>
    /// <returns>组件对象 没有反空</returns>
    public T GetModule<T>(string name) where T : class
    {
        if (_uiDic.ContainsKey(name))
        {
            return _uiDic[name] as T;
        }
        Debug.Log($"没有 {name} 这名字的组件对象");
        return null;
    }
    /// <summary>
    /// 检查是否存储
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckKey(string key)
    {
        return _uiDic.ContainsKey(key);
    }
    
}

[Serializable]
public struct SubModule
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Object obj;
    
    public string Name
    {
        get => name;
    }
    [SerializeField]
    public Object Obj
    {
        get => obj;
    }
}
