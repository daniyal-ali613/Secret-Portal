using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject pauseMenu;

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
