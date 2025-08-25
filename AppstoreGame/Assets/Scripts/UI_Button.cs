using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Play);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Play);
    }
    public void Play() => SFXController.Instance.Play(SoundType.Button);
}
