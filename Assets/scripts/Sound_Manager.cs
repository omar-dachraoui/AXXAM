using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    //sound effects

     public static Sound_Manager Instance { get; set; }
     public AudioSource dropitemSound;
     public AudioSource pickupitemSound;
    public AudioSource swingSound;
    public AudioSource chopsound;
    public AudioSource walkSound;
    public AudioSource craftSound;
    public AudioSource eatSound;
    //music
    public AudioSource backgroundMusic;
    private void Awake()
    {
        // Singleton implementation: destroy duplicate instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
       
    }
     public void PlaySound(AudioSource soundtoPlay)
        {
            
            if(!soundtoPlay.isPlaying)
            {
                soundtoPlay.Play();
            }

        }






}
