using UnityEngine;
using System.Collections;

public class Pick_ShieldControler : MonoBehaviour
{
    int _numTurn = 0;//Dùng thực hiện đếm turn
    bool _isReally = false;//Cho phép đếm turn và không cho phép
    public int _NumTurn = 4;
    GameControler _GameControler;
    UIManager _UIManager;
    bool _checkIssHit = false;
    SoundControler _SoundControler;
    //Bảo vệ trong vòng 3 turn
    //Đếm turn 
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
        Player player = gameObject.GetComponentInParent<Player>();
        _UIManager = FindObjectOfType<UIManager>();
        _SoundControler = FindObjectOfType<SoundControler>();
        _SoundControler.PlayShieldSound(true);
        player._ISShield = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (_GameControler._GameState._IsChangding==false && _isReally==false)
        {
            _isReally = true;
            if (!_checkIssHit) {
                if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[6].Ammo -= 1; else _GameControler._ListStatePickEnemy[6].Ammo -= 1;
                _GameControler._CheckHit = true;
                _UIManager.ResetImgPick();
                GameControler gameControler = FindObjectOfType<GameControler>();
                if (gameControler._GameObj.tag == "Enemy")
                {
                    gameControler.ChangeTurn();
                }
            }
            _checkIssHit = true;
            
        }
        if (_isReally && _GameControler._GameState._IsChangding && _checkIssHit)
        {
            _isReally = false;
            _numTurn++;
            if (_numTurn == _NumTurn)
            {
                Player player = gameObject.GetComponentInParent<Player>();
                player._ISShield = false;
                Destroy(gameObject);
            }
        }
       
	}
}
