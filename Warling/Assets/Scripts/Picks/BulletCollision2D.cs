using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BulletCollision2D : MonoBehaviour {

    public float RelativeVelocityRequired;
    public GameObject dot;
    public int _Turn = 1;
    public bool _DestroyParent = true;
    BoxCollider2D collider;
    const float skinWidth = 0.005f;
    float xMax, xMin, yMin, yMax;
    public bool _BulletFinish=false;
    public bool _CheckYMax = false;
    SoundControler _SoundControler;
    Vector3 _PosStart;



	// Use this for initialization
	void Start () {
        GameObject other = GameObject.FindGameObjectWithTag("Map_Front");
        xMax = other.transform.position.x + (other.GetComponent<BoxCollider2D>().size.x * other.transform.localScale.x)/2;
        xMin = other.transform.position.x - (other.GetComponent<BoxCollider2D>().size.x * other.transform.localScale.x)/2;
        yMin = other.transform.position.y - (other.GetComponent<BoxCollider2D>().size.y * other.transform.localScale.y)/2;
        yMax = other.transform.position.y + (other.GetComponent<BoxCollider2D>().size.y * other.transform.localScale.y)/2;
        collider = gameObject.GetComponent<BoxCollider2D>();
        _SoundControler = FindObjectOfType<SoundControler>();
        _PosStart = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (_CheckYMax && gameObject.transform.position.y > yMax)
        {
            CollionActive();
        }
        if (gameObject.transform.position.x > xMax || gameObject.transform.position.x < xMin || gameObject.transform.position.y < yMin)
        {
            CollionActive();
        }  
        UpdateSkin();
        //Destroy(gameObject.GetComponent<PolygonCollider2D>());
        //PolygonCollider2D poly = gameObject.AddComponent<PolygonCollider2D>();
	}
    /// <summary>
    /// KHi đạn va chạm player
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            if (gameObject.name=="CarrotSmall(Clone)")
            {
                if (Vector2.Distance(other.gameObject.transform.position, _PosStart) < 0.5f) return;
            }
            Player player = other.GetComponent<Player>();
            if (player._IsCurrent) return;
            if (dot.tag == "Explosion" || dot.tag == "Explosion_Bomp")
            {
                _SoundControler.PlayExplosionSound(true);
            }
            if (dot.tag == "Explosion_Poison")
            {
                _SoundControler.PlayGlassSound(true);
            }
            Instantiate(dot, transform.position, transform.rotation);
            CollionActive();
        }
        if (other.gameObject.tag == "Map")
        {
            if (dot.tag == "Explosion" || dot.tag == "Explosion_Bomp")
            {
                _SoundControler.PlayExplosionSound(true);
            }
            if (dot.tag == "Explosion_Poison")
            {
                _SoundControler.PlayGlassSound(true);
            }
            Instantiate(dot, transform.position, transform.rotation);
            CollionActive();
        }
    }
    
    /// <summary>
    /// Va chạm với map
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude >= RelativeVelocityRequired)
        {
            if (other.gameObject.tag == "Map")
            {
                var contact0 = other.contacts[0];//Vi tri tiep xuc
                if (dot.tag == "Explosion" || dot.tag == "Explosion_Bomp")
                {
                    _SoundControler.PlayExplosionSound(true);
                }
                if (dot.tag == "Explosion_Poison")
                {
                    _SoundControler.PlayGlassSound(true);
                }
                Instantiate(dot, contact0.point, transform.rotation);
                CollionActive();
            }

        }
    }
    private void CollionActive()
    {
        GameControler gameControler = FindObjectOfType<GameControler>();
        if(_Turn!=1)
        {
            gameControler.MoveCameraToObj();
        }
        if (!gameControler._GameState._IsChangding && _Turn == 1 && _BulletFinish) gameControler.ChangeTurn();
        if (_DestroyParent)
        {
            Destroy(gameObject.transform.parent.gameObject);// 
        }
        else Destroy(gameObject);
    }
    private void UpdateSkin()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
    }
}
