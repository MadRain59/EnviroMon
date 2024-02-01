using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Subscribe to the sceneLoaded event
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from the sceneLoaded event
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
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

    void Resume()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1.0f;
            GameIsPaused = false;
            pauseMenuUI.SetActive(false);
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);

        Debug.Log("Game Has Paused");
    }

    public void Lobby()
    {
        LoadLevelLobby("Lobby(v0.01)");
    }

    public void LoadLevelLobby(string lobby)
    {
        // Ensure the time scale is set back to 1 before loading the new scene
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(lobby);
    }

    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the lobby and pause if necessary
        if (scene.name == "Lobby(v0.01)")
        {
            if (GameIsPaused)
            {
                Pause();
            }
        }
    }
}

