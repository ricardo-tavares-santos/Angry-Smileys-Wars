using UnityEngine;
using System.Collections;

public class BulletPoison : MonoBehaviour {

    public GameObject _Poison;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player" || other.gameObject.tag=="Enemy")
        {
            GameObject poison = (GameObject)Instantiate(_Poison, other.gameObject.transform.position, Quaternion.identity);
            poison.transform.parent = other.gameObject.transform;
            Player player = other.gameObject.GetComponent<Player>();
            player._IsPoison = true;
        }
    }
}
