using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private LevelsController _level;
    [SerializeField] private TextMeshProUGUI _display;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lock;
    private int _index;

    public void Initialize(int level, bool isOpen)
    {
        _button.interactable = isOpen;
        _index = level;
        _lock.gameObject.SetActive(!isOpen);
        _display.text = $"{_index + 1}";
    }

    public void LoadLevel()
    {
        _level.SetSelectedLevel(_index);
        _sceneSwitcher.SwitchScene("GameScene");
    }
}
