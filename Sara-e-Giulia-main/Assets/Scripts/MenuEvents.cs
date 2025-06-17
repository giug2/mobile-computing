using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuEvents : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;
    public Slider soundsSlider;
    public AudioMixer mixerS;
    private float valueS;

    private void Start()
    {
        mixer.GetFloat("volume", out value);
        volumeSlider.value = value;
        mixerS.GetFloat("sounds", out valueS);
        soundsSlider.value = valueS;
    }

    public void SetVolume()
    {
        mixer.SetFloat("volume", volumeSlider.value);
    }

    public void SetSounds()
    {
        mixerS.SetFloat("sounds", soundsSlider.value);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
