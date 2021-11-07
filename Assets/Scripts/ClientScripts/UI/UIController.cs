using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject miniMenu, settingsMenu;
    public static bool isOnMenu;

    private CursorLockMode cursorLock;

    private void Update()
    {
        if(miniMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (miniMenu.activeSelf)
                    CloseMenu();
                else
                    OpenMenu();
        }
    }

    public void OpenMenu()
    {
        cursorLock = Cursor.lockState;
        Cursor.lockState = CursorLockMode.Confined;
        isOnMenu = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        miniMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        isOnMenu = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = cursorLock;
        settingsMenu.SetActive(false);
        miniMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        CloseMenu();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
