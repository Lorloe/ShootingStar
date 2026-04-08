using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 1f; // Wait for the specified delay before loading the game scene
    
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = ScoreKeeper.Instance;
    }

    public void LoadGameScene()
    {
        // SceneManager.LoadScene("GameScene");
        StartCoroutine(WaitAndLoad("GameScene", delayBeforeLoading));

        if (scoreKeeper != null)
        {
            scoreKeeper.ResetScore();
        }
    }

    public void LoadGameOverScene()
    {
        // SceneManager.LoadScene("GameOverScene");
        StartCoroutine(WaitAndLoad("GameOverScene", delayBeforeLoading));
    }

    public void LoadMenuScene()
    {
        // SceneManager.LoadScene("MenuScene");
        StartCoroutine(WaitAndLoad("MenuScene", delayBeforeLoading));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    private IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
