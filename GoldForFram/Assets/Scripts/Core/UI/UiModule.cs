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
using UnityEngine.U2D;
using UnityEngine.UI;
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
     /// 设置图片
     /// </summary>
     /// <param name="img"></param>
     /// <param name="path"></param>
     public void SetImage(string name, string path)
     {
            Image img = GetModule<Image>(name);
            if (img == null)
            {
                Debug.LogError($"这 {img.name} 为空");
            }
            int index = path.LastIndexOf('/');
            string imgName = path.Substring(index+1);
            string atlasPath = path.Remove(index);
            SpriteAtlas _atlas = XResourcesManager.instance.LoadSpriteAtlas(atlasPath);
            if (_atlas!=null)
            {
                Sprite _sprite = _atlas.GetSprite(imgName);
                if (_sprite!= null)
                {
                    img.sprite = _sprite; 
                }
                else
                {
                    Debug.LogError($"这 {path} 路径下没有图片");
                }
            }
            else
            {
                Debug.LogError($"这 {atlasPath} 路径下没有图集");
            }
        }
        
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="img"></param>
        /// <param name="path"></param>
        public void SetTexture(string name, string path)
        {
            RawImage img = GetModule<RawImage>(name);
            if (img == null)
            {
                Debug.LogError($"这 {img.name} 为空");
            }
    
            Texture _texture = XResourcesManager.instance.LoadTexture(path);
            if (_texture != null)
            {
                img.texture = _texture;
            }
            else
            {
                Debug.LogError($"这 {path} 路径下没有图片");
            }
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
