using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIUpdater : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = ScoreKeeper.Instance;
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("D9"); // Display score with leading zeros
    }
}
