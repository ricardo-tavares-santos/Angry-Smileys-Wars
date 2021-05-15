using UnityEngine;
using System.Collections;

public class Glow_PoisonControler : MonoBehaviour {

    public GameObject _Particle_skull_small;
    public Transform[] _PosParticle;
    float _TimeCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _TimeCount += Time.deltaTime;
        if (_TimeCount>0.5f)
        {
            _TimeCount = 0;
            int ran = Random.Range(0,_PosParticle.Length);
            Instantiate(_Particle_skull_small,_PosParticle[ran].transform.position,Quaternion.identity);
        }
	}
}
