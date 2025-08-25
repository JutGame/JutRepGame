using UnityEngine;

public class LevelsScreen : MonoBehaviour
{
    [SerializeField] private LevelButton[] _levels;
    [SerializeField] private LevelsController _controller;

    private void Start()
    {
        for(int i = 0; i < _levels.Length; i++)
        {
            _levels[i].Initialize(i, i <= _controller.LastLevel);
        }
    }
}
