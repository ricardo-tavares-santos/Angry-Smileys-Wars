using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject[] _ListPick;//lưu trữ danh sách vũ khí
    GameControler _GameControler;
    public LayerMask collisionMask;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
	}
	
	// Update is called once per frame
	void Update () {
	}
    /// <summary>
    /// Thực hiện khi người chơi chọn vào một vũ khí nào đó
    /// </summary>
    public void UsePick(int index)
    {
        GameObject[] arrPickOld = GameObject.FindGameObjectsWithTag("Pick");
        for (int i = 0; i < arrPickOld.Length; i++)
        {
            Pick_ShieldControler pick = arrPickOld[i].GetComponent<Pick_ShieldControler>();
            Pick_MineControler pickMine = arrPickOld[i].GetComponent<Pick_MineControler>();
            if (pick == null && pickMine == null) Destroy(arrPickOld[i]);
            if (pickMine!=null)
            {
                if (!pickMine._IsInit)
                {
                    Destroy(arrPickOld[i]);
                }
            }
        }
        GameObject[] arrBullet = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < arrBullet.Length; i++) { Destroy(arrBullet[i]); }

        GameObject dart = GameObject.FindGameObjectWithTag("Dart");
        if (dart != null) Destroy(dart);
        //======================
        Player player = new Player();
        Object obj = new Object();
        _GameControler._Pick = _ListPick[index];
        player = _GameControler._GameObj.GetComponent<Player>();
        if (index == 3 || index == 9 || index == 14 || index == 15)
        {
            obj = Instantiate(_ListPick[18], _GameControler._GameObj.transform.position, Quaternion.identity);
            AirCallControler air=((GameObject)obj).GetComponent<AirCallControler>();
            switch(index)
            {
                case 3: air._Type = "Pick_Bomb"; break;
                case 9: air._Type = "Pick_Teleport"; break;
                case 14: air._Type = "Pick_Health"; break;
                case 15: air._Type = "Pick_Swap"; break;
            }
        }
        else
        {
            FindObjectOfType<CameraControler>()._IsMove = false;
            FindObjectOfType<MapControler>()._IsMove = false;
            obj = Instantiate(_ListPick[index], _GameControler._GameObj.transform.position, Quaternion.identity);  
        }
        if (player._Dir == Player.Dir.right)
        {
            ((GameObject)obj).transform.localScale = new Vector3(1, 1, 1);
        }
        else ((GameObject)obj).transform.localScale = new Vector3(-1, 1, 1);
        ((GameObject)obj).transform.parent = _GameControler._GameObj.transform;
        //Hiển thị súng
    }
    /// <summary>
    /// Check khởi tạo player
    /// </summary>
    public bool CheckInitPlayer(Vector3 target)
    {
        //Check xem dưới 1 vị trí khởi tạo có map ko, có vị trí đứng hay không
        float directionX = Mathf.Sign((target - transform.position).x);
        for (int i = 0; i < 4; i++)
        {
            Vector2 rayOrigin = target;
            // rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            float rayLengthHor = 10;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, new Vector3(0, -1, 0), rayLengthHor, collisionMask);

            Debug.DrawRay(rayOrigin, new Vector3(0, 1, 0) * rayLengthHor, Color.red);
            if (hit)
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckPosXPlayer(float posX,List<float> listX,float dis)
    {
        for (int i = 0; i < listX.Count; i++)
        {
            if (posX > listX[i] - dis && posX <listX[i]+dis)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Check vi tri khoi tao doi tuong di chuyen
    /// </summary>
    public bool CheckTeleportPlayer(Vector3 target)
    {
        //Check xem dưới 1 vị trí khởi tạo có map ko, có vị trí đứng hay không
        float directionX = Mathf.Sign((target - transform.position).x);
        for (int i = 0; i < 4; i++)
        {
            Vector2 rayOrigin = target;
            // rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            float rayLengthHor = 10;
            RaycastHit2D hitD = Physics2D.Raycast(rayOrigin, new Vector3(0, -1, 0), rayLengthHor, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector3(0, 1, 0) * rayLengthHor, Color.red);
            RaycastHit2D hitL = Physics2D.Raycast(rayOrigin, new Vector3(-1, 0, 0), rayLengthHor, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector3(-1, 0, 0) * rayLengthHor, Color.red);
            RaycastHit2D hitR = Physics2D.Raycast(rayOrigin, new Vector3(1, 0, 0), rayLengthHor, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector3(1, 0, 0) * rayLengthHor, Color.red);
            RaycastHit2D hitT = Physics2D.Raycast(rayOrigin, new Vector3(0, 1, 0), rayLengthHor, collisionMask);
            Debug.DrawRay(rayOrigin, new Vector3(0, 1, 0) * rayLengthHor, Color.red);
            if (hitD && hitL && hitR && hitT)
            {
                return true;
            }
        }
        return false;
    }
}
