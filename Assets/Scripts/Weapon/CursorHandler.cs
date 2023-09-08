using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public GameObject pauseCanvas;
    bool pause;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pause = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                PauseMenu();
            }

        }

    }

    private void PauseMenu()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        UnLockCursor();
        pause = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void UnLockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pause = false;
        LockCursor();
        pauseCanvas.SetActive(false);
    }
}
