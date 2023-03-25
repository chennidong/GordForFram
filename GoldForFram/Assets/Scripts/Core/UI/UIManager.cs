#region << 文件说明 >>

/*--------------------------------------
// 文件名称: UIManager
// 创建者: CND
// 创建时间: 2023年03月25日  星期六 20:51
//======================================
// 功能描述:
//
//
//--------------------------------------*/

#endregion

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

/// <summary>
/// ui层级类型
/// </summary>
public enum LayerUi
{
    main = 1, //主ui
    win = 2, //窗口
    pop = 3, //弹窗
}

public class UIManager
{
    private static UIManager _manager;

    public static UIManager instance
    {
        get
        {
            if (_manager == null)
            {
                _manager = new UIManager();
            }

            return _manager;
        }
    }

    private Dictionary<string, BasePanel> ui_show_list = null;

    private List<BasePanel> ui_hide_list = null;

    public string curShowUi;

    private UIManager()
    {
        ui_show_list = new Dictionary<string, BasePanel>();
        ui_hide_list = new List<BasePanel>();
    }

    /// <summary>
    /// 检查打开的ui层级
    /// </summary>
    /// <param name="panel"></param>
    private void CheckOpenUiLayer(BasePanel panel)
    {
        LayerUi _layer = panel.ui_layer;
        List<string> tempRemove = new List<string>();
        switch (_layer)
        {
            case LayerUi.main:
                foreach (var item in ui_show_list)
                {
                    if (item.Value.ui_layer == LayerUi.win)
                    {
                        ColseUi(item.Key);
                        tempRemove.Add(item.Key);
                    }
                    else if (item.Value.ui_layer == LayerUi.main)
                    {
                        HideUi(item.Key);
                    }
                }
                break;
            case LayerUi.win:
                foreach (var item in ui_show_list)
                {
                    if (item.Value.ui_layer == LayerUi.main)
                    {
                        HideUi(item.Key);
                    }
                    else if (item.Value.ui_layer == LayerUi.win)
                    {
                        ColseUi(item.Key);
                    }
                }

                break;
        }
        
        //删除显示表中的缓存界面
        foreach (var item in tempRemove)
        {
            ui_show_list.Remove(item);
        }
    }

    /// <summary>
    /// 关闭界面检查
    /// </summary>
    /// <param name="panel"></param>
    private void CheckCloseUiLayer(BasePanel panel)
    {
        LayerUi _layer = panel.ui_layer;
        switch (_layer)
        {
            case LayerUi.main:
                foreach (var item in ui_hide_list)
                {
                    item.gameObject.SetActive(true);
                }
                break;
            case LayerUi.win:
                foreach (var item in ui_hide_list)
                {
                    item.gameObject.SetActive(true);
                }
                ui_hide_list.Clear();
                break;
        }
    }

    /// <summary>
    /// 显示ui
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void ShowUi<T>(UnityAction func = null) where T : BasePanel
    {
        string name = typeof(T).Name;
        string path = GameConstant.UI_PATH + name;
        XResourcesManager.instance.LoadAsync<GameObject>(path, (obj) =>
        {
            GameObject uiObj = GameObject.Instantiate(obj as GameObject, GameConstant.UiRoot, false);
            T _ui = uiObj.AddComponent<T>();
            if (func != null)
            {
                _ui.showCallback += func;
            }

            //容错 判空
            if (ui_show_list == null)
            {
                ui_show_list = new Dictionary<string, BasePanel>();
            }

            //判断打开的界面是否与当前的界面互斥
            CheckOpenUiLayer(_ui);
            ui_show_list.Add(name, _ui);
        });
    }

    /// <summary>
    /// 获取当前显示的界面
    /// </summary>
    /// <returns></returns>
    public BasePanel GetCurShowUi()
    {
        if (string.IsNullOrEmpty(curShowUi))
        {
            foreach (var item in ui_show_list)
            {
                return item.Value;
            }
        }

        return ui_show_list[curShowUi];
    }

    /// <summary>
    /// 隐藏ui
    /// </summary>
    /// <param name="name"></param>
    public void HideUi(string name)
    {
        name = name.Replace("(Clone)", "");
        if (ui_show_list.ContainsKey(name))
        {
            ui_show_list[name].gameObject.SetActive(false);
            ui_hide_list.Add(ui_show_list[name]);
        }
    }

    /// <summary>
    /// 关闭ui
    /// </summary>
    /// <param name="name"></param>
    public void ColseUi(string name)
    {
        name = name.Replace("(Clone)", "");
        if (ui_show_list.ContainsKey(name))
        {
            CheckCloseUiLayer(ui_show_list[name]);
            GameObject.Destroy(ui_show_list[name].gameObject);
        }
    }
}