using UnityEngine;
using System.Collections;

public class Glow_HealthControler : MonoBehaviour {

    public GameObject particle_cross;
    public Transform[] _PosParticle;
    float _TimeCount = 0;
    float _TimeAddHealth = 0;
    int _Health = 70;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _TimeCount += Time.deltaTime;
        if (_TimeCount > 0.5f)
        {
            _TimeCount = 0;
            int ran = Random.Range(0, _PosParticle.Length);
            Instantiate(particle_cross, _PosParticle[ran].transform.position, Quaternion.identity);
        }
        _TimeAddHealth += Time.deltaTime;
        if (_TimeAddHealth>0.2f && _Health>0)
        {
            _TimeAddHealth = 0;
            _Health -= 2;
            Player player = gameObject.GetComponentInParent<Player>();
            player._Health += 2;
            if (player._Health>100)
            {
                player._Health = 100;
            }
            NumberText textHealth = player.gameObject.GetComponentInChildren<NumberText>();
            textHealth.SetNumberText(player._Health);
            GameControler gameControler=FindObjectOfType<GameControler>();
            if (_Health <= 0) {
                if (gameControler._GameObj.tag == "Enemy")
                {
                    gameControler.ChangeTurn();
                }
                Destroy(gameObject);
            } 
        }
    }
}
