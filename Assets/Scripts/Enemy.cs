using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    private int _speed = 4;
    private int _outOfBounds = -6;
    private Player _player;
    private Animator _animator;
    private AudioManager _audioManager;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = this.GetComponent<Animator>();
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
            DestroyEnemy();
        }
        else if (tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        _speed = 0;
        _animator.SetTrigger("onEnemyDeath");
        GameObject.Find("Audio_Manager").GetComponent<AudioManager>().Explosion();
        Destroy(this.gameObject, 2.5f);
    }
}
