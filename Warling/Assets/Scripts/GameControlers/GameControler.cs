using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameControler : MonoBehaviour {
   
    public GameObject _GameObj;//Lưu trữ đối tượng player hoặc Enemy đang được chọn
    public GameObject _Pick;//Lưu vũ khí đang được chọn hiện tại
    public GameObject[] _Prefab_MyTurn;//Luu prefabs myturn
    public GameState _GameState=new GameState();//Lưu lại các trạng thái game
    public Pick[] _ListStatePickEnemy;//Lưu lại danh sách trạng thái vũ khí hiện tại có được sử dụng hay không cua quaan ddich
    public Pick[] _ListStatePickPlayer;//Lưu lại danh sách trạng thái vũ khí hiện tại có được sử dụng hay không cua Player
    public Image[] _ListImageTimer;//Lưu list số để chạy 30s
    public GameObject[] _ListPlayer;
    public GameObject[] _ListMap;
    public GameObject INood;//Lưu tọa độ khi click chuột để kiểm tra
    public int _TypeGame = 1;//0: Single player 1: Hotseat
    public bool _CheckHit = false;
    public int _Level = 1;//Level 1:Easy 2:Normal 3:Hard

    UIManager _UIManager;
    CameraControler _cameraControler;
    MapControler _mapControler;
    NumberTextScale _numberText;
    GameManager _GameManager;
    int _Timer = 30;
    float _timeCount = 0;
    public int _NumberPlayer=3;//Số lượng player của người chơi chọn
    public int _IndexMap = 1;//Chi số map hiện tại
    public bool _StopTime = false;
    public int _Gold=0;//Số gold hiện tại
    
    BackgoundSoundControler _BgControler;
    SoundControler _SoundControler;
      
	// Use this for initialization
	void Start () {
	    _UIManager =FindObjectOfType<UIManager>();
        _cameraControler = FindObjectOfType<CameraControler>();
        _numberText = GetComponent<NumberTextScale>();
        _GameManager = FindObjectOfType<GameManager>();
        _BgControler = FindObjectOfType<BackgoundSoundControler>();
        _BgControler.PlayBackground(true);
        _SoundControler = FindObjectOfType<SoundControler>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { _UIManager.ShowPanelExitGame(); }
        if (!_GameState._IsGamePlay) return;
        if ((_GameState._IsPlayerStart || _GameState._IsEnemyStart) && !_GameState._IsChangding && !_StopTime)
        {
            _timeCount += Time.deltaTime;
            if (_timeCount>=1)
            {
                _timeCount = 0;
                if (_Timer > 0) _Timer--;
                _numberText.SetNumText2(_Timer,_ListImageTimer);
                GameObject[] objBullet=GameObject.FindGameObjectsWithTag("Bullet");
                if (_Timer<=0 && objBullet.Length==0 )
                {
                    if (!_GameState._IsChangding)
                    {
                        ChangeTurn();
                    }
                }
            }
        }
        else
        {
            _Timer = 30;
          //  _numberText.SetNumberText(0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (_GameState._IsPlayer && !_GameState._IsPlayerStart)
            {
                _UIManager.HidePopopTurnGreen();
                _GameObj=GetMyTurn();
                _GameState._IsChangding = false;
                _StopTime = false;
                //======
                _mapControler = FindObjectOfType<MapControler>();
                _cameraControler.MoveCameraToBullet(_GameObj.transform.position);
                _mapControler.MoveMapToBullet(_GameObj.transform.position);
                _cameraControler._IsMove = true;
                _mapControler._IsMove = true;
            }
        }
        if (_TypeGame==1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_GameState._IsPlayer && !_GameState._IsEnemyStart)
                {
                    _UIManager.HidePopopTurnBlue();
                    _GameObj = GetMyTurn();
                    _GameState._IsChangding = false;
                    _StopTime = false;
                    //======
                    _mapControler = FindObjectOfType<MapControler>();
                    _cameraControler.MoveCameraToBullet(_GameObj.transform.position);
                    _mapControler.MoveMapToBullet(_GameObj.transform.position);
                    _cameraControler._IsMove = true;
                    _mapControler._IsMove = true;
                }
            }
        }
    }
    public GameObject GetMyTurn()
    {
        if (_GameState._IsPlayer)
        {
            GameObject[] arrEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < arrEnemy.Length; i++)
            {
                Player player = arrEnemy[i].GetComponent<Player>();
                player._IsCurrent = false;
            }
        }
        else
        {
            GameObject[] arrPlayer = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < arrPlayer.Length; i++)
            {
                Player player = arrPlayer[i].GetComponent<Player>();
                player._IsCurrent = false;
            }
        }
        string tag = _GameState._IsPlayer ? "Player" : "Enemy";
        GameObject[] arrGameObj = GameObject.FindGameObjectsWithTag(tag);
        Object obj = new Object();
        int index = 100;
        int count = 0;//Dếm số phần tử chưa bắn còn lại
        #region==========Test thử xem có thằng nào chưa được bắn hay ko========
        for (int i = 0; i < arrGameObj.Length; i++)
        {
            Player player = arrGameObj[i].GetComponent<Player>();
            if (!player._IsFile)
            {
                count++;
            }
        }
        if (count == 0)
        {
            for (int i = 0; i < arrGameObj.Length; i++)
            {
                Player player = arrGameObj[i].GetComponent<Player>();
                player._IsFile = false;
            }
        }
        #endregion
        for (int i = 0; i < arrGameObj.Length; i++)
        {
            Player player = arrGameObj[i].GetComponent<Player>();
            player._IsCurrent = false;
            if (!player._IsFile)
            {
                if (player._Index<index) obj = arrGameObj[i];
            }
        }
     
        //Hiện myturn vào thăng obj được chọn
        Player playerChoose=((GameObject)obj).GetComponent<Player>();
        playerChoose._IsCurrent = true;
        playerChoose._IsFile = true;
        GameObject myTurnOld = GameObject.FindGameObjectWithTag("MyTurn");
        if (myTurnOld != null) Destroy(myTurnOld);
        GameObject myTurn;
        if (playerChoose._Dir == Player.Dir.right) myTurn= (GameObject)Instantiate(_Prefab_MyTurn[0], ((GameObject)obj).transform.position, Quaternion.identity);
        else myTurn= (GameObject)Instantiate(_Prefab_MyTurn[1], ((GameObject)obj).transform.position, Quaternion.identity);
        myTurn.transform.parent = ((GameObject)obj).transform;
        return (GameObject)obj;
    }
    public void SetMyTurnForObj(GameObject obj)
    {
        if (_GameState._IsPlayer)
        {
            GameObject[] arrEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < arrEnemy.Length; i++)
            {
                Player player = arrEnemy[i].GetComponent<Player>();
                player._IsCurrent = false;
            }
        }
        else
        {
            GameObject[] arrPlayer = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < arrPlayer.Length; i++)
            {
                Player player = arrPlayer[i].GetComponent<Player>();
                player._IsCurrent = false;
            }
        }
        GameObject myTurnOld = GameObject.FindGameObjectWithTag("MyTurn");
        if (myTurnOld != null) Destroy(myTurnOld);
        GameObject myTurn;
        Player playerChoose = obj.GetComponent<Player>();
        playerChoose._IsCurrent = true;
        if (playerChoose._Dir == Player.Dir.right) myTurn = (GameObject)Instantiate(_Prefab_MyTurn[0], ((GameObject)obj).transform.position, Quaternion.identity);
        else myTurn = (GameObject)Instantiate(_Prefab_MyTurn[1], ((GameObject)obj).transform.position, Quaternion.identity);
        myTurn.transform.parent = ((GameObject)obj).transform;
    }
    public void MoveCameraToObj()
    {
        _mapControler = FindObjectOfType<MapControler>();
        _cameraControler.MoveCameraToBullet(_GameObj.transform.position);
        _mapControler.MoveMapToBullet(_GameObj.transform.position);
        _cameraControler._IsMove = false;
        _mapControler._IsMove = false;
    }
    /// <summary>
    /// Thực hiện chuyển turn giữa các đối tượng trong game
    /// </summary>
    public void ChangeTurn()
    {
        _GameState._IsChangding = true;
        _CheckHit = false;
        StartCoroutine(WaitForChangeTurn());
    }
    private int CheckGameOver()
    {
        GameObject[] arrGreen = GameObject.FindGameObjectsWithTag("Player");
        if (arrGreen.Length == 0)
        {
            _GameState._IsGamePlay = false;
            return 1;//Đội blue thắng
        }
        GameObject[] arrBlue = GameObject.FindGameObjectsWithTag("Enemy");
        if (arrBlue.Length == 0)
        {
            _GameState._IsGamePlay = false;
            return 0;//Đội green thắng
        }
        return -1;
    }
    /// <summary>
    /// Bắt đầu chơi game
    /// </summary>
    public void StartGame()
    {
        GameObject mapOld = GameObject.FindGameObjectWithTag("Maps");
        if (mapOld != null) Destroy(mapOld);
        GameObject[] playerOld = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerOld.Length; i++)
        {
            Destroy(playerOld[i]);
        }
        GameObject[] enemyOld = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyOld.Length; i++)
        {
            Destroy(enemyOld[i]);
        }
        //=============
        Instantiate(_ListMap[_IndexMap],new Vector3(0,0,0),Quaternion.identity);

        GameObject other = GameObject.FindGameObjectWithTag("Map_Front");
        float xMax = other.transform.position.x + (other.GetComponent<BoxCollider2D>().size.x * other.transform.localScale.x) / 2;
        float xMin = other.transform.position.x - (other.GetComponent<BoxCollider2D>().size.x * other.transform.localScale.x) / 2;
        float yMax = other.transform.position.y + (other.GetComponent<BoxCollider2D>().size.y * other.transform.localScale.y) / 2;
        List<float> listPos = new List<float>();
		
	
        for (int i = 0; i < _NumberPlayer; i++)
        {
            float ranPosX = Random.Range(xMin+2,xMax-2);
            Vector3 pos = new Vector3(ranPosX, yMax, 0);
            while (!_GameManager.CheckInitPlayer(pos) || !_GameManager.CheckPosXPlayer(pos.x, listPos, 0.75f))
            {
                ranPosX = Random.Range(xMin + 2, xMax - 2);
                pos = new Vector3(ranPosX, yMax, 0);
            }
            Instantiate(_ListPlayer[0],pos,Quaternion.identity);
            listPos.Add(pos.x);
        }
        for (int i = 0; i < _NumberPlayer; i++)
        {
            float ranPosX = Random.Range(xMin + 2, xMax - 2);
            Vector3 pos = new Vector3(ranPosX, yMax, 0);
            while (!_GameManager.CheckInitPlayer(pos) || !_GameManager.CheckPosXPlayer(pos.x, listPos, 0.75f))
            {
                ranPosX = Random.Range(xMin + 2, xMax - 2);
                pos = new Vector3(ranPosX, yMax, 0);
            }
            Instantiate(_ListPlayer[1], pos, Quaternion.identity);
            listPos.Add(pos.x);
        }

		
        _UIManager.LoadStatePickEnemy();
        _UIManager.LoadStatePickPlayer();
        //===========
        Camera.main.transform.position = new Vector3(0, 0, -10);
        //============
        StartCoroutine(WaitForStartGame());
    }
    /// <summary>
    /// Lấy số Gold hiện tại
    /// </summary>
    public int LoadGold()
    {
        string gold=ReadWriteFileText.GetStringFromPrefab(Data._Gold);
        if(gold!="")
        {
            return int.Parse(gold);
        }
        return 0;
    }
    /// <summary>
    /// Thoat khoi game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Khi click vào nut time
    /// </summary>
    public void btnTime()
    {
        _SoundControler.PlayClickSound(true);
        GameObject[] arrBullet = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] arrDart = GameObject.FindGameObjectsWithTag("Dart");
        if (_CheckHit && !_GameState._IsChangding && arrBullet.Length==0 && arrDart.Length==0)
        {
            ChangeTurn();
        }
    }
    IEnumerator WaitForChangeTurn()
    {
        yield return new WaitForSeconds(2f);
        Player[] arrPlayer = FindObjectsOfType<Player>();
        for (int i = 0; i < arrPlayer.Length; i++)
        {
            if (arrPlayer[i]._IsPoison)
            {
                if (arrPlayer[i]._Health > 10)
                {
                    arrPlayer[i]._Health -= 10;
                }
                else arrPlayer[i]._Health = 1;
                NumberText textHealth = arrPlayer[i].gameObject.GetComponentInChildren<NumberText>();
                textHealth.SetNumberText(arrPlayer[i]._Health);
            }
        }
        _UIManager.SetImgButtonChoosePick();
        _numberText.SetNumText2(30,_ListImageTimer);
        int checkOver = CheckGameOver();
        if (checkOver != -1)
        {
            _UIManager.ShowPanelGameOver(checkOver);
        }
        else
        {
            GameObject[] arrPickOld = GameObject.FindGameObjectsWithTag("Pick");
            for (int i = 0; i < arrPickOld.Length; i++)
            {
                Pick_ShieldControler pick = arrPickOld[i].GetComponent<Pick_ShieldControler>();
                Pick_MineControler pickMine = arrPickOld[i].GetComponent<Pick_MineControler>();
                if (pickMine!=null) pickMine.ActivePick_Mine();
                if (pick == null && pickMine==null) Destroy(arrPickOld[i]);
            }
            GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bullet.Length; i++)
            {
                Destroy(bullet[i]);
            }
            GameObject dart = GameObject.FindGameObjectWithTag("Dart");
            if (dart != null) Destroy(dart);

            GameObject[] line = GameObject.FindGameObjectsWithTag("Dot");
            for (int i = 0; i < line.Length; i++)
            {
                Destroy(line[i]);
            }
            //======================
            if (_GameState._IsPlayer)//Chuyển sang Enemy
            {
                _GameState._IsPlayer = false;
                _UIManager.ShowPopupTurnBlue();
            }
            else//Chuyển sang Player
            {
                _GameState._IsPlayer = true;
                _UIManager.ShowPopopTurnGreen();
            }
        }
       
    }

    IEnumerator WaitForStartGame()
    {
        yield return new WaitForSeconds(1);
        _GameState._IsPlayer = true;
        _GameState._IsPlayerStart = false;
        _GameState._IsGamePlay = true;
        _GameState._IsChangding = true;
        _CheckHit = false;
        _StopTime = false;
    }
    /// <summary>
    /// Class lưu lại các trạng thái game
    /// </summary>
    public class GameState
    {
        public  bool _IsGamePlay = false;
        public  bool _IsPlayer = true;//Lưu trữ lượt chơi hiện tại đang là của thằng Player
        public  bool _IsPlayerStart = false;//Trạng thái quân xanh lá cây (quân ta) bắt đầu chơi
        public  bool _IsEnemyStart = false;//trạng thái quân địch bắt đầu được chơi
        public bool _IsChangding = false;//Trang thai đang chuyển trạng thái
        public bool _Sound = true;//Lưu trữ trạng thái âm thanh
        public bool _Music = true;//Lưu trữ trạng thái music
    }
}
