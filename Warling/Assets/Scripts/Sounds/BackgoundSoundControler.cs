using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgoundSoundControler : MonoBehaviour {
   
    public List<AudioClip> EffectUrl;
    private AudioSource audio;//
    GameControler _GameControler;
	// Use this for initialization
	void Awake () {
        audio = GetComponent<AudioSource>();
        _GameControler = FindObjectOfType<GameControler>();
	}

    public void PlayBackground(bool isShow)
    {
        _GameControler = FindObjectOfType<GameControler>();
        if (isShow && _GameControler._GameState._Music) { audio.Play();
        audio.loop = true;
        }
        else audio.Stop();
       
    }
}
