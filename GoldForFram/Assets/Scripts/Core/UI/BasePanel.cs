/*--------------------------------------
// 文件名称: BasePanel
// 创建者: CND
// 创建时间: 2023年03月24日 星期五 23:14
//======================================
// 功能描述:
//
//
//--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class BasePanel : MonoBehaviour
{
    public UnityAction showCallback = null;

    public LayerUi ui_layer = LayerUi.win;

    public UiModule _uiDic = null;

    protected virtual void Awake()
    {
        _uiDic = new UiModule();
        UIBehaviour[] allSub = GetComponentsInChildren<UIBehaviour>();
        foreach (var item in allSub)
        {
            string name = item.name.Split('_')[0];
            string type = string.IsNullOrEmpty(name) ? "" : name.ToLower();
            if (type == "btn" && item.GetType() == typeof(Button))
            {
                _uiDic.AddObj(item);
            }
            else if (type == "txt" && item.GetType() == typeof(Text))
            {
                _uiDic.AddObj(item);
            }
            else if (type == "img" && item.GetType() == typeof(Image))
            {
                _uiDic.AddObj(item);
            }
            else if (type == "obj")
            {
                _uiDic.AddObj(item);
            }
        }
    }

    protected virtual void Start()
    {
        UIManager.instance.curShowUi = name;
        
        Showed();

        if (showCallback != null)
        {
            showCallback();
        }
    }

    /// <summary>
    /// 显示完成
    /// </summary>
    public void Showed()
    {
    }

    /// <summary>
    /// 按钮事件注册简化方法
    /// </summary>
    /// <param name="btnName">按钮名</param>
    /// <param name="func">方法</param>
    public void ButtinEvent(string btnName, UnityAction func)
    {
        if (_uiDic.CheckKey(btnName))
        {
            _uiDic.GetModule<Button>(btnName).onClick.AddListener(func);
        }
        else
        {
            Debug.LogError($"这{btnName}键不存在");
        }
    }

    /// <summary>
    /// 移除按钮的所有事件监听
    /// </summary>
    /// <param name="btnName"></param>
    public void RemoveBtn(string btnName)
    {
        _uiDic.GetModule<Button>(btnName).onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 隐藏自身
    /// </summary>
    public void CloseSelf()
    {
        UIManager.instance.ColseUi(this.name);
    }

    /// <summary>
    /// 隐藏时触发
    /// </summary>
    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
        showCallback = null;
        _uiDic = null;
    }
}