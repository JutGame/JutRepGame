using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class QuizSctript : MonoBehaviour
{
    [Header("Buttons (answers)")]
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button btn3;
    [SerializeField] private Button btn4;

    [Header("Question text")]
    [SerializeField] private TMP_Text textQuestion;

    [Header("Navigation")]
    [SerializeField] private Button next;
    [SerializeField] private Button back;

    [Header("Results Canvas")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMP_Text resultCountRightAnswer;
    [SerializeField] private Button MenuButton;

    [Header("Data source")]
    [SerializeField] private TextAsset quizJson; 

    [Header("Colors")]
    [SerializeField] private Color defaultButtonColor = Color.white;
    [SerializeField] private Color correctColor = new Color(0.35f, 0.8f, 0.35f);
    [SerializeField] private Color wrongColor = new Color(0.9f, 0.3f, 0.3f);

    private List<Question> questions = new List<Question>();
    private int currentIndex = 0;
    private int correctCount = 0;
    private bool questionAnswered = false;

    [Serializable]
    private class Question
    {
        public string question;
        public List<string> options;
        public string answer;
    }

    [Serializable]
    private class QuestionList
    {
        public List<Question> questions;
    }

    void Awake()
    {
        if (resultsPanel != null) resultsPanel.SetActive(false);

        back.onClick.AddListener(() => SceneManager.LoadScene("HomeScene"));

        btn1.onClick.AddListener(() => OnAnswerClick(0));
        btn2.onClick.AddListener(() => OnAnswerClick(1));
        btn3.onClick.AddListener(() => OnAnswerClick(2));
        btn4.onClick.AddListener(() => OnAnswerClick(3));

        if (next != null)
        {
            next.onClick.AddListener(OnNextClick);
            next.gameObject.SetActive(false); 
        }
    }

    void Start()
    {
        LoadQuestionsFromJson();
        ShowCurrentQuestion();
    }

    private void LoadQuestionsFromJson()
    {
        if (quizJson == null || string.IsNullOrWhiteSpace(quizJson.text))
        {
            Debug.LogError("Quiz JSON (TextAsset) is not assigned or empty.");
            return;
        }

        string raw = quizJson.text.Trim();
        if (raw.StartsWith("["))
        {
            raw = "{\"questions\":" + raw + "}";
        }

        try
        {
            var wrapped = JsonUtility.FromJson<QuestionList>(raw);
            if (wrapped != null && wrapped.questions != null && wrapped.questions.Count > 0)
            {
                questions = wrapped.questions;
            }
            else
            {
                Debug.LogError("Parsed questions list is empty.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to parse quiz JSON: " + e);
        }
    }

    private void ShowCurrentQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            textQuestion.text = "No questions loaded.";
            SetButtonsInteractable(false);
            return;
        }

        if (currentIndex >= questions.Count)
        {
            ShowResults();
            return;
        }

        var q = questions[currentIndex];

        textQuestion.text = q.question;

        ResetButtonVisual(btn1);
        ResetButtonVisual(btn2);
        ResetButtonVisual(btn3);
        ResetButtonVisual(btn4);
        SetButtonsInteractable(true);
        questionAnswered = false;

        if (next != null) next.gameObject.SetActive(false);

        SetButtonLabel(btn1, q.options[0]);
        SetButtonLabel(btn2, q.options[1]);
        SetButtonLabel(btn3, q.options[2]);
        SetButtonLabel(btn4, q.options[3]);
    }

    private void OnAnswerClick(int optionIndex)
    {
        if (questionAnswered) return;

        var q = questions[currentIndex];
        string selected = q.options[optionIndex];
        bool isCorrect = string.Equals(selected, q.answer, StringComparison.Ordinal);

        Button selectedBtn = GetButtonByIndex(optionIndex);
        if (selectedBtn != null)
        {
            PaintButton(selectedBtn, isCorrect ? correctColor : wrongColor);
        }

        if (!isCorrect)
        {
            int correctIdx = q.options.FindIndex(opt => string.Equals(opt, q.answer, StringComparison.Ordinal));
            Button correctBtn = GetButtonByIndex(correctIdx);
            if (correctBtn != null)
            {
                PaintButton(correctBtn, correctColor);
            }
        }
        else
        {
            correctCount++;
        }

        SetButtonsInteractable(false);
        questionAnswered = true;

        if (next != null) next.gameObject.SetActive(true);
    }

    private void OnNextClick()
    {
        if (!questionAnswered) return;

        currentIndex++;
        if (currentIndex >= questions.Count)
        {
            ShowResults();
        }
        else
        {
            ShowCurrentQuestion();
        }
    }

    private void ShowResults()
    {
        SetButtonsInteractable(false);
        if (next != null) next.gameObject.SetActive(false);

        if (resultsPanel != null) resultsPanel.SetActive(true);
        if (resultCountRightAnswer != null)
        {
            resultCountRightAnswer.text = $"{correctCount} / {questions.Count}";
        }

        MenuButton.onClick.AddListener(() => SceneManager.LoadScene("HomeScene"));
    }


    private void SetButtonsInteractable(bool value)
    {
        btn1.interactable = value;
        btn2.interactable = value;
        btn3.interactable = value;
        btn4.interactable = value;
    }

    private void ResetButtonVisual(Button btn)
    {
        if (btn == null) return;
        var img = btn.GetComponent<Image>();
        if (img != null) img.color = defaultButtonColor;
    }

    private void PaintButton(Button btn, Color color)
    {
        var img = btn.GetComponent<Image>();
        if (img != null) img.color = color;
    }

    private void SetButtonLabel(Button btn, string text)
    {
        if (btn == null) return;
        var label = btn.GetComponentInChildren<TMP_Text>(true);
        if (label != null) label.text = text;
    }

    private Button GetButtonByIndex(int idx)
    {
        switch (idx)
        {
            case 0: return btn1;
            case 1: return btn2;
            case 2: return btn3;
            case 3: return btn4;
            default: return null;
        }
    }
}
