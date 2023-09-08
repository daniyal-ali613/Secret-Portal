using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    int enemyCounter;
    bool allDead;

    void Start()
    {
        allDead = false;
    }
    void Update()
    {
        if (allDead)
        {
            StartCoroutine(LoadBossScene());
        }
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

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            allDead = true;
        }
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

    public void Quit()
    {
        Application.Quit();
    }


    public void LoadBoss()
    {
        SceneManager.LoadScene(5);
    }

    private void SceneChange()
    {
        StartCoroutine(LoadBossScene());
    }

    IEnumerator LoadBossScene()
    {

        yield return new WaitForSeconds(3);
        LoadBoss();
    }

    public bool GetDeathStatus()
    {
        return allDead;
    }

}
