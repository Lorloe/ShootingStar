using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 1f; // Wait for the specified delay before loading the game scene
    
    const string GAME_SCENE = "GameScene";
    const string GAME_OVER_SCENE = "GameOverScene";
    const string MENU_SCENE = "MenuScene";

    public void LoadGameScene()
    {
        StartCoroutine(WaitAndLoad(GAME_SCENE, delayBeforeLoading));

        if (ScoreKeeper.Instance != null)
        {
            ScoreKeeper.Instance.ResetScore();
        }
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad(GAME_OVER_SCENE, delayBeforeLoading));
    }

    public void LoadMenuScene()
    {
        StartCoroutine(WaitAndLoad(MENU_SCENE, delayBeforeLoading));
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
