using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private int _speed = 4;
    private int _outOfBounds = -6;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        MoveToRandomPosition();
    }

    void Update()
    {
        MoveEnemy();
        MoveOutOfBounds();
    }

    void MoveEnemy()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void MoveOutOfBounds()
    {
        if (transform.position.y < _outOfBounds) {
            MoveToRandomPosition();
        }
    }

    void MoveToRandomPosition()
    {
        transform.position = new Vector3(Random.Range(-9.20f, 9.20f), 8, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        if (tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
            Destroy(this.gameObject);
        }
        else if (tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            Destroy(this.gameObject);
        }
    }
}
