/*--------------------------------------
// 文件名称: LoginUi
// 创建者: CND
// 创建时间: 2023年03月19日 星期日 22:40
//======================================
// 功能描述:
//
//
//--------------------------------------*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginUi : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
        ui_layer = LayerUi.win;
    }

    private void Start()
    {
        ButtinEvent("Btn_Start",() =>
        {
            UIManager.instance.ShowUi<MainUi>();
        });
        ButtinEvent("Btn_Quit", () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        
        ButtinEvent("Btn_Set", () =>
        {
            Debug.Log("打开设置面板");
        });
    }
}