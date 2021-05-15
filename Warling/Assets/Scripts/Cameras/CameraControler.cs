using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {

    public bool _IsMove = true;//không cho phép di chuyển 
    GameObject _Map;
    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;
    public float _Speed = 2f;
    GameControler _GameControler;
	// Use this for initialization
	void Start () {
        _GameControler = FindObjectOfType<GameControler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_Map==null)
        {
            _Map = GameObject.FindGameObjectWithTag("Map_Front");
            if (_Map == null) return;
        }
        if (!_GameControler._GameState._IsGamePlay) return;
        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
        GameObject dart = GameObject.FindGameObjectWithTag("Dart");
        GameObject Tar=null;
        if (bullet != null) Tar = bullet;
        if (dart != null) Tar = dart;
        if (Tar != null) MoveCameraToBullet(Tar.transform.position);
        if (!_IsMove) return;
        if (Input.GetMouseButtonDown(0))
        {
            hit_position = Input.mousePosition;
            camera_position = Helper.OrthographicBounds(Camera.main).center;// transform.position;
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
        current_position.z = hit_position.z = camera_position.y;

        // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
        // anyways.  
        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

        // Invert direction to that terrain appears to move with the mouse.
        direction = (direction * -1) * _Speed;

        Vector3 position = camera_position + direction;
        position.z = -10;
        Vector2 size = _Map.GetComponent<BoxCollider2D>().size * gameObject.transform.localScale.x;
        if (position.x + Helper.OrthographicBounds(Camera.main).size.x/2 >= (_Map.transform.position.x + size.x / 2))
        {
            position.x = (_Map.transform.position.x + size.x / 2) - Helper.OrthographicBounds(Camera.main).size.x / 2;
        }

        if (position.x - Helper.OrthographicBounds(Camera.main).size.x / 2 <= (_Map.transform.position.x - size.x / 2))
        {
            position.x = (_Map.transform.position.x - size.x / 2) + Helper.OrthographicBounds(Camera.main).size.x / 2;
        }
        ////====
        if (position.y + Helper.OrthographicBounds(Camera.main).size.y / 2 >= (_Map.transform.position.x + size.y / 2))
        {
            position.y = (_Map.transform.position.y + size.y / 2) - Helper.OrthographicBounds(Camera.main).size.y / 2;
        }

        if (position.y - Helper.OrthographicBounds(Camera.main).size.y / 2 <= (_Map.transform.position.y - size.y / 2))
        {
            position.y = (_Map.transform.position.y - size.y / 2) + Helper.OrthographicBounds(Camera.main).size.y / 2;
        }
        transform.position = position;
    }
    /// <summary>
    /// Di chuyển camera theo viên dạn
    /// </summary>
    public void MoveCameraToBullet(Vector3 _target)
    {
        Vector3 position = _target;
        position.z = Camera.main.transform.position.z;
        Vector2 size = _Map.GetComponent<BoxCollider2D>().size * gameObject.transform.localScale.x;
        if (position.x + Helper.OrthographicBounds(Camera.main).size.x / 2 >= (_Map.transform.position.x + size.x / 2))
        {
            position.x = (_Map.transform.position.x + size.x / 2) - Helper.OrthographicBounds(Camera.main).size.x / 2;
        }

        if (position.x - Helper.OrthographicBounds(Camera.main).size.x / 2 <= (_Map.transform.position.x - size.x / 2))
        {
            position.x = (_Map.transform.position.x - size.x / 2) + Helper.OrthographicBounds(Camera.main).size.x / 2;
        }
        ////====
        if (position.y + Helper.OrthographicBounds(Camera.main).size.y / 2 >= (_Map.transform.position.x + size.y / 2))
        {
            position.y = (_Map.transform.position.y + size.y / 2) - Helper.OrthographicBounds(Camera.main).size.y / 2;
        }

        if (position.y - Helper.OrthographicBounds(Camera.main).size.y / 2 <= (_Map.transform.position.y - size.y / 2))
        {
            position.y = (_Map.transform.position.y - size.y / 2) + Helper.OrthographicBounds(Camera.main).size.y / 2;
        }
        transform.position = position;
    }
}
