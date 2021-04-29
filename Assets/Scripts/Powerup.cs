using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private int _speed = 3;
    private int _outOfBounds = -6;
    [SerializeField]
    private int powerupID;

    void Start()
    {
        MoveToRandomPosition();
    }

    void Update()
    {
        MovePowerUp();
        MoveOutOfBounds();
    }

    void MovePowerUp()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void MoveOutOfBounds()
    {
        if (transform.position.y < _outOfBounds) {
            Destroy(this.gameObject);
        }
    }

    void MoveToRandomPosition()
    {
        transform.position = new Vector3(Random.Range(-9.20f, 9.20f), 8, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        if (tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            AddPowerUpToPlayer(player);
        }
    }

    void AddPowerUpToPlayer(Player player) 
    {
        switch(powerupID)
        {
            case 0:
                player.GetTripleShot();
                break;
            case 1:
                player.GetSpeedUp();
                break;
            case 2:
                player.GetShield();
                break;
        }
        
        GameObject.Find("Audio_Manager").GetComponent<AudioManager>().PowerUp();
        Destroy(this.gameObject);
    }
}
