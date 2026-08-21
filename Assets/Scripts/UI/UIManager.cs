using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Top UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Lives")]
    [SerializeField] private Image[] heartImages;

    [Header("Result Panel")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text finalScoreText;

    private void Start()
    {
        resultPanel.SetActive(false);

        UpdateScore(0);
        UpdateLives(5);
        UpdateTimer(30);
    }

    public void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);

        timerText.text = "TIME: " + seconds;

        // Make timer pulse when time is low
        if (seconds <= 5)
        {
            timerText.transform.localScale =
                Vector3.one * 1.1f;
        }
        else
        {
            timerText.transform.localScale =
                Vector3.one;
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Diamonds " + score;
    }

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < lives;
        }
    }

    public void ShowWin(int score)
    {
        resultPanel.SetActive(true);

        resultText.text = "YOU WIN!";
        finalScoreText.text =
            "Diamonds Collected: " + score;

        resultText.transform.localScale = Vector3.zero;
    }

    public void ShowGameOver(int score)
    {
        resultPanel.SetActive(true);

        resultText.text = "GAME OVER";
        finalScoreText.text =
            "Diamonds Collected: " + score;

        resultText.transform.localScale = Vector3.one;
    }
}