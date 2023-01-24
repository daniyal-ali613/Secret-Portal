using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    int enemyCounter;

    void Start()
    {
        enemyCounter = 0;

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void LoadMainMenuFromCredits()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Add(int counter)
    {
        enemyCounter += counter;
    }

    public void LoadCutScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SceneChange()
    {
        StartCoroutine(LoadBossScene());
    }

    IEnumerator LoadBossScene()
    {

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;

        yield return new WaitForSeconds(2);

        Camera.main.clearFlags = CameraClearFlags.Depth;
        SceneManager.LoadScene(5);

    }

}
