using System;
using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    private int currentScore = 0;
    private int highScore = 0;

    public event Action<int> ScoreChanged;

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void AddScore(int scoreToAdd)
    {
        int updatedScore = Mathf.Clamp(currentScore + scoreToAdd, 0, int.MaxValue);
        if (updatedScore == currentScore)
        {
            return;
        }

        currentScore = updatedScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
        }

        // ScoreChanged?.Invoke(currentScore);
        if (ScoreChanged != null)
        {
            ScoreChanged.Invoke(currentScore);
        }
    }

    public void ResetScore()
    {
        if (currentScore == 0)
        {
            return;
        }

        currentScore = 0;
        // ScoreChanged?.Invoke(currentScore);

        if (ScoreChanged != null)
        {
            ScoreChanged.Invoke(currentScore);
        }
    }
}
