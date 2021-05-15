using UnityEngine;
using System.Collections;

public class Pick_MineControler : MonoBehaviour {

    public GameObject Explose;
    bool _IsReally = false;
    public bool _IsInit = false;
    bool _IsStart = false;
    float _timeCount = 0;
    bool _checkIssHit = false;
    GameControler _GameControler;
    UIManager _UIManager;
    SoundControler _SoundControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        _UIManager = FindObjectOfType<UIManager>();
        _SoundControler = FindObjectOfType<SoundControler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_IsInit)
        {
            _timeCount += Time.deltaTime;
            if (_timeCount > 5)
            {
                _IsInit = false;
                _IsStart = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                Animator ani = gameObject.GetComponent<Animator>();
                ani.SetBool("Init", false);
                ani.SetBool("Start", true);
                if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[5].Ammo -= 1; else _GameControler._ListStatePickEnemy[5].Ammo -= 1;
                //  _GameControler._CheckHit = true;
            }
        }
        if (_checkIssHit) return;
        if (_GameControler._TypeGame == 0 && _GameControler._GameState._IsEnemyStart) return;
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < _GameControler.INood.transform.position.y || _UIManager._ListPopup[0].activeSelf) return;
        if (!_IsReally)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject obj = Helper.GetGameObjectAtPossition();
                if (obj == null) return;
                if (obj.tag == "Pick")
                {
                    if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[5].Ammo -= 1; else _GameControler._ListStatePickEnemy[5].Ammo -= 1;
                    _GameControler._CheckHit = true;
                    _UIManager.ResetImgPick();
                    ActivePick_Mine();
                }
            }
        }  
	}
    public void ActivePick_Mine()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        _IsReally = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_IsReally) return;
        if (other.gameObject.tag=="Map")
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _IsInit = true;
            Animator ani = gameObject.GetComponent<Animator>();
            ani.SetBool("Init",true);
            ani.SetBool("Start", false);
            gameObject.transform.parent = other.gameObject.transform;
            _checkIssHit = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_IsStart) return;
        if (other.gameObject.tag=="Player"|| other.gameObject.tag=="Enemy")
        {
            _SoundControler.PlayExplosionSound(true);
            Instantiate(Explose,gameObject.transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
