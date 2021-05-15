using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelArmory : MonoBehaviour {

    public Sprite[] _ListSpriteButton;
    public Image[] _ListButton;
    public Image[] _listGoldImage;
    SoundControler _SoundControler;
    GameControler _GameControler;
    NumberTextScale _NumberText;

	// Use this for initialization
	void Start () {
        _SoundControler = FindObjectOfType<SoundControler>();
        _GameControler = FindObjectOfType<GameControler>();
 
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void btnBack()
    {
        _SoundControler.PlayClickSound(true);
        gameObject.SetActive(false);
    }
    public void LoadPickMenu()
    {
        _NumberText = FindObjectOfType<NumberTextScale>();
        _GameControler = FindObjectOfType<GameControler>();
        _NumberText.SetNumText3(_GameControler._Gold, _listGoldImage);
        //============
        if (_GameControler._Gold>=1)
        {
            _ListButton[4].sprite = _ListSpriteButton[8];
            _ListButton[7].sprite = _ListSpriteButton[14];
        }
        if (_GameControler._Gold >= 3) { _ListButton[8].sprite = _ListSpriteButton[16]; }
        if (_GameControler._Gold >= 7) { _ListButton[9].sprite = _ListSpriteButton[18]; }
        if (_GameControler._Gold >= 12) { _ListButton[10].sprite = _ListSpriteButton[20]; }
        if (_GameControler._Gold >= 18) { _ListButton[11].sprite = _ListSpriteButton[22]; }
        if (_GameControler._Gold >= 24) { _ListButton[12].sprite = _ListSpriteButton[24]; }
        if (_GameControler._Gold >= 32) { _ListButton[13].sprite = _ListSpriteButton[26]; }
        if (_GameControler._Gold >= 45) { _ListButton[14].sprite = _ListSpriteButton[28]; }
        if (_GameControler._Gold >= 60) { _ListButton[15].sprite = _ListSpriteButton[30]; }
        if (_GameControler._Gold >= 90) { _ListButton[16].sprite = _ListSpriteButton[32]; }
    }
}
