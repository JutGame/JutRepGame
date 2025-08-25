using UnityEngine;
using UnityEngine.UI;

public class ExitGameBtn : MonoBehaviour
{
    [Header("Exit btn")]
    [SerializeField] private Button exit;

    void Start()
    {
        exit.onClick.AddListener(() => Application.Quit());
    }

   
}
