using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class QuizLoadScene : MonoBehaviour
{
    [SerializeField] private Button Quiz;


    void Start()
    {
        Quiz.onClick.AddListener(() => SceneManager.LoadScene("QuizScene"));
    }

   
}
