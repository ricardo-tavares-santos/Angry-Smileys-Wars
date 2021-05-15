using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelStartGame : MonoBehaviour {
    //refrence for the pause menu panel in the hierarchy
    public GameObject PanelChoosePlayer;
    public Image[] _ListButton;
    //animator reference
    private Animator anim;
    SoundControler _SoundControler;
    GameControler _GameControler;
    BackgoundSoundControler _BgSound;

    // Use this for initialization
	// Use this for initialization
    void Awake()
    {
        string sound = ReadWriteFileText.GetStringFromPrefab(Data._LinkSound);
        if (sound == "")
        {
            _GameControler = FindObjectOfType<GameControler>();
            _GameControler._GameState._Music = true;
            _GameControler._GameState._Sound = true;
        }
        else
        {
            _GameControler = FindObjectOfType<GameControler>();
            string[] arrSound = sound.Split('#');
            _GameControler._GameState._Music = bool.Parse(arrSound[0]);
            _GameControler._GameState._Sound = bool.Parse(arrSound[1]);
        }
        _GameControler = FindObjectOfType<GameControler>();
        //===========
        _GameControler._Gold= _GameControler.LoadGold();
    }
	void Start () {
        PanelChoosePlayer.SetActive(true);
        //get the animator component
        anim = PanelChoosePlayer.GetComponent<Animator>();
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
        Time.timeScale = 0;
        _SoundControler = FindObjectOfType<SoundControler>();
        _BgSound = FindObjectOfType<BackgoundSoundControler>();
        if (_GameControler._GameState._Music) _ListButton[0].color = Color.white;
        else _ListButton[0].color = Color.grey;
        if (_GameControler._GameState._Sound) _ListButton[1].color = Color.white;
        else _ListButton[1].color = Color.grey;
	}


    //function to pause the game
    public void CallPanelChoosePlayer()
    {
        _SoundControler.PlayClickSound(true);
        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("PanelChoosePlayerIn");
        //freeze the timescale
    }
    //function to unpause the game
    public void UnpauseGame()
    {
        _SoundControler.PlayClickSound(true);
        //play the SlideOut animation
        anim.Play("SlideOut");
    }
    public void btnMusic_Click()
    {
        if (_GameControler._GameState._Music)
        {
            _GameControler._GameState._Music = false;
            _BgSound.PlayBackground(false);
        }
        else
        {
            _GameControler._GameState._Music = true;
            _BgSound.PlayBackground(true);
        }
        ReadWriteFileText.SaveStringToPrefab(Data._LinkSound,_GameControler._GameState._Music + "#" + _GameControler._GameState._Sound);
        if (_GameControler._GameState._Music) _ListButton[0].color = Color.white;
        else _ListButton[0].color = Color.grey;
    }
    public void btnSound_Click()
    {
        if (_GameControler._GameState._Sound)
        {
            _GameControler._GameState._Sound = false;
        }
        else
        {
            _GameControler._GameState._Sound = true;
        }
        ReadWriteFileText.SaveStringToPrefab(Data._LinkSound, _GameControler._GameState._Music + "#" + _GameControler._GameState._Sound);
        if (_GameControler._GameState._Sound) _ListButton[1].color = Color.white;
        else _ListButton[1].color = Color.grey;
    }
}
