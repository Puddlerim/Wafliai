using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private FirstPersonLook lookScript;

    void Start()
    {
        // Hide pause menu at start
        pauseMenuUI.SetActive(false);

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Make sure game runs normally
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Find camera look script
        lookScript = FindObjectOfType<FirstPersonLook>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Hide pause UI
        pauseMenuUI.SetActive(false);

        // Resume game
        Time.timeScale = 1f;
        GameIsPaused = false;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume camera look
        if (lookScript != null)
        {
            lookScript.ResumeLook();
        }
    }

    void Pause()
    {
        // Show pause UI
        pauseMenuUI.SetActive(true);

        // Pause game
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Freeze camera look
        if (lookScript != null)
        {
            lookScript.FreezeLook();
        }
    }

    public void LoadMenu()
    {
        // Prevent frozen time in next scene
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}