using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    private int currentScore = 0;
    private int highScore = 0;

    public int GetScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
        return highScore;
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
