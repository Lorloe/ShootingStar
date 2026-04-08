using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    private int currentScore = 0;

    public int GetScore()
    {
        return currentScore;
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
        // Debug.Log("Score: " + currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        // Debug.Log("Score reset to: " + currentScore);
    }
}
