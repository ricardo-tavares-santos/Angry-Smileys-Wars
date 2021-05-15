﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pick_FlamethrowerControler : MonoBehaviour {
    public GameObject collier;
    public GameObject TrajectoryPointPrefeb;
    public Transform[] _PosInitBullet;//Vị trí khởi tạo đạn
    public int numOfTrajectoryPoints = 30;
    public float _Angle;//Góc sai khác của súng so vs goc ban đầu
    public int _NumTurn = 1;//Số turn của khẩu súng 
    private bool isPressed, isBallThrown;
    private float power = 5;
    private List<GameObject> trajectoryPoints;
    GameControler _GameControler;
    private float _scale = 0.1f;
    UIManager _UIManager;
    float _TimeCount = 0;
    SoundControler _SoundControler;
   
    //---------------------------------------	
    void Start()
    {
        //======
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
        if (isBallThrown && _TimeCount<4f)
        {
            _TimeCount += Time.deltaTime;
            collier.SetActive(true);
        }
        if (_TimeCount > 4f)
        {
            _GameControler.ChangeTurn();
            Destroy(gameObject);
        }
        //==============
        if (_GameControler._CheckHit) return;
        if (_GameControler._TypeGame == 0 && _GameControler._GameState._IsEnemyStart) return;
        if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).y < _GameControler.INood.transform.position.y || _UIManager._ListPopup[0].activeSelf) && !isPressed) return;

        if (isBallThrown)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
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
            Player player = gameObject.GetComponentInParent<Player>();
            player.RotateRight();
            Vector3 vel = GetForceFrom(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));//Vector vận tốc ban đầu
            PickRotate(vel);
            setTrajectoryPoints(transform.position, vel);
            //
            //Transform tf = collier.GetComponentInChildren<Transform>();
            //float angleZ = tf.eulerAngles.z;
            //angleZ = (360 - transform.eulerAngles.z) - 180;
            ////tf.eulerAngles = new Vector3(0, 0, angleZ);
            //Debug.Log("eulerAngles:" + transform.eulerAngles.z + " eulerAngles: " + angleZ);
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
    private void throwBall()
    {
        _SoundControler.PlayFireSound(true);
        if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[16].Ammo -= 1; else _GameControler._ListStatePickEnemy[16].Ammo -= 1;
        //RaycastHit2D hitD = Physics2D.Raycast(rayOrigin, new Vector3(0, -1, 0), rayLengthHor, collisionMask);
        //Debug.DrawRay(rayOrigin, new Vector3(0, 1, 0) * rayLengthHor, Color.red);
        Animator ani = gameObject.GetComponentInChildren<Animator>();
        ani.SetBool("start", true);
        _NumTurn--;
        if (_NumTurn == 0)
        {
            isBallThrown = true;
            ///Xóa đường chỉ dẫn
            GameObject[] arrObj = GameObject.FindGameObjectsWithTag("Dot");
            for (int i = 0; i < arrObj.Length; i++) Destroy(arrObj[i]);
            //===================
        }
        _GameControler._CheckHit = true;
        _GameControler._StopTime = true;
        _UIManager.ResetImgPick();
    }
    private void InitBullet(bool isFinish)
    {
        //GameObject bullet = (GameObject)Instantiate(_Butlet, _PosInitBullet[0].position, Quaternion.Euler(_PosInitBullet[1].position - _PosInitBullet[0].position));
        //BulletCollision2D collision = bullet.GetComponentInChildren<BulletCollision2D>();
        //collision._BulletFinish = isFinish;
        //bullet.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(_PosInitBullet[0].position, _PosInitBullet[1].position), ForceMode2D.Impulse);
    }
    public void throwBallEnemy(Vector3 target)
    {
        Player player = gameObject.GetComponentInParent<Player>();
        player.RotateRight();
        _SoundControler.PlayFireSound(true);
        if (_GameControler._GameObj.tag == "Player") _GameControler._ListStatePickPlayer[16].Ammo -= 1; else _GameControler._ListStatePickEnemy[16].Ammo -= 1;
        Vector3 vel = target - transform.position;
        PickRotate(vel);
        Animator ani = gameObject.GetComponentInChildren<Animator>();
        ani.SetBool("start", true);
        //	ball.GetComponent<Rigidbody2D>().useGravity = true;
        _NumTurn--;
        if (_NumTurn == 0)
        {
            isBallThrown = true;
        }
        _GameControler._CheckHit = true;
        _GameControler._StopTime = true;
        _UIManager.ResetImgPick();
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
