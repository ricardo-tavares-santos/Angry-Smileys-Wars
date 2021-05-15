using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pick_GrenadeControler : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;
    public GameObject _NumberText;

    public int numOfTrajectoryPoints = 30;
    public float _Angle;//Góc sai khác của súng so vs goc ban đầu
    private GameObject numText;
    public GameObject _Explosion;
    private bool isPressed, isBallThrown;
    private float power = 5;
    private List<GameObject> trajectoryPoints;
    GameControler _GameControler;
    private float _scale = 0.1f;
    private int _timeDestroy = 3;
    private float _timeCount = 0;
    private Vector3 _posDown, _posUp;//Điểm bắt đầu và kết thúc kéo tay lấy lực
    UIManager _UIManager;
    SoundControler _SoundControler;
    //---------------------------------------	
    void Start()
    {
        trajectoryPoints = new List<GameObject>();
        _GameControler = FindObjectOfType<GameControler>();
        _UIManager = FindObjectOfType<UIManager>();
        _SoundControler = FindObjectOfType<SoundControler>();
        isPressed = isBallThrown = false;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = (GameObject)Instantiate(TrajectoryPointPrefeb);
            dot.transform.localScale = new Vector3((numOfTrajectoryPoints - i * 0.6f) * _scale, (numOfTrajectoryPoints - i * 0.6f) * _scale, (numOfTrajectoryPoints - i * 0.6f) * _scale);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i, dot);
        }
    }
    //---------------------------------------	
    void Update()
    {
      

        //=======Di chuyển số theo gameObject
        if (isBallThrown && numText!=null)
        {
            numText.transform.position = gameObject.transform.position;
            _timeCount += Time.deltaTime;
            if (_timeCount>1)
            {
                _timeDestroy--;
                _timeCount = 0;
                NumberText lbNumText = numText.GetComponentInChildren<NumberText>();
                lbNumText.SetNumberText(_timeDestroy);
                if (_timeDestroy==0)
                {
                    Instantiate(_Explosion, transform.position, Quaternion.identity);
                    _SoundControler.PlayExplosionSound(true);
                    //Chuyển turn============================
                    GameControler gameControler = FindObjectOfType<GameControler>();
                    if (!gameControler._GameState._IsChangding) gameControler.ChangeTurn();
                    //==================
                    Destroy(numText);
                    Destroy(gameObject);
                }
            }
            return;
        }
        if (_GameControler._CheckHit) return;
        if (_GameControler._TypeGame == 0 && _GameControler._GameState._IsEnemyStart) return;
        if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).y < _GameControler.INood.transform.position.y || _UIManager._ListPopup[0].activeSelf) && !isPressed) return;
        if (Input.GetMouseButtonDown(0))
        {
            _posDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isPressed = true;
            if (!numText)
                createText();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            _posUp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!isBallThrown)
            {
                throwBall();
            }
        }
        if (isPressed)
        {
            Vector3 vel = GetForceFrom(Camera.main.ScreenToWorldPoint(Input.mousePosition), _posDown);//Vector vận tốc ban đầu
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;//Góc tạo giữa vel và trục Ox
            Player player = _GameControler._GameObj.GetComponent<Player>();
            angle += _Angle;
            if (player._Dir == Player.Dir.left)
            {
                angle = 360 - angle;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
            setTrajectoryPoints(transform.position, vel / gameObject.GetComponent<Rigidbody2D>().mass);
        }

    }

    private void createText()
    {
        numText = (GameObject)Instantiate(_NumberText);

        Vector3 pos = transform.position;
        pos.z = 1;
        numText.transform.position = pos;
    }
  
    //---------------------------------------	
    private void throwBall()
    {
        _NumberText.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        //	ball.GetComponent<Rigidbody2D>().useGravity = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(_posUp, _posDown), ForceMode2D.Impulse);
        isBallThrown = true;
        ///Xóa đường chỉ dẫn
        GameObject[] arrObj = GameObject.FindGameObjectsWithTag("Dot");
        for (int i = 0; i < arrObj.Length; i++) Destroy(arrObj[i]);
        _GameControler._CheckHit = true;
        _GameControler._StopTime = true;
        _UIManager.ResetImgPick();
    }
    public void throwBallAIEnemy(Vector3 vel)
    {
        if (!numText)
            createText();
        _NumberText.SetActive(true);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        //	ball.GetComponent<Rigidbody2D>().useGravity = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);
        isBallThrown = true;
        _GameControler._CheckHit = true;
        _GameControler._StopTime = true;
        _UIManager.ResetImgPick();
    }
    //---------------------------------------	
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        power = 5;
        while (((new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power).sqrMagnitude > 450f) power -= 0.05f;
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;//*ball.rigidbody.mass;
    }
    //---------------------------------------	
    // It displays projectile trajectory path
    //---------------------------------------	
    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;

        fTime += 0.1f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
