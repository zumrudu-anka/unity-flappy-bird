using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void setVolume(float value)
    {
        AudioListener.volume = value;
    }

}
