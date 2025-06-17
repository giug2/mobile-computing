using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour
{
	public AudioSource myAudio;
    public AudioClip clickAudio;

    public void Click() {
        myAudio.PlayOneShot(clickAudio);
    }
}
