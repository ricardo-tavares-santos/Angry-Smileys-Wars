using UnityEngine;
using System.Collections;

public class AirCallControler : MonoBehaviour {

    public GameObject[] Pick;//Vũ khí được sử dụng hiện tại
    public string _Type = "Pick_Bomb";
    GameControler _GameControler;
    GameManager _GameManager;
    UIManager _UIManager;
    bool _CheckHit = false;
    SoundControler _SoundControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
        _GameManager = FindObjectOfType<GameManager>();
        _UIManager = FindObjectOfType<UIManager>();
        _SoundControler = FindObjectOfType<SoundControler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_Type=="Pick_Swap" && _CheckHit)
        {
            ActivePick_Swap();
        }
        if (_CheckHit) return;
        if (_Type == "Pick_Health") Pick_Health();
        if (_GameControler._TypeGame == 0 && _GameControler._GameState._IsEnemyStart) return;
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < _GameControler.INood.transform.position.y || _UIManager._ListPopup[0].activeSelf) return;
        if (_Type == "Pick_Bomb") Pick_Bomb();
        if (_Type == "Pick_Teleport") Pick_Teleport();
        if (_Type == "Pick_Swap") Pick_Swap();
	}
    /// <summary>
    /// Thực hiện khi type là Pick_bomp
    /// </summary>
    private void Pick_Bomb()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[3].Ammo -= 1; else _GameControler._ListStatePickEnemy[3].Ammo -= 1;
            Bounds bound = Helper.OrthographicBounds(Camera.main);
            Vector3 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 posInit = new Vector3(posMouse.x,bound.max.y,0);
            StartCoroutine(waitForInit(0, new Vector3(posInit.x + 1.5f, posInit.y, posInit.z)));
            StartCoroutine(waitForInit(0.1f, new Vector3(posInit.x + 0.75f, posInit.y, posInit.z)));
            StartCoroutine(waitForInit(0.2f, new Vector3(posInit.x + 0.0f, posInit.y, posInit.z)));
            StartCoroutine(waitForInit(0.3f, new Vector3(posInit.x - 0.75f, posInit.y, posInit.z)));
            StartCoroutine(waitForInit(0.4f, new Vector3(posInit.x - 1.5f, posInit.y, posInit.z)));
            _CheckHit = true;
            _GameControler._CheckHit = true;
            _UIManager.ResetImgPick();
        }
    }
    /// <summary>
    /// Bomp cho AI Enemy
    /// </summary>
    public void Pick_BombEnemy(Vector3 target)
    {
        if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[3].Ammo -= 1; else _GameControler._ListStatePickEnemy[3].Ammo -= 1;
        Bounds bound = Helper.OrthographicBounds(Camera.main);
        Vector3 posMouse = target;
        Vector3 posInit = new Vector3(posMouse.x, bound.max.y, 0);
        StartCoroutine(waitForInit(0, new Vector3(posInit.x + 1.5f, posInit.y, posInit.z)));
        StartCoroutine(waitForInit(0.1f, new Vector3(posInit.x + 0.75f, posInit.y, posInit.z)));
        StartCoroutine(waitForInit(0.2f, new Vector3(posInit.x + 0.0f, posInit.y, posInit.z)));
        StartCoroutine(waitForInit(0.3f, new Vector3(posInit.x - 0.75f, posInit.y, posInit.z)));
        StartCoroutine(waitForInit(0.4f, new Vector3(posInit.x - 1.5f, posInit.y, posInit.z)));
        _GameControler._CheckHit = true;
        _UIManager.ResetImgPick();
    }
    IEnumerator waitForInit(float time,Vector3 pos)
    {
        yield return new WaitForSeconds(time);
        Instantiate(Pick[0], pos, Quaternion.identity);
    }
    
    /// <summary>
    /// Thực hiện hàm chuyển đổi vị trí
    /// </summary
    Vector3 _posDes;//Vị trí nhân vật di chuyển đến
    GameObject _objTeleportOn, _objTeleportOff;
    private void Pick_Teleport()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _posDes = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_GameManager.CheckTeleportPlayer(_posDes)) return;
            _posDes.z = -1;
            if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[9].Ammo -= 1; else _GameControler._ListStatePickEnemy[9].Ammo -= 1;
            _SoundControler.PlayTeleportSound(true);
            _objTeleportOff = (GameObject)Instantiate(Pick[1], gameObject.transform.position, Quaternion.identity);
            StartCoroutine(waitPick_Teleport_Off());
            _CheckHit = true;
        }
    }
    IEnumerator waitPick_Teleport_Off()
    {
        yield return new WaitForSeconds(0.7f);
        _SoundControler.PlayTeleportSound(true);
        Destroy(_objTeleportOff);
        _objTeleportOn= (GameObject)Instantiate(Pick[2], _posDes, Quaternion.identity);
        StartCoroutine(waitPick_Teleport_On());
    }
    IEnumerator waitPick_Teleport_On()
    {
        yield return new WaitForSeconds(0.6f);
        _GameControler._GameObj.transform.position = _posDes;
        _UIManager.SetImgButtonChoosePick();
        Destroy(_objTeleportOn);
        _GameControler._CheckHit = true;
        _UIManager.ResetImgPick();
        _GameControler.ChangeTurn();
        Destroy(gameObject);
    }

    #region-===================PICK_SWAP=============
    public void Pick_Swap()//Pick 3
    {
        Player player = GetComponentInParent<Player>();
        string tag="Player";
        if (player.gameObject.tag == "Player") tag = "Player"; else tag = "Enemy";
        GameObject[] arrPlayer = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < arrPlayer.Length; i++)
        {
            player = arrPlayer[i].GetComponent<Player>();
            if (!player._IsCurrent)
            {
                GameObject swap = (GameObject)Instantiate(Pick[3], player.gameObject.transform.position, Quaternion.identity);
                swap.transform.parent = player.gameObject.transform;
                swap.transform.position = new Vector3(swap.transform.position.x, swap.transform.position.y,-5);
            }
        }
        _CheckHit = true;
    }
    public void ActivePick_Swap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Helper.GetGameObjectAtPossition();
            if (obj != null && obj.tag == "Swap")
            {
                if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[15].Ammo -= 1; else _GameControler._ListStatePickEnemy[15].Ammo -= 1;
                _GameControler._GameObj = obj.transform.parent.gameObject;
                //======
                _GameControler.SetMyTurnForObj(_GameControler._GameObj);
                _UIManager.SetImgButtonChoosePick();
               //========
                GameObject[] arrBullet = GameObject.FindGameObjectsWithTag("Swap");
                for (int i = 0; i < arrBullet.Length; i++)
                {
                    Destroy(arrBullet[i]);
                }
                Destroy(gameObject);
            }
        }
    }
    #endregion===

    #region================Health===========
    public void Pick_Health()
    {
        if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[14].Ammo -= 1; else _GameControler._ListStatePickEnemy[14].Ammo -= 1;
        GameObject health=(GameObject)Instantiate(Pick[4], _GameControler._GameObj.transform.position, Quaternion.identity);
        health.transform.parent = _GameControler._GameObj.transform;
        Glow_PoisonControler poison = _GameControler._GameObj.GetComponentInChildren<Glow_PoisonControler>();
        if (poison != null) Destroy(poison.gameObject);
        Player player = _GameControler._GameObj.GetComponent<Player>();
        if (player._IsPoison) player._IsPoison = false;
        GameObject[] pick = GameObject.FindGameObjectsWithTag("Pick");
        for (int i = 0; i < pick.Length; i++) Destroy(pick[i]);
        //=======
        _CheckHit = true;
        _GameControler._CheckHit = true;
        _UIManager.ResetImgPick();
    }
    #endregion==============================
}
