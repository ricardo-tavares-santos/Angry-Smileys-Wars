using UnityEngine;
using System.Collections;

public class LeafFall_Controler : MonoBehaviour {
    private float _speed=0;//Tốc độ rơi
    private int _dir;//Hướng quay
    private Bounds _bound;
	// Use this for initialization
	void Start () {
        _bound = Helper.OrthographicBounds(Camera.main);
        _speed = Random.Range(1f,3f);
        _dir = Random.Range(0,2);
        if (_dir == 1) _dir = 1;
        else _dir = -1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, Vector3.forward * _dir, 10*Time.deltaTime);
        transform.Translate(Vector3.down * _speed*Time.deltaTime);
        if (transform.position.y < Camera.main.transform.position.y - _bound.size.y/2 || transform.position.x < Camera.main.transform.position.x - _bound.size.x/2 || transform.position.x > Camera.main.transform.position.x + _bound.size.x/2 || transform.position.y > Camera.main.transform.position.y + _bound.size.y/2)
        {
            Destroy(gameObject);
        }
	}
}
