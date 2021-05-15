﻿using UnityEngine;
using System.Collections;

public class PanelLevel : MonoBehaviour {
    public GameObject _PanelChooseMap;
    private Animator _animChooseMap;
    private Animator anim;//Anim của chính nó
    SoundControler _SoundControler;
	// Use this for initialization\
    void Awake()
    {
        _PanelChooseMap.SetActive(true);
        anim = gameObject.GetComponent<Animator>();
        anim.enabled = false;
        _animChooseMap = _PanelChooseMap.GetComponent<Animator>();
        _animChooseMap.enabled = false;
        _SoundControler = FindObjectOfType<SoundControler>();
    }
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void btnBack()
    {
        _SoundControler.PlayClickSound(true);
        anim.enabled = true;
        //play the SlideOut animation
        anim.Play("PanelLevelOut");
        //set back the time scale to normal time scale
       // Time.timeScale = 1;
    }
    public void CallPanelChooseMap(int level)
    {
        GameControler gameControler = FindObjectOfType<GameControler>();
        gameControler._Level = level;
        PanelChooseMap panel = _PanelChooseMap.GetComponent<PanelChooseMap>();
        panel.LoadMapInfo();
        _SoundControler.PlayClickSound(true);
        //enable the animator component
        _animChooseMap.enabled = true;
        //play the Slidein animation
        _animChooseMap.Play("PanelChooseMapIn");

        //freeze the timescale
       // Time.timeScale = 0;
    }
}
