using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    [SerializeField] Text scoreText;
    [SerializeField] Text hiScoreText;

    [SerializeField] int score;
    [SerializeField] int hiScore;

    private void OnEnable()
    {
        EventManager.onStartGame += ResetScore;
        EventManager.onStartGame += LoadHiScore;
        EventManager.onPlayerDeath += CheckNewHiScore;
        EventManager.onScorePoints += AddScore;
    }

    private void OnDisable()
    {
        EventManager.onStartGame -= ResetScore;
        EventManager.onStartGame -= LoadHiScore;
        EventManager.onPlayerDeath -= CheckNewHiScore;
        EventManager.onScorePoints -= AddScore;
    }

    void ResetScore()
    {
        score = 0;
        DisplayScore();
        LoadHiScore();
        DisplayHiScore();
        CheckNewHiScore();
    }

    void AddScore(int amt)
    {
        score += amt;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }

    void LoadHiScore()
    {
        hiScore = PlayerPrefs.GetInt("hiScore", 0);
    }

    void CheckNewHiScore()
    {
        if(score > hiScore)
        {
            PlayerPrefs.SetInt("hiScore", score);
            DisplayHiScore();
        }
    }

    void DisplayHiScore()
    {
        hiScoreText.text = hiScore.ToString();
    }
}
