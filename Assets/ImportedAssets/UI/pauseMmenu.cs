using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMmenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            pause();
        }
    }
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void home()
    {
        
        
            
        
        SceneManager.LoadScene("MAINMENU");
        Time.timeScale =1.0f;

    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

    }
    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;

    }
    public void settings()
    {
        SceneManager.LoadScene("MAINMENU");
        Time.timeScale = 1.0f;


    }
}

