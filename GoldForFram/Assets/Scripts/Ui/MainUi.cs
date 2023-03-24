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

public class MainUi : MonoBehaviour
{
    private UiModule _uiDic;
    private int day = 1;
    private int money = 999;
    private void OnEnable()
    {
        _uiDic = GetComponent<UiModule>();
    }

    private void Start()
    {
        _uiDic.GetModule<Button>("FarmBtn").onClick.AddListener(() =>
        {
            Debug.Log("打开农场");
        });
        _uiDic.GetModule<Button>("MineBtn").onClick.AddListener(() =>
        {
            Debug.Log("打开矿场");
        });
        _uiDic.GetModule<Button>("CloseBtn").onClick.AddListener(() =>
        {
            //退出
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        
        //显示天数
        _uiDic.GetModule<Text>("DayText").text = $"天数:{day}";
        //显示金币
        _uiDic.GetModule<Text>("GoldText").text = $"$: {money}";
    }
}
