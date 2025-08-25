using UnityEngine;

public class PlayLastButton : MonoBehaviour
{
    [SerializeField] private LevelsController _levelController;
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    public void OpenLastLevel()
    {
        _levelController.SetSelectedLevel(_levelController.LastLevel);
        _sceneSwitcher.SwitchScene("GameScene");
    }
}
