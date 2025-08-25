using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioVolumeSlider : AudioDisplay, IPointerUpHandler
{
    [SerializeField] private Slider _slider;

    public override void Initialize(AudioController audioController)
    {
        base.Initialize(audioController);
        _slider.value = AudioController.Volume;
        _slider.onValueChanged.AddListener(OnSliderMoving);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderMoving);
    }

    public void OnSliderMoving(float value)
    {
        AudioController.ChangeVolume(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioController.SaveVolume();
    }
}