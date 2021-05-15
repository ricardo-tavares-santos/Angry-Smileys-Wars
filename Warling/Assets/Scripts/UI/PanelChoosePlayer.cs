using UnityEngine;
using System.Collections;

public class PanelChoosePlayer : MonoBehaviour {

    public GameObject _PanelLevel;
    public GameObject _PanelChooseMap;
    public GameObject _PanelArmory;
    private Animator _animPanelLevel;
    private Animator _animPanelChooseMap;
    GameControler _GameControler;
    SoundControler _SoundControler;


    private Animator anim;//Anim của chính nó
	// Use this for initialization
	void Start () {
        _PanelLevel.SetActive(true);
        anim = gameObject.GetComponent<Animator>();
        _animPanelLevel = _PanelLevel.GetComponent<Animator>();
        _animPanelChooseMap = _PanelChooseMap.GetComponent<Animator>();
        _animPanelLevel.enabled = false;
        _animPanelChooseMap.enabled = false;
        anim.enabled = false;
        _SoundControler = FindObjectOfType<SoundControler>();
	}
    public void btnBack()
    {
        _SoundControler.PlayClickSound(true);
        anim.enabled = true;
        //play the SlideOut animation
        anim.Play("PanelChoosePlayerOut");
        //set back the time scale to normal time scale
       // Time.timeScale = 1;
    }
    public void CallPanelLevel()
    {
        _SoundControler.PlayClickSound(true);
        _GameControler = FindObjectOfType<GameControler>();
        _GameControler._TypeGame = 0;
        //enable the animator component
        _animPanelLevel.enabled = true;
        //play the Slidein animation
        _animPanelLevel.Play("PanelLevelIn");
        //freeze the timescale
       // Time.timeScale = 0;
    }
    public void CallPanelChooseMap()
    {
        _SoundControler.PlayClickSound(true);
        _GameControler = FindObjectOfType<GameControler>();
        _GameControler._TypeGame = 1;
        PanelChooseMap panel = _PanelChooseMap.GetComponent<PanelChooseMap>();
        panel.LoadMapInfo();
        _animPanelChooseMap.enabled = true;
        _animPanelChooseMap.Play("PanelChooseMapIn");
    }
    public void CallPanelArmory()
    {
        _SoundControler.PlayClickSound(true);
        _PanelArmory.SetActive(true);
        PanelArmory panel = _PanelArmory.GetComponent<PanelArmory>();
        panel.LoadPickMenu();
    }
}
