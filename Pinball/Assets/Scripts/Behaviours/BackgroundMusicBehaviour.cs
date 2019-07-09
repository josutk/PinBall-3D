using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicBehaviour : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClip;
    public bool paused = false;
    private SignalHandlerScript signalHandler;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        signalHandler = Finder.GetSignalHandler();
        if (musicSource == null) musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.volume = 0.5F;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (signalHandler.buttons.select && !paused)
        {
            musicSource.Pause();
            paused = true;
        }else if (signalHandler.buttons.select && paused)
        {
            musicSource.UnPause();
            paused = false;
        }
    }
}
