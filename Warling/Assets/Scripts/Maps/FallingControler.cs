using UnityEngine;
using System.Collections;

public class FallingControler : MonoBehaviour {

    public GameObject[] _Falling;
    public float _TimeInit=2;
    private float _timeCount = 0;
    Bounds _boundCamera;
  
	// Use this for initialization
	void Start () {
        _boundCamera = Helper.OrthographicBounds(Camera.main);
	}
	
	// Update is called once per frame
	void Update () {
        _timeCount += Time.deltaTime;
        if (_timeCount>_TimeInit)
        {
            _timeCount = 0;
            int ranFalling = Random.Range(0,_Falling.Length);
            Vector2 pos = new Vector2(Random.Range(Camera.main.transform.position.x - _boundCamera.min.x, Camera.main.transform.position.x + _boundCamera.min.x), Camera.main.transform.position.y + _boundCamera.max.y);
            Instantiate(_Falling[ranFalling], pos, Quaternion.identity);
        }
	}
}
