﻿/*--------------------------------------
// 文件名称: GameManager
// 创建者: CND
// 创建时间: 2023年03月19日 星期日 15:23
//======================================
// 功能描述:
//
//
//--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 游戏整体流程的总控制
/// </summary>
public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        //切场景不销毁
        DontDestroyOnLoad(this);
        GameConstant.UiRoot = GameObject.FindWithTag("UiRoot").transform;
    }

    private void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// 项目入口 初始化
    /// </summary>
    private void Init()
    {
        //初始需要初始化的控制器

        //初始项目的初始数据


        //加载登录界面
        LoadLoginUi();
    }
    /// <summary>
    /// 登录界面
    /// </summary>
    private void LoadLoginUi()
    {
        UIManager.instance.ShowUi<LoginUi>();
    }
}