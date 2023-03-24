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

public class LoginUi : MonoBehaviour
{
    private UiModule _uiDic;

    private void OnEnable()
    {
        _uiDic = GetComponent<UiModule>();
    }

    private void Start()
    {
        //按钮事件
        _uiDic.GetModule<Button>("StartBtn").onClick.AddListener(() =>
        {
            XResourcesManager.instance.LoadAsync<GameObject>("Ui/MainUi", (obj) =>
            {
                GameObject uiObj = Instantiate(obj as GameObject, GameConstant.UiRoot, false);
                uiObj.AddComponent<MainUi>();
                Destroy(gameObject);
            });
        });

        _uiDic.GetModule<Button>("QuitBtn").onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
        
        _uiDic.GetModule<Button>("SetBtn").onClick.AddListener(() =>
        {
            Debug.Log("打开设置面板");
        });
    }
}