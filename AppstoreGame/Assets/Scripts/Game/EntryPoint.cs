using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private LevelsController _levelController;
    [SerializeField] private TextMeshProUGUI _levelDisplay;
    [SerializeField] private Character _character;
    [SerializeField] private List<Sprite> _skins;
    [SerializeField] private SkinsController _skinController;
    [SerializeField] private List<LevelConfig> _configs;
    [SerializeField] private ScoresController _scores;
    [SerializeField] private PipesController _pipes;
    private void Start()
    {
        int currentLevel = _levelController.SelectedLevel;
        _levelDisplay.text = "LVL " + (currentLevel + 1).ToString();
        

        LevelConfig config = _configs[_levelController.SelectedLevel];
        _character.Initialize(_skins[(int)_skinController.SelectedSkin], config.Pipes.Count);
        _scores.Initialize(config.NeedScores);
        _pipes.Initialize(config.Pipes);
    }
}
