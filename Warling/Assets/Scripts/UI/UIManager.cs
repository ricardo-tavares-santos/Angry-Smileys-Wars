﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//???
using EasyMobile;

public class UIManager : MonoBehaviour {

    public GameObject[] _ListPopup;
    public Sprite[] _ListPickSprite;
    public Sprite[] _ListAmmoSprite;
    public Image[] _ListImgButtonPopupPick;
    public Image[] _ListImgAmmo;
    public Sprite[] _ListSpriteImgButtonPopupPick;
    public Image[] _ListImgChoosePick;
    public GameObject[] _ListText;
    GameControler _GameControler;
    GameManager _GameManager;
    CameraControler _cameraControler;
    MapControler _mapControler;
    SoundControler _SoundControler;
	
	
	//??? 
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }		
	
	
	// Use this for initialization
    void Start()
    {
        _GameControler = FindObjectOfType<GameControler>();
        _GameManager = FindObjectOfType<GameManager>();
        _cameraControler = FindObjectOfType<CameraControler>();
        _SoundControler = FindObjectOfType<SoundControler>();
    }
    public void ShowPopupPick()
    {
        _SoundControler.PlayClickSound(true);
        if (!_ListPopup[0].activeSelf && !_GameControler._CheckHit)
        {
            if (_GameControler._TypeGame==0)
            {
                if (!_GameControler._GameState._IsPlayerStart) return;
            }
            else
            {
                if (!_GameControler._GameState._IsPlayerStart && !_GameControler._GameState._IsEnemyStart) return;
            }
            _ListPopup[0].SetActive(true);
            LoadDataPopupPick();
        }
        else
        {
            _ListPopup[0].SetActive(false);
        }
       
    }
    /// <summary>
    /// Reset lại các Image button sau khi đã bắn đạn
    /// </summary>
    public void ResetImgPick()
    {
        _ListImgChoosePick[0].sprite = _ListPickSprite[4];
        _ListImgChoosePick[1].sprite = _ListPickSprite[3];
        _ListText[1].SetActive(false);
        _ListImgChoosePick[2].sprite = _ListPickSprite[1];
    }
    /// <summary>
    /// Reset lại button về biểu tượng cho phép chọn súng
    /// </summary>
    public void SetImgButtonChoosePick()
    {
        _GameControler._CheckHit = false;
        _ListImgChoosePick[0].sprite = _ListPickSprite[4];
        _ListImgChoosePick[1].sprite = _ListPickSprite[3];
        _ListText[1].SetActive(false);
        _ListImgChoosePick[2].sprite = _ListPickSprite[0];
        
    }
    private void HidePopupPick()
    {
        _ListPopup[0].SetActive(false);
    }
    /// <summary>
    /// Load súng đã được unlock
    /// </summary>
    public void LoadDataPopupPick()
    {
        if (_GameControler._GameObj.tag=="Player")
        {
            if (_GameControler._ListStatePickPlayer[0].State) _ListImgButtonPopupPick[0].sprite = _ListSpriteImgButtonPopupPick[0]; else _ListImgButtonPopupPick[0].sprite = _ListSpriteImgButtonPopupPick[1];
            if (_GameControler._ListStatePickPlayer[1].State) _ListImgButtonPopupPick[1].sprite = _ListSpriteImgButtonPopupPick[2]; else _ListImgButtonPopupPick[1].sprite = _ListSpriteImgButtonPopupPick[3];
            if (_GameControler._ListStatePickPlayer[2].State) _ListImgButtonPopupPick[2].sprite = _ListSpriteImgButtonPopupPick[4]; else _ListImgButtonPopupPick[2].sprite = _ListSpriteImgButtonPopupPick[5];
            if (_GameControler._ListStatePickPlayer[3].State)
                if (_GameControler._ListStatePickPlayer[3].Ammo > 0)
                {
                    _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[6];
                    _ListImgAmmo[3].enabled = true;
                }
                else
                {
                    _GameControler._ListStatePickPlayer[3].State = false;
                    _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[7];
                    _ListImgAmmo[3].enabled = false;
                }
            else
            {
                _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[7];
                _ListImgAmmo[3].enabled = false;
            }

            if (_GameControler._ListStatePickPlayer[4].State)
            {
                if (_GameControler._ListStatePickPlayer[4].Ammo > 0)
                {
                    _ListImgAmmo[4].enabled = true;
                    _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[8];
                    _ListImgAmmo[4].sprite = _ListAmmoSprite[_GameControler._ListStatePickPlayer[4].Ammo - 1];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[4].State = false;
                    _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[9];
                    _ListImgAmmo[4].enabled = false;
                }
            }
            else { _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[9];
            _ListImgAmmo[4].enabled = false;
            }
            if (_GameControler._ListStatePickPlayer[5].State)
            {
                if (_GameControler._ListStatePickPlayer[5].Ammo > 0)
                {
                    _ListImgAmmo[5].enabled = true;
                    _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[10];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[5].State = false;
                    _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[11];
                    _ListImgAmmo[5].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[11]; _ListImgAmmo[5].enabled = false; }
            if (_GameControler._ListStatePickPlayer[6].State)
            {
                if (_GameControler._ListStatePickPlayer[6].Ammo > 0)
                {
                    _ListImgAmmo[6].enabled = true;
                    _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[12];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[6].State = false;
                    _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[13];
                    _ListImgAmmo[6].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[13]; _ListImgAmmo[6].enabled = false; }
            if (_GameControler._ListStatePickPlayer[7].State)
            {
                if (_GameControler._ListStatePickPlayer[7].Ammo > 0)
                {
                    _ListImgAmmo[7].enabled = true;
                    _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[14];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[7].State = false;
                    _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[15];
                    _ListImgAmmo[7].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[15]; _ListImgAmmo[7].enabled = false; }
            if (_GameControler._ListStatePickPlayer[8].State)
            {
                if (_GameControler._ListStatePickPlayer[8].Ammo > 0)
                {
                    _ListImgAmmo[8].enabled = true;
                    _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[16];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[8].State = false;
                    _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[17];
                    _ListImgAmmo[8].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[17]; _ListImgAmmo[8].enabled = false; }
            if (_GameControler._ListStatePickPlayer[9].State)
            {
                if (_GameControler._ListStatePickPlayer[9].Ammo > 0)
                {
                    _ListImgAmmo[9].enabled = true;
                    _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[18];
                    _ListImgAmmo[9].sprite = _ListAmmoSprite[_GameControler._ListStatePickPlayer[9].Ammo - 1];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[9].State = false;
                    _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[19];
                    _ListImgAmmo[9].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[19]; _ListImgAmmo[9].enabled = false; }
            if (_GameControler._ListStatePickPlayer[10].State)
            {
                if (_GameControler._ListStatePickPlayer[10].Ammo > 0)
                {
                    _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[20];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[10].State = false;
                    _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[21];
                }
            }
            else { _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[21]; }
            if (_GameControler._ListStatePickPlayer[11].State){
                if (_GameControler._ListStatePickPlayer[11].Ammo > 0)
                {
                    _ListImgAmmo[11].enabled = true;
                    _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[22];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[11].State = false;
                    _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[23];
                    _ListImgAmmo[11].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[23]; _ListImgAmmo[11].enabled = false; }
            if (_GameControler._ListStatePickPlayer[12].State)
            {
                if (_GameControler._ListStatePickPlayer[12].Ammo > 0)
                {
                    _ListImgAmmo[12].enabled = true;
                    _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[24];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[12].State = false;
                    _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[25];
                    _ListImgAmmo[12].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[25]; _ListImgAmmo[12].enabled = false; }
            if (_GameControler._ListStatePickPlayer[13].State)
            {
                if (_GameControler._ListStatePickPlayer[13].Ammo > 0)
                {
                    _ListImgAmmo[13].enabled = true;
                    _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[26];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[13].State = false;
                    _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[27];
                    _ListImgAmmo[13].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[27]; _ListImgAmmo[13].enabled = false; }
            if (_GameControler._ListStatePickPlayer[14].State)
            {
                if (_GameControler._ListStatePickPlayer[14].Ammo > 0)
                {
                    _ListImgAmmo[14].enabled = true;
                    _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[28];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[14].State = false;
                    _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[29];
                    _ListImgAmmo[14].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[29]; _ListImgAmmo[14].enabled = false; }
            if (_GameControler._ListStatePickPlayer[15].State)
            {
                if (_GameControler._ListStatePickPlayer[15].Ammo > 0)
                {
                    _ListImgAmmo[15].enabled = true;
                    _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[30];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[15].State = false;
                    _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[31];
                    _ListImgAmmo[15].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[31]; _ListImgAmmo[15].enabled = false; }
            if (_GameControler._ListStatePickPlayer[16].State)
            {
                if (_GameControler._ListStatePickPlayer[16].Ammo > 0)
                {
                    _ListImgAmmo[16].enabled = true;
                    _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[32];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[16].State = false;
                    _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[33];
                    _ListImgAmmo[16].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[33]; _ListImgAmmo[16].enabled = false; }
            if (_GameControler._ListStatePickPlayer[17].State)
            {
                if (_GameControler._ListStatePickPlayer[17].Ammo > 0)
                {
                    _ListImgAmmo[17].enabled = true;
                    _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[34];
                }
                else
                {
                    _GameControler._ListStatePickPlayer[17].State = false;
                    _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[35];
                    _ListImgAmmo[17].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[35]; _ListImgAmmo[17].enabled = false; }
        }
        else
        {
            if (_GameControler._ListStatePickEnemy[0].State) _ListImgButtonPopupPick[0].sprite = _ListSpriteImgButtonPopupPick[0]; else _ListImgButtonPopupPick[0].sprite = _ListSpriteImgButtonPopupPick[1];
            if (_GameControler._ListStatePickEnemy[1].State) _ListImgButtonPopupPick[1].sprite = _ListSpriteImgButtonPopupPick[2]; else _ListImgButtonPopupPick[1].sprite = _ListSpriteImgButtonPopupPick[3];
            if (_GameControler._ListStatePickEnemy[2].State) _ListImgButtonPopupPick[2].sprite = _ListSpriteImgButtonPopupPick[4]; else _ListImgButtonPopupPick[2].sprite = _ListSpriteImgButtonPopupPick[5];
            if (_GameControler._ListStatePickEnemy[3].State)
                if (_GameControler._ListStatePickEnemy[3].Ammo > 0)
                {
                    _ListImgAmmo[3].enabled = true;
                    _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[6];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[3].State = false;
                    _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[7];
                    _ListImgAmmo[3].enabled = false; 
                }
            else { _ListImgButtonPopupPick[3].sprite = _ListSpriteImgButtonPopupPick[7]; _ListImgAmmo[3].enabled = false; }

            if (_GameControler._ListStatePickEnemy[4].State)
            {
                if (_GameControler._ListStatePickEnemy[4].Ammo > 0)
                {
                    _ListImgAmmo[4].enabled = true;
                    _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[8];
                    _ListImgAmmo[4].sprite = _ListAmmoSprite[_GameControler._ListStatePickEnemy[4].Ammo - 1];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[4].State = false;
                    _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[9];
                    _ListImgAmmo[4].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[4].sprite = _ListSpriteImgButtonPopupPick[9]; _ListImgAmmo[4].enabled = false; }
            if (_GameControler._ListStatePickEnemy[5].State)
            {
                if (_GameControler._ListStatePickEnemy[5].Ammo > 0)
                {
                    _ListImgAmmo[5].enabled = true;
                    _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[10];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[5].State = false;
                    _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[11];
                    _ListImgAmmo[5].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[5].sprite = _ListSpriteImgButtonPopupPick[11]; _ListImgAmmo[5].enabled = false; }
            if (_GameControler._ListStatePickEnemy[6].State)
            {
                if (_GameControler._ListStatePickEnemy[6].Ammo > 0)
                {
                    _ListImgAmmo[6].enabled = true;
                    _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[12];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[6].State = false;
                    _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[13];
                    _ListImgAmmo[6].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[6].sprite = _ListSpriteImgButtonPopupPick[13]; _ListImgAmmo[6].enabled = false; }
            if (_GameControler._ListStatePickEnemy[7].State)
            {
                if (_GameControler._ListStatePickEnemy[7].Ammo > 0)
                {
                    _ListImgAmmo[7].enabled = true;
                    _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[14];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[7].State = false;
                    _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[15];
                    _ListImgAmmo[7].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[7].sprite = _ListSpriteImgButtonPopupPick[15]; _ListImgAmmo[7].enabled = false; }
            if (_GameControler._ListStatePickEnemy[8].State)
            {
                if (_GameControler._ListStatePickEnemy[8].Ammo > 0)
                {
                    _ListImgAmmo[8].enabled = true;
                    _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[16];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[8].State = false;
                    _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[17];
                    _ListImgAmmo[8].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[8].sprite = _ListSpriteImgButtonPopupPick[17]; _ListImgAmmo[8].enabled = false; }
            if (_GameControler._ListStatePickEnemy[9].State)
            {
                if (_GameControler._ListStatePickEnemy[9].Ammo > 0)
                {
                    _ListImgAmmo[9].enabled = true;
                    _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[18];
                    _ListImgAmmo[9].sprite = _ListAmmoSprite[_GameControler._ListStatePickEnemy[9].Ammo - 1];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[9].State = false;
                    _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[19];
                    _ListImgAmmo[9].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[9].sprite = _ListSpriteImgButtonPopupPick[19]; _ListImgAmmo[9].enabled = false; }
            if (_GameControler._ListStatePickEnemy[10].State)
            {
                if (_GameControler._ListStatePickEnemy[10].Ammo > 0)
                {
                    _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[20];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[10].State = false;
                    _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[21];
                }
            }
            else { _ListImgButtonPopupPick[10].sprite = _ListSpriteImgButtonPopupPick[21]; }
            if (_GameControler._ListStatePickEnemy[11].State)
            {
                if (_GameControler._ListStatePickEnemy[11].Ammo > 0)
                {
                    _ListImgAmmo[11].enabled = true;
                    _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[22];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[11].State = false;
                    _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[23];
                    _ListImgAmmo[11].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[11].sprite = _ListSpriteImgButtonPopupPick[23]; _ListImgAmmo[11].enabled = false; }
            if (_GameControler._ListStatePickEnemy[12].State)
            {
                if (_GameControler._ListStatePickEnemy[12].Ammo > 0)
                {
                    _ListImgAmmo[12].enabled = true;
                    _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[24];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[12].State = false;
                    _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[25];
                    _ListImgAmmo[12].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[12].sprite = _ListSpriteImgButtonPopupPick[25]; _ListImgAmmo[12].enabled = false; }
            if (_GameControler._ListStatePickEnemy[13].State)
            {
                if (_GameControler._ListStatePickEnemy[13].Ammo > 0)
                {
                    _ListImgAmmo[13].enabled = true;
                    _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[26];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[13].State = false;
                    _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[27];
                    _ListImgAmmo[13].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[13].sprite = _ListSpriteImgButtonPopupPick[27]; _ListImgAmmo[13].enabled = false; }
            if (_GameControler._ListStatePickEnemy[14].State)
            {
                if (_GameControler._ListStatePickEnemy[14].Ammo > 0)
                {
                    _ListImgAmmo[14].enabled = true;
                    _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[28];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[14].State = false;
                    _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[29];
                    _ListImgAmmo[14].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[14].sprite = _ListSpriteImgButtonPopupPick[29]; _ListImgAmmo[14].enabled = false; }
            if (_GameControler._ListStatePickEnemy[15].State)
            {
                if (_GameControler._ListStatePickEnemy[15].Ammo > 0)
                {
                    _ListImgAmmo[15].enabled = true;
                    _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[30];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[15].State = false;
                    _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[31];
                    _ListImgAmmo[15].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[15].sprite = _ListSpriteImgButtonPopupPick[31]; _ListImgAmmo[15].enabled = false; }
            if (_GameControler._ListStatePickEnemy[16].State)
            {
                if (_GameControler._ListStatePickEnemy[16].Ammo > 0)
                {
                    _ListImgAmmo[16].enabled = true;
                    _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[32];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[16].State = false;
                    _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[33];
                    _ListImgAmmo[16].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[16].sprite = _ListSpriteImgButtonPopupPick[33]; _ListImgAmmo[16].enabled = false; }
            if (_GameControler._ListStatePickEnemy[17].State)
            {
                if (_GameControler._ListStatePickEnemy[17].Ammo > 0)
                {
                    _ListImgAmmo[17].enabled = true;
                    _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[34];
                }
                else
                {
                    _GameControler._ListStatePickEnemy[17].State = false;
                    _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[35];
                    _ListImgAmmo[17].enabled = false; 
                }
            }
            else { _ListImgButtonPopupPick[17].sprite = _ListSpriteImgButtonPopupPick[35]; _ListImgAmmo[17].enabled = false; }
        }
      
    }
    /// <summary>
    /// Load danh sách súng Enemy
    /// </summary>
    public void LoadStatePickEnemy()
    {
        string strPick = ReadWriteFileText.GetStringFromPrefab(Data._LinkPic);
        if (strPick == "") strPick = "1*1*1*0*1*0*0*0*1*0*0*1*0*0*0*1*0*0";
        string[] arrPick = strPick.Split('*');
        string[] arrAmmo = Data._LinkAmmo.Split('*');
        _GameControler._ListStatePickEnemy = new Pick[arrPick.Length];
        for (int i = 0; i < arrPick.Length; i++)
        {
            string state = arrPick[i] == "1" ? "true" : "false";
            Pick pic = new Pick(i, int.Parse(arrAmmo[i]), bool.Parse(state));
            _GameControler._ListStatePickEnemy[i] = pic;
        }
    }
    /// <summary>
    /// Load danh sách súng player
    /// </summary>
    public void LoadStatePickPlayer()
    {
        string strPick = ReadWriteFileText.GetStringFromPrefab(Data._LinkPic);
        if (strPick=="") strPick = "1*1*1*0*1*0*0*0*1*0*0*1*0*0*0*1*0*0";
        string[] arrPick = strPick.Split('*');
        string[] arrAmmo = Data._LinkAmmo.Split('*');
        _GameControler._ListStatePickPlayer = new Pick[arrPick.Length];
        for (int i = 0; i < arrPick.Length; i++)
        {
            string state = arrPick[i] == "1" ? "true" : "false";
            Pick pic = new Pick(i, int.Parse(arrAmmo[i]),bool.Parse(state));
            _GameControler._ListStatePickPlayer[i] = pic;
        }
    }
    /// <summary>
    /// Thực hiện mở khóa pick
    /// </summary>
    public void UnlockPick()
    {
        string strPick = "1*1*1*0*1*0*0*0*1*0*0*1*0*0*0*1*0*0";
        ReadWriteFileText.SaveStringToPrefab(Data._LinkPic, strPick);
    }
    /// <summary>
    /// Chọn súng trên bảng hiển thị
    /// </summary>
    public void ChoosePick(int index)
    {
        if (_GameControler._GameObj.tag=="Player")
        {
            if (!_GameControler._ListStatePickPlayer[index].State) return;
        }
        else
        {
            if(!_GameControler._ListStatePickEnemy[index].State) return;
        }
        //==============
        _SoundControler.PlayClickSound(true);
        _ListImgChoosePick[0].sprite = _ListSpriteImgButtonPopupPick[index * 2];
        if (index == 0) {
            _ListImgChoosePick[1].sprite = _ListPickSprite[2];
            _ListText[1].SetActive(true);
        }
        else
        {
            _ListImgChoosePick[1].sprite = _ListPickSprite[3];
            _ListText[1].SetActive(false);
        }
        _GameManager.UsePick(index);
        HidePopupPick();
        Destroy(GameObject.FindGameObjectWithTag("MyTurn"));
    }
    /// <summary>
    /// Show popup đến lượt quân xanh lá cây bắn (quân ta)
    /// </summary>
    public void ShowPopopTurnGreen()
    {
        _ListPopup[1].SetActive(true);
        _GameControler._GameState._IsEnemyStart = false;
    }
    /// <summary>
    /// Ẩn popup báo đến lượt quân xanh lá cây
    /// </summary>
    public void HidePopopTurnGreen()
    {
        _ListPopup[1].SetActive(false);
        _GameControler._GameState._IsPlayerStart = true;
    }
    /// <summary>
    /// Show popup báo đến lượt quân xanh da trời (quân địch)
    /// </summary>
    public void ShowPopupTurnBlue()
    {
        _ListPopup[2].SetActive(true);
        _GameControler._GameState._IsPlayerStart = false;
        if (_GameControler._TypeGame == 0) StartCoroutine(HidePopupTurnBlue());
        if (_GameControler._TypeGame==1)
        {
            _ListText[0].SetActive(true);
        }
        else _ListText[0].SetActive(false);
    }
    /// <summary>
    /// Ẩn popup báo đến lượt quân xanh da trời
    /// </summary>
    public void HidePopopTurnBlue()
    {
        _ListPopup[2].SetActive(false);
        _GameControler._GameState._IsEnemyStart = true;
    }
    /// <summary>
    /// An popup lượt quân xanh da trời
    /// </summary>
    /// <returns></returns>
    IEnumerator  HidePopupTurnBlue()
    {
        yield return new WaitForSeconds(2);
        _ListPopup[2].SetActive(false);
        _GameControler._GameState._IsEnemyStart = true;
        _GameControler._GameObj = _GameControler.GetMyTurn();
        _GameControler._GameState._IsChangding = false;
        _GameControler._StopTime = false;
        _mapControler = FindObjectOfType<MapControler>();
        _cameraControler.MoveCameraToBullet(_GameControler._GameObj.transform.position);
        _mapControler.MoveMapToBullet(_GameControler._GameObj.transform.position);
        StartCoroutine(TestWaitOffMyTurn());
    }
    IEnumerator TestWaitOffMyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        AIEnemy ai = _GameControler._GameObj.GetComponent<AIEnemy>();
        ai.HitPlayer();
    }
    #region======PanelOverGame=====
    /// <summary>
    /// Hiện panel Overgame
    /// </summary>
    public void ShowPanelGameOver(int isplayer)//Index là cho đội player(0) hoặc Enemy(1) 
    {
		//???
		Advertising.LoadInterstitialAd();
		bool isReady = Advertising.IsInterstitialAdReady(); 
		if (isReady)
		{
			Advertising.ShowInterstitialAd(); 
		}		
		
        _ListPopup[3].SetActive(true);
        PanelGameOver over = _ListPopup[3].GetComponent<PanelGameOver>();
        over.SetPopup(isplayer);
    }
    /// <summary>
    /// Ẩn Panel Over game
    /// </summary>
    public void HidePanelGameOver()
    {
        _ListPopup[3].SetActive(false);
    }
    #endregion
    #region======PanelStartGame=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelStartGame()
    {
        _ListPopup[4].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelStartGame()
    {
        _ListPopup[4].SetActive(false);
    }
    #endregion
    #region======PanelLoadGame=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelLoadGame()
    {
        _ListPopup[5].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelLoadGame()
    {
        _ListPopup[5].SetActive(false);
    }
    #endregion
    #region======PanelChoosePlayer=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelChoosePlayer()
    {
        _ListPopup[6].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelChoosePlayer()
    {
        _ListPopup[6].SetActive(false);
    }
    #endregion
    #region======PanelArmory=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelArmory()
    {
        _ListPopup[7].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelArmory()
    {
        _ListPopup[7].SetActive(false);
    }
    #endregion
    #region======PanelLevel=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelLevel()
    {
        _ListPopup[8].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelLevel()
    {
        _ListPopup[8].SetActive(false);
    }
    #endregion
    #region======PanelChooseMapAndNumPlayer=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelChooseMapAndNumPlayer()
    {
        _ListPopup[9].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelChooseMapAndNumPlayer()
    {
        _ListPopup[9].SetActive(false);
    }
    #endregion
    #region======PanelExitGame=====
    /// <summary>
    /// Hiện panel ShowPanelStartGame
    /// </summary>
    public void ShowPanelExitGame()
    {
        _ListPopup[10].SetActive(true);
    }
    /// <summary>
    /// Ẩn Panel ShowPanelStartGame
    /// </summary>
    public void HidePanelExitGame()
    {
        _ListPopup[10].SetActive(false);
    }
    #endregion
}
