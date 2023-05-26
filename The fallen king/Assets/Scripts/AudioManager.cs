using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer music, effects;
    public AudioSource player1, player2, player3, playershield, cofre, skelwalk, skelhit;
    public AudioSource getitem, skelsword, skellancer, skelbutcher;
    public static AudioManager instance;

    void Start()
    {
       if(instance == null)
       {
            instance = this;
       }
    }

    
    void Update()
    {
        
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
