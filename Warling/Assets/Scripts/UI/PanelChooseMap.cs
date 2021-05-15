using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelChooseMap : MonoBehaviour {

	//???
	public rwdesenvOnline rw;

    private Animator anim;
    public Image[] _ListNumCoin;
    public Image[] _ListNumPlayer;
    public Sprite[] _ListSpriteMap;
    public Image _ImgMap;
    int _NumPlayer = 3;
    int _IndexMap = 0;
    int _IndexMapMax = 2;
    NumberTextScale _NumberText;
    GameControler _GameControler;
    UIManager _UIManager;
    SoundControler _SoundControler;

	// Use this for initialization
	void Start () {
        _NumberText = FindObjectOfType<NumberTextScale>();
        _GameControler = FindObjectOfType<GameControler>();
        _UIManager = FindObjectOfType<UIManager>();
        anim = gameObject.GetComponent<Animator>();
        _SoundControler = FindObjectOfType<SoundControler>();
        anim.enabled = false;
	}
    public void btnBack()
    {
        _SoundControler.PlayClickSound(true);
        anim.enabled = true;
        //play the SlideOut animation
        anim.Play("PanelChooseMapOut");
        //set back the time scale to normal time scale
     //   Time.timeScale = 1;
    }
    /// <summary>
    /// Chọn player tiếp theo
    /// </summary>
    public void NextPlayer()
    {
        _SoundControler.PlayClickSound(true);
        _NumPlayer++;
        if (_NumPlayer>5)
        {
            _NumPlayer = 1;
        }
        _NumberText.SetNumText1(_NumPlayer,_ListNumPlayer);
    }
    /// <summary>
    /// Lùi 1 player
    /// </summary>
    public void PrePlayer()
    {
        _SoundControler.PlayClickSound(true);
        _NumPlayer--;
        if (_NumPlayer < 1)
        {
            _NumPlayer = 5;
        }
        _NumberText.SetNumText1(_NumPlayer, _ListNumPlayer);
    }
    public void NextMap()
    {
        _SoundControler.PlayClickSound(true);
        _IndexMap++;
        if (_IndexMap > 6)
        {
            _IndexMap = 0;
        }
        if (_IndexMap<=_IndexMapMax)
        {
            _ImgMap.sprite = _ListSpriteMap[_IndexMap];
        }
        else _ImgMap.sprite = _ListSpriteMap[_IndexMap+4];
       
    }
    public void PreMap()
    {
        _SoundControler.PlayClickSound(true);
        _IndexMap--;
        if (_IndexMap < 0)
        {
            _IndexMap = 6;
        }
        if (_IndexMap <= _IndexMapMax)
        {
            _ImgMap.sprite = _ListSpriteMap[_IndexMap];
        }
        else _ImgMap.sprite = _ListSpriteMap[_IndexMap + 4];
        
    }
    public void LoadMapInfo()
    {
        _NumberText.SetNumText3(_GameControler._Gold,_ListNumCoin);
        //=========
        if (_GameControler._Gold>=100)
        {
            _IndexMapMax = 6;
        }
        else if (_GameControler._Gold >=50)
        {
            _IndexMapMax = 5;
        }
        else if (_GameControler._Gold >=25)
        {
            _IndexMapMax = 4;
        }
        else if (_GameControler._Gold >=10)
        {
            _IndexMapMax = 3;
        }
        else _IndexMapMax = 2;
    }
    /// <summary>
    /// Khi ấn vào nut bắt đầu chơi game
    /// </summary>
    public void StartGame()
    {
		//???
		//rw.isInternet();
		
		
        if (_IndexMap>_IndexMapMax) return;
        _SoundControler.PlayClickSound(true);
        Time.timeScale = 1;
        _GameControler._NumberPlayer = _NumPlayer;
        _GameControler._IndexMap = _IndexMap;
        _GameControler.StartGame();
        _UIManager.ShowPopopTurnGreen();
        _UIManager.HidePanelArmory();
        _UIManager.HidePanelChooseMapAndNumPlayer();
        _UIManager.HidePanelLevel();
        _UIManager.HidePanelLoadGame();
        _UIManager.HidePanelChoosePlayer();
        _UIManager.HidePanelStartGame();
    }
}
