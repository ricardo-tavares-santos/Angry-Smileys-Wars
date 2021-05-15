using UnityEngine;
using System.Collections;

public class Pick_SwapControler : MonoBehaviour {

    public float _Speed = 3;
    GameControler _GameControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * _Speed);
	}
}
