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

    void OnEnable()
    {
        scoreKeeper = ScoreKeeper.Instance;
        SubscribeToEvents();
        SyncUI();
    }

    void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged += RefreshHealthUI;
        }

        if (scoreKeeper != null)
        {
            scoreKeeper.ScoreChanged += RefreshScoreUI;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= RefreshHealthUI;
        }

        if (scoreKeeper != null)
        {
            scoreKeeper.ScoreChanged -= RefreshScoreUI;
        }
    }

    private void SyncUI()
    {
        if (playerHealth != null && healthSlider != null)
        {
            healthSlider.maxValue = playerHealth.GetMaxHealth();
            RefreshHealthUI(playerHealth.GetHealth());
        }

        if (scoreKeeper != null)
        {
            RefreshScoreUI(scoreKeeper.GetScore());
        }
    }

    private void RefreshHealthUI(int currentHealth)
    {
        if (healthSlider == null)
        {
            return;
        }

        healthSlider.value = currentHealth;
    }

    private void RefreshScoreUI(int currentScore)
    {
        if (scoreText == null)
        {
            return;
        }

        scoreText.text = currentScore.ToString("D9");
    }
}
