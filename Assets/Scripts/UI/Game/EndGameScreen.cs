using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private Button _continueButton;

    private void OnEnable()
    {
        _continueButton.Select();
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
    }

    public void FilleFields(string title, int score)
    {
        _title.text = title;
        string message = $"your score = {score}. {GenerateMessage(score)}";
        _message.text = message;
    }

    private string GenerateMessage(int score)
    {
        const int LittleResult = 45;
        const int AverageResult = 70;
        const string message1 = "The result is not the best, try again";
        const string message2 = "Not a bad result";
        const string message3 = "Wow don't forget it's just a game <3";
        
        if(score < LittleResult)
        {
            return message1;
        }
        else if(score < AverageResult)
        {
            return message2;
        }
        else
        {
            return message3;
        }
    }

    private void OnContinueButtonClicked()
    {
        MainMenu.Load();
    }
}
