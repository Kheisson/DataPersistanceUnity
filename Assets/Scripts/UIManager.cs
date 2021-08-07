using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

[DefaultExecutionOrder(1000)]
public class UIManager : MonoBehaviour
{
    public InputField inputField;
    public Button startButton;
    public Text bestScore;

    private int score;
    private string playerName;
    private string bestScorePlayerName;

    private void Start()
    {
        try
        {
            DataManager.Instance.LoadScore();
            if (DataManager.Instance.GetPlayerName() != null)
            {
                playerName = DataManager.Instance.GetPlayerName();
                inputField.text = playerName;
            }
            if (DataManager.Instance.GetBestScore() != 0 &&
                DataManager.Instance.GetBestScorePlayerName() != null)
            {
                score = DataManager.Instance.GetBestScore();
                bestScorePlayerName = DataManager.Instance.GetBestScorePlayerName();
                bestScore.text = $"Best Score: {score}, Name: {bestScorePlayerName}";
            }
        } catch
        {
            Debug.Log("Not save data, will create new in game");
            DataManager.Instance.SetBestScore(0);
        }
    }

    private void Update()
    {
        if(inputField.text.Length == 0)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void StartGame()
    {
        DataManager.Instance.SetPlayerName(inputField.text);
        SceneManager.LoadScene(1);
    }

    public void QuitGame() =>
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

}
