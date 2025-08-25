using TMPro;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _display;
    private int _value;
    private string _key = "CoinsFFF";

    public int Value => _value;

    private void Awake()
    {
        _value = PlayerPrefs.GetInt(_key, 0);
        RefreshDisplay();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(_key, _value);
    }

    public bool TryRemoveValue(int removed)
    {
        if (removed > _value)
            return false;
        _value -= removed;
        RefreshDisplay();
        return true;
    }

    public void AddValue(int value)
    {
        _value += value;
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        if (_display == null)
            return;
        _display.text = _value.ToString();
    }
}
