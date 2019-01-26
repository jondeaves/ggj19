using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicState {
    Default = 0,
    BurglarWin = 1,
    DogWin = 2
}

public class MusicManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string MusicEvent;
    FMOD.Studio.EventInstance music;

    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        music.start();
    }

    public void SetMusicState(MusicState state)
    {   
        // convert state to float
        float value = (float)(int)state;
        Debug.Log(value);
        music.setParameterValue("winner", value);
    }
}
