using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShotGunControler : MonoBehaviour {

    public GameObject TrajectoryPointPrefeb;
    public GameObject BallPrefb;
    public int numOfTrajectoryPoints = 30;
    public int _NumShot;//Số nòng súng có thể là 3||5
    public GameObject[] _listShot;//ListCac nòng súng
    public float _Angle;//Góc sai khác của súng so vs goc ban đầu
    public int _NumTurn = 1;//Số turn của khẩu súng 
    private GameObject[] ball;
    private bool isPressed, isBallThrown;
    private float power = 5;
    private List<GameObject> trajectoryPoints;
    GameControler _GameControler;
    private float _scale = 0.1f;
    private Animator _anim;
    UIManager _UIManager;
    SoundControler _SoundControler;
    //---------------------------------------	
    void Start()
    {
        trajectoryPoints = new List<GameObject>();
        _GameControler = FindObjectOfType<GameControler>();
        _anim = gameObject.GetComponentInChildren<Animator>();
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
        if (_GameControler._StopTime) return;
        if (_GameControler._TypeGame == 0 && _GameControler._GameState._IsEnemyStart) return;
        if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).y < _GameControler.INood.transform.position.y || _UIManager._ListPopup[0].activeSelf) && !isPressed) return;
        if (isBallThrown)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (ball==null)
                createBall();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            if (!isBallThrown)
            {
                throwBall();
            }
        }
        if (isPressed)
        {
            Vector3 vel = GetForceFrom(ball[0].transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));//Vector vận tốc ban đầu
            PickRotate(vel);
            setTrajectoryPoints(transform.position, vel);
        }
    }
    private void PickRotate(Vector3 vel)
    {
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;//Góc tạo giữa vel và trục Ox
        Player player = _GameControler._GameObj.GetComponent<Player>();
        angle += _Angle;
        if (player._Dir == Player.Dir.left)
        {
            if (angle % 360 > -90 && angle % 360 < 90)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else transform.localScale = new Vector3(1, 1, 1);
            angle = 360 - angle + 180;
        }
        else
        {
            if (angle % 360 > 90 || angle % 360 < -90)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else transform.localScale = new Vector3(1, 1, 1);
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    //---------------------------------------	
    // When ball is thrown, it will create new ball
    //---------------------------------------	
    private void createBall()
    {
        if (_NumShot == 3) ball = new GameObject[3]; else ball = new GameObject[5];
        ball[0] = (GameObject)Instantiate(BallPrefb);
        BulletCollision2D bulletCo = ball[0].GetComponentInChildren<BulletCollision2D>(); bulletCo._Turn = _NumTurn;
        ball[1] = (GameObject)Instantiate(BallPrefb);
        BulletCollision2D bulletC1 = ball[1].GetComponentInChildren<BulletCollision2D>(); bulletC1._Turn = _NumTurn;
        ball[2] = (GameObject)Instantiate(BallPrefb);
        BulletCollision2D bulletC2 = ball[2].GetComponentInChildren<BulletCollision2D>(); bulletC2._Turn = _NumTurn;
        Vector3 pos = transform.position;
        pos.z = 1;
        ball[0].transform.position = ball[1].transform.position = ball[2].transform.position = pos;
        ball[0].SetActive(false); ball[1].SetActive(false); ball[2].SetActive(false);
        if (_NumShot==5)
        {
            ball[3] = (GameObject)Instantiate(BallPrefb);
            BulletCollision2D bulletC3 = ball[3].GetComponentInChildren<BulletCollision2D>(); bulletC3._Turn = _NumTurn;
            ball[4] = (GameObject)Instantiate(BallPrefb);
            BulletCollision2D bulletC4 = ball[4].GetComponentInChildren<BulletCollision2D>(); bulletC4._Turn = _NumTurn;
            ball[3].transform.position = ball[4].transform.position = pos;
            ball[3].SetActive(false); ball[4].SetActive(false);
        }
    }
    //---------------------------------------	
    private void throwBall()
    {
        _SoundControler.PlayShotgun_shotSound(true);
        _anim.Play("Hotgun");
        ball[0].SetActive(true); ball[1].SetActive(true); ball[2].SetActive(true);
        ball[0].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[0].transform.position, _listShot[0].transform.position), ForceMode2D.Impulse);
        ball[1].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[1].transform.position, _listShot[1].transform.position), ForceMode2D.Impulse);
        ball[2].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[2].transform.position, _listShot[2].transform.position), ForceMode2D.Impulse);
        if (_NumShot==5)
        {
            ball[3].SetActive(true); ball[4].SetActive(true);
            ball[3].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[3].transform.position, _listShot[3].transform.position), ForceMode2D.Impulse);
            ball[4].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[4].transform.position, _listShot[4].transform.position), ForceMode2D.Impulse);
        }
        //ball.GetComponent<Rigidbody2D>().useGravity = true;
        _GameControler._CheckHit = true;
        _NumTurn--;
        ball = null;
        if (_NumTurn!=0)
        {
            for (int i = 0; i < numOfTrajectoryPoints; i++)
            {
                trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
            }
        }
        if (_NumTurn==0)
        {
            isBallThrown = true;
            ///Xóa đường chỉ dẫn
            GameObject[] arrObj = GameObject.FindGameObjectsWithTag("Dot");
            for (int i = 0; i < arrObj.Length; i++) Destroy(arrObj[i]);
            _GameControler._StopTime = true;
            _UIManager.ResetImgPick();
        }

    }
    public void throwBallAIEnemy(Vector3 target)
    {
        _SoundControler.PlayShotgun_shotSound(true);
        _anim.Play("Hotgun");
        Vector3 vel = target - gameObject.transform.position;
        PickRotate(vel);
        if (ball == null)
            createBall();
        ball[0].SetActive(true); ball[1].SetActive(true); ball[2].SetActive(true);
        ball[0].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[0].transform.position, _listShot[0].transform.position), ForceMode2D.Impulse);
        ball[1].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[1].transform.position, _listShot[1].transform.position), ForceMode2D.Impulse);
        ball[2].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[2].transform.position, _listShot[2].transform.position), ForceMode2D.Impulse);
        if (_NumShot == 5)
        {
            ball[3].SetActive(true); ball[4].SetActive(true);
            ball[3].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[3].transform.position, _listShot[3].transform.position), ForceMode2D.Impulse);
            ball[4].GetComponent<Rigidbody2D>().AddForce(GetForceFrom(ball[4].transform.position, _listShot[4].transform.position), ForceMode2D.Impulse);
        }

        //	ball.GetComponent<Rigidbody2D>().useGravity = true;
        _NumTurn--;
        ball = null;
        if (_NumTurn != 0)
        {
            for (int i = 0; i < numOfTrajectoryPoints; i++)
            {
                trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
            }
        }
        if (_NumTurn == 0)
        {
            isBallThrown = true;
            _GameControler._CheckHit = true;
            _GameControler._StopTime = true;
            _UIManager.ResetImgPick();
        }
    }
    //---------------------------------------	
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        power = 5;
        while (((new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power).sqrMagnitude < 30f) power += 0.05f;
        while (((new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power).sqrMagnitude > 35f) power -= 0.05f;
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
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = pos;
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
