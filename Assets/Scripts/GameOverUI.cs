using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
    }

    void Start()
    {
        scoreText.text = scoreKeeper.GetScore().ToString();
        highScoreText.text = scoreKeeper.GetHighScore().ToString();
    }
}
