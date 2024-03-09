using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class controls : MonoBehaviour
{
    
    public void play()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void mute(bool mute)
    {
       
        if (mute)
        {
          AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume= 1;
        }
    }
    
}
