using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [HideInInspector] public VideoPlayer videoPlayer;
    [SerializeField] GameObject Panel;
    [SerializeField] private GameObject levelsPanel;
    [HideInInspector] public Audio _audioManager;


    private void Start()
    {

        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
    }


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        _audioManager.PlaySFX(_audioManager.ButtonClick);

    }

    public void Setting()
    {
        Panel.SetActive(true);
        _audioManager.PlaySFX(_audioManager.IconClick);


    }

    public void BackToMenu()
    {
        Panel.SetActive(false);
        levelsPanel.SetActive(false);
        _audioManager.PlaySFX(_audioManager.IconClick);


    }

    public void PauseGame()
    {
        Panel.SetActive(true);
        _audioManager.PlaySFX(_audioManager.IconClick);
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
        _audioManager.PlaySFX(_audioManager.ButtonClick);
        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _audioManager.PlaySFX(_audioManager.ButtonClick);
        
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        _audioManager.PlaySFX(_audioManager.ButtonClick);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _audioManager.PlaySFX(_audioManager.ButtonClick);
    }
    
    public void Levels()
    {
        levelsPanel.SetActive(true);
        _audioManager.PlaySFX(_audioManager.IconClick);
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Level2()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Level4()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Level5()
    {
        SceneManager.LoadSceneAsync(5);
    }
}
