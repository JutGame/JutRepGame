using System;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static Action<int> ScoresAdded;
    [SerializeField] private TextMeshPro _number;
    public void ShowNumbers(Vector3 arrowPosition)
    {
        float distance = Mathf.Abs(arrowPosition.y);
        int amount = GetNumberByDistance(distance);

        _number.text = "+" + amount.ToString();
        _number.gameObject.SetActive(true);

        ScoresAdded?.Invoke(amount);
    }

    public int GetNumberByDistance(float distance)
    {
        if(distance <= 0.2f)
        {
            _number.color = Color.yellow;
            return 40;
        }
        else if(distance <= 0.62f)
        {
            _number.color = Color.red;
            return 30;
        }
        else if(distance <= 0.91f)
        {
            _number.color = Color.blue;
            return 20;
        }
        else
        {
            return 10;
        }
    }
}
