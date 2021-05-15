using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundControler : MonoBehaviour {


    public List<AudioClip> EffectUrl;
    private AudioSource audio;//
    GameControler _GameControler;
    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        _GameControler = FindObjectOfType<GameControler>();
    }

    public void PlayClickSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[0]);
        else audio.Stop();
    }
    public void PlayExplosionSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[1]);
        else audio.Stop();
    }
    public void PlayAircraftSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[2]);
        else audio.Stop();
    }
    public void PlayBazookaSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[3]);
        else audio.Stop();
    }
    public void PlayBounceSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[4]);
        else audio.Stop();
    }
    public void PlayDrillSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[5]);
        else audio.Stop();
    }
    public void PlayFireSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[6]);
        else audio.Stop();
    }
    public void PlayGlassSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[7]);
        else audio.Stop();
    }
    public void PlayLaserSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[8]);
        else audio.Stop();
    }
    public void PlayMinigun_shotSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) {
            audio.clip = EffectUrl[9];
            audio.Play();
            audio.loop = true;
        }
        else audio.Stop();
    }
    public void PlayPunchSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[10]);
        else audio.Stop();
    }
    public void PlayRollSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[11]);
        else audio.Stop();
    }
    public void PlayShieldSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[12]);
        else audio.Stop();
    }

    public void PlayShotgun_shotSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[13]);
        else audio.Stop();
    }
    public void PlayTeleportSound(bool isShow)
    {
        if (isShow && _GameControler._GameState._Sound) audio.PlayOneShot(EffectUrl[14]);
        else audio.Stop();
    }
}
