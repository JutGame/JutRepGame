using UnityEngine;

public class LevelsController : MonoBehaviour
{
    private int _maxLevel = 12;
    private string _selectedLevelKey = "SelectedLevel111";
    private string _lastLevelKey = "LastLevel111";

    public int MaxLevel => _maxLevel;
    public int SelectedLevel => PlayerPrefs.GetInt(_selectedLevelKey, 0);
    public int LastLevel => PlayerPrefs.GetInt(_lastLevelKey, 0);

    private void Awake()
    {
        Debug.Log($"Selected level: {SelectedLevel}");
        Debug.Log($"Last level: {LastLevel}");
    }

    public void SetSelectedLevel(int level)
    {
        if (level > _maxLevel - 1)
            return;
        PlayerPrefs.SetInt(_selectedLevelKey, level);
    }

    public void OpenLevel()
    {
        if(LastLevel < _maxLevel && SelectedLevel == LastLevel)
        {
            PlayerPrefs.SetInt(_lastLevelKey, LastLevel + 1);
        }
    }
}
