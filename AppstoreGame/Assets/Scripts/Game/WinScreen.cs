using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _monetDisplay;
    [SerializeField] private CoinsController _coins;
    [SerializeField] private LevelsController _levelsController;
    [SerializeField] private SceneSwitcher _scene;
    public void Show(int coins)
    {
        gameObject.SetActive(true);
        _coins.AddValue(coins);
        _monetDisplay.text = coins.ToString();
        _levelsController.OpenLevel();
    }

    public void GoNext()
    {
        _levelsController.SetSelectedLevel(_levelsController.SelectedLevel + 1);
        _scene.SwitchScene("GameScene");
    }
}
