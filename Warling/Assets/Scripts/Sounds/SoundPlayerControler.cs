using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayerControler : MonoBehaviour {

    public List<AudioClip> EffectUrl;
    private AudioSource audio;//
    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayJump(bool isShow)
    {
        if (isShow) audio.PlayOneShot(EffectUrl[0]);
        else audio.Stop();
    }
    public void PlayHurt(bool isShow)
    {
        if (isShow) audio.PlayOneShot(EffectUrl[1]);
        else audio.Stop();
    }
}
