/*--------------------------------------
// 文件名称: MainUi
// 创建者: CND
// 创建时间: 2023年03月24日 星期五 22:34
//======================================
// 功能描述:
//
//
//--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class MainUi : BasePanel
{
    private int day = 1;
    private int money = 999;

    protected override void Awake()
    {
        base.Awake();
        ui_layer = LayerUi.main;
    }

    private void Start()
    {
        _uiDic.GetModule<Button>("Btn_Farm").onClick.AddListener(() =>
        {
            Debug.Log("打开农场");
        });
        _uiDic.GetModule<Button>("Btn_Mine").onClick.AddListener(() =>
        {
            Debug.Log("打开矿场");
        });
        _uiDic.GetModule<Button>("Btn_Close").onClick.AddListener(() =>
        {
            //退出
            CloseSelf();
        });
        
        //显示天数
        _uiDic.GetModule<Text>("Txt_Day").text = $"天数:{day}";
        //显示金币
        _uiDic.GetModule<Text>("Txt_Gold").text = $"$: {money}";
    }
}
