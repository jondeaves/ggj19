using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MusicState {
    Default = 0,
    BurglarWin = 1,
    DogWin = 2,
    Menu = 3,
}

public class MusicManager : MonoBehaviour
{
    public bool muteMusic = false;

    private static MusicManager instance = null;
    public static MusicManager Instance {
        get { return instance; }
    }

    [FMODUnity.EventRef]
    public string MusicEventFMOD;
    FMOD.Studio.EventInstance musicEventInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {   
        if (!GameData.isMusicPlaying)
        {
            musicEventInstance = FMODUnity.RuntimeManager.CreateInstance(MusicEventFMOD);

            if (!muteMusic)
            {
                musicEventInstance.start();
                GameData.isMusicPlaying = true;
            }
        }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetMusicState(MusicState.Menu);

            FMOD.Studio.PLAYBACK_STATE playbackState;
            musicEventInstance.getPlaybackState(out playbackState);

            Debug.Log(playbackState);

        }
    }

    public void SetMusicState(MusicState state)
    {   
        // convert state to float for FMOD
        float value = (float)(int)state;
        musicEventInstance.setParameterValue("winner", value);
    }
}
