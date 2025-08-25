using System.Collections;
using TMPro;
using UnityEngine;

public class ScoresController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _display;
    [SerializeField] private WinScreen _win;
    private int _scores;
    private int _needScores;

    public void Initialize(int needScores)
    {
        _scores = 0;
        _needScores = needScores;
        _display.text = $"{_scores}/{_needScores}";
        Target.ScoresAdded += AddScores;
    }

    private void OnDestroy()
    {
        Target.ScoresAdded -= AddScores;
    }

    public void AddScores(int add)
    {
        _scores += add;
        _display.text = $"{_scores}/{_needScores}";
        if(_scores >= _needScores)
        {
            _scores = _needScores;
            _display.text = $"{_scores}/{_needScores}";
            StartCoroutine(WinDelay());
        }
    }

    private IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _win.Show(_scores / 10);
    }
}
