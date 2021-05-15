using UnityEngine;
using System.Collections;

public class BulletControler : MonoBehaviour {

    bool _Check = false;//Kiểm tra trạng thái khi bắt đâu xử lý va chạm với vật khác (map,player,enemy)
	// Use this for initialization
    Vector3 _posOld = new Vector3();
	void Start () {
        _posOld = transform.position;
	}
	// Update is called once per frame
	void Update () {
       
        //Destroy(gameObject.GetComponent<PolygonCollider2D>());
        //PolygonCollider2D poly = gameObject.AddComponent<PolygonCollider2D>();
        Vector3 dir = transform.position - _posOld;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//Góc tạo giữa vel và trục Ox
        if (angle!=0)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            _posOld = transform.position;
        }
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
	}
}
