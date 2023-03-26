using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// 设置面板界面
/// </summary>
public class SetUi : BasePanel
{
    private int curVolume = 100;
    private Slider _slider;
    private Text _volume;

    protected override void Awake()
    {
        base.Awake();
        ui_layer = LayerUi.pop;
        curVolume = PlayerPrefs.GetInt(GameConstant.VOLUME_KEY, 100);
    }

    protected override void Start()
    {
        _slider = _uiDic.GetModule<Slider>("Slider_Volume");
        _volume = _uiDic.GetModule<Text>("txt_Volume");
        _slider.value = curVolume;
        _volume.text = $"音量: {curVolume}";
        
        base.Start();
    }

    public override void Showed()
    {
        _slider.onValueChanged.AddListener(delegate(float value)
        {
            curVolume = (int) value;
            _volume.text = $"音量: {curVolume}";
        });
        ButtinEvent("btn_Bg", () => { CloseSelf(); });

        _uiDic.SetTexture("Img_bg", "Texture/box/box_select2");
    }

    protected override void OnDestroy()
    {
        //关闭界面时保存
        PlayerPrefs.SetInt(GameConstant.VOLUME_KEY, curVolume);
        PlayerPrefs.Save();
        base.OnDestroy();
    }
}