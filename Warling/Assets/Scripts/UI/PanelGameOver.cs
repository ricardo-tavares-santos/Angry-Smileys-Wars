using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//???
using EasyMobile;

public class PanelGameOver : MonoBehaviour {

    public GameObject[] _imgTeam;
    public GameObject[] _imgMedal;
    public Sprite[] _SpriteMedal;
    UIManager _UIManager;
    GameControler _GameControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
	}
    public void SetPopup(int _Team)
    {
        _GameControler = FindObjectOfType<GameControler>();
        _UIManager = FindObjectOfType<UIManager>();
        if (_Team == 0)//Green thắng
        {
            _imgTeam[0].SetActive(true);
            _imgTeam[1].SetActive(false);
            if (_GameControler._TypeGame==0)
            {
                _imgMedal[0].SetActive(true);
                _imgMedal[1].SetActive(true);
                _imgMedal[2].SetActive(true);
                //======
                int numStar = _GameControler._NumberPlayer;
                if (numStar > 3) numStar = 3;
                GameObject[] arrPlayer = GameObject.FindGameObjectsWithTag("Player");
                int a = _GameControler._NumberPlayer - arrPlayer.Length;
                numStar -= a;
                if (numStar < 1) numStar = 1;
                if (numStar==1)
                {
                    _imgMedal[0].GetComponent<Image>().sprite = _SpriteMedal[0];
                    _imgMedal[1].GetComponent<Image>().sprite = _SpriteMedal[1];
                    _imgMedal[2].GetComponent<Image>().sprite = _SpriteMedal[1];
                }
                else if (numStar==2)
                {
                    _imgMedal[0].GetComponent<Image>().sprite = _SpriteMedal[0];
                    _imgMedal[1].GetComponent<Image>().sprite = _SpriteMedal[0];
                    _imgMedal[2].GetComponent<Image>().sprite = _SpriteMedal[1];
                }
                else
                {
                    _imgMedal[0].GetComponent<Image>().sprite = _SpriteMedal[0];
                    _imgMedal[1].GetComponent<Image>().sprite = _SpriteMedal[0];
                    _imgMedal[2].GetComponent<Image>().sprite = _SpriteMedal[0];
                }
                //=============
                ReadWriteFileText.SaveStringToPrefab(Data._Gold, (_GameControler._Gold + numStar)+"");
                _GameControler._Gold += numStar;
                string strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1";
                if (_GameControler._Gold>= 90)
                {
                    strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1";
                }
                else if (_GameControler._Gold >= 60)
                {
                    strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*0*1";
                }
                else if (_GameControler._Gold >= 45)
                {
                    strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*1*0*0";
                }
                else if (_GameControler._Gold >= 32)
                {
                    strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*1*0*1*0*0";
                }
                else if (_GameControler._Gold >= 24)
                {
                    strPick = "1*1*1*1*1*1*1*1*1*1*1*1*1*0*0*1*0*0";
                }
                else if (_GameControler._Gold >= 18)
                {
                    strPick = "1*1*1*1*1*1*1*0*1*1*1*1*1*0*0*1*0*0";
                }
                else if (_GameControler._Gold >= 12)
                {
                    strPick = "1*1*1*1*1*1*0*0*1*1*1*1*1*0*0*1*0*0";
                }
                else if (_GameControler._Gold >= 7)
                {
                    strPick = "1*1*1*0*1*1*0*0*1*1*1*1*1*0*0*1*0*0";
                }
                else if (_GameControler._Gold >= 3)
                {
                    strPick = "1*1*1*0*1*1*0*0*1*0*1*1*1*0*0*1*0*0";
                }
                else if (_GameControler._Gold >= 1)
                {
                    strPick = "1*1*1*0*1*1*0*0*1*0*1*1*0*0*0*1*0*0";
                }
                else strPick = "1*1*1*0*1*0*0*0*1*0*0*1*0*0*0*1*0*0";
                ReadWriteFileText.SaveStringToPrefab(Data._LinkPic, strPick);
            }
            else
            {
                _imgMedal[0].SetActive(false);
                _imgMedal[1].SetActive(false);
                _imgMedal[2].SetActive(false);
            }
        }
        else
        {
            _imgTeam[0].SetActive(false);
            _imgTeam[1].SetActive(true);
            //===========
            _imgMedal[0].SetActive(false);
            _imgMedal[1].SetActive(false);
            _imgMedal[2].SetActive(false);
        }


		//???
		if (GameServices.IsInitialized()) {
			GameServices.ReportScore(_GameControler._Gold, EM_GameServicesConstants.Leaderboard_Medals);
		}		

		
		
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            _UIManager.ShowPanelStartGame();
            _UIManager.ShowPanelChoosePlayer();
            _UIManager.ShowPanelChooseMapAndNumPlayer();
            _UIManager.ShowPanelLevel();
        }
	}
}
