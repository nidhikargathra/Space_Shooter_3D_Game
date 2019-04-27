using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    [SerializeField] Text scoreText;
    [SerializeField] Text hiScoreText;
    [SerializeField] Text orbScoreText;

    [SerializeField] int score;
    [SerializeField] int hiScore;
    [SerializeField] int totalNumberOfOrbs;

    public int numberOfOrbsCollected;

    private void OnEnable()
    {
        EventManager.onStartGame += ResetScore;
        EventManager.onStartGame += LoadHiScore;
        EventManager.onPlayerDeath += CheckNewHiScore;
        EventManager.onScorePoints += AddScore;
        EventManager.onCollectOrb += CollectOrb;
    }

    private void OnDisable()
    {
        EventManager.onStartGame -= ResetScore;
        EventManager.onStartGame -= LoadHiScore;
        EventManager.onPlayerDeath -= CheckNewHiScore;
        EventManager.onScorePoints -= AddScore;
        EventManager.onCollectOrb -= CollectOrb;
    }

    void ResetScore()
    {
        numberOfOrbsCollected = 0;
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

    void CollectOrb()
    {
        numberOfOrbsCollected++;
        orbScoreText.text = "Orbs Collected: " + numberOfOrbsCollected.ToString() +"/"+ totalNumberOfOrbs.ToString();
        if(numberOfOrbsCollected == totalNumberOfOrbs)
        {
            print("all orbs collected");
        }
    }
}
