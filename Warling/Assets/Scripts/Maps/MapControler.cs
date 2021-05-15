using UnityEngine;
using System.Collections;

public class MapControler : MonoBehaviour {

    public bool _IsMove = true;//không cho phép di chuyển 
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 map_position = Vector3.zero;
    public float _Speed = 2f;
    GameControler _GameControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_GameControler._GameState._IsGamePlay) return;
        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
        GameObject dart = GameObject.FindGameObjectWithTag("Dart");
        GameObject Tar = null;
        if (bullet != null) Tar = bullet;
        if (dart != null) Tar = dart;
        if (Tar != null) MoveMapToBullet(Tar.transform.position);
        if (!_IsMove) return;
        if (Input.GetMouseButtonDown(0))
        {
            hit_position = Input.mousePosition;
            map_position = transform.position;

        }
        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
        }
	}
    void LeftMouseDrag()
    {
        // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
        // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
        current_position.z = hit_position.z = map_position.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways.  
        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

        // Invert direction to that terrain appears to move with the mouse.
        direction = (direction * -1)*_Speed;

        Vector3 position = map_position + direction;
        Vector2 size=gameObject.GetComponent<BoxCollider2D>().size*gameObject.transform.localScale.x;
        if (position.x + size.x / 2 <= (Helper.OrthographicBounds(Camera.main).size.x / 2 + Camera.main.transform.position.x))
        {
            position.x = (Helper.OrthographicBounds(Camera.main).size.x / 2 + Camera.main.transform.position.x) - size.x / 2;
        }

        if (position.x - size.x / 2 >= (Camera.main.transform.position.x - Helper.OrthographicBounds(Camera.main).size.x / 2))
        {
            position.x = (Camera.main.transform.position.x - Helper.OrthographicBounds(Camera.main).size.x / 2) + size.x / 2;
        }
        //====
        if (position.y + size.y / 2 <= (Helper.OrthographicBounds(Camera.main).size.y / 2 + Camera.main.transform.position.y))
        {
            position.y = (Helper.OrthographicBounds(Camera.main).size.y / 2 + Camera.main.transform.position.y) - size.y / 2;
        }

        if (position.y - size.y / 2 >= (Camera.main.transform.position.y - Helper.OrthographicBounds(Camera.main).size.y / 2))
        {
            position.y = (Camera.main.transform.position.y - Helper.OrthographicBounds(Camera.main).size.y / 2) + size.y / 2;
        }
        transform.position = position;
    }
    /// <summary>
    /// Di chuyển map theo đạn
    /// </summary>
    public void MoveMapToBullet(Vector3 _target)
    {
        Vector3 position = _target;
        position.z = gameObject.transform.position.z;
        Vector2 size = gameObject.GetComponent<BoxCollider2D>().size * gameObject.transform.localScale.x;
        if (position.x + size.x / 2 <= (Helper.OrthographicBounds(Camera.main).size.x / 2 + Camera.main.transform.position.x))
        {
            position.x = (Helper.OrthographicBounds(Camera.main).size.x / 2 + Camera.main.transform.position.x) - size.x / 2;
        }

        if (position.x - size.x / 2 >= (Camera.main.transform.position.x - Helper.OrthographicBounds(Camera.main).size.x / 2))
        {
            position.x = (Camera.main.transform.position.x - Helper.OrthographicBounds(Camera.main).size.x / 2) + size.x / 2;
        }
        //====
        if (position.y + size.y / 2 <= (Helper.OrthographicBounds(Camera.main).size.y / 2 + Camera.main.transform.position.y))
        {
            position.y = (Helper.OrthographicBounds(Camera.main).size.y / 2 + Camera.main.transform.position.y) - size.y / 2;
        }

        if (position.y - size.y / 2 >= (Camera.main.transform.position.y - Helper.OrthographicBounds(Camera.main).size.y / 2))
        {
            position.y = (Camera.main.transform.position.y - Helper.OrthographicBounds(Camera.main).size.y / 2) + size.y / 2;
        }
        transform.position = position;
    }
}
