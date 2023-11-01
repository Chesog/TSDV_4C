using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private string _level1SceneName = "Test_Scene";
    [SerializeField] private string _creditsSceneName = "Credits_Scene";
    [SerializeField] private string _mainMenuSceneName = "Menu_Scene";
    [SerializeField] private string _optionsSceneName = "Options_Scene";
    [SerializeField] private string _leaderboardSceneName = "Leaderboard_Scene";

    #endregion

    #region PUBLIC_METHODS

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayLevel1()
    {
        LoadScene(_level1SceneName);
    }

    public void ShowCredits()
    {
        LoadScene(_creditsSceneName);
    }

    public void GoMainMenu()
    {
        LoadScene(_mainMenuSceneName);
    }

    public void GoOptions()
    {
        LoadScene(_optionsSceneName);
    }

    public void GoLeaderboard()
    {
        LoadScene(_leaderboardSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}