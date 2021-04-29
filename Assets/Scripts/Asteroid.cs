using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{    
    private int _speed = 4;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _explosion;

    void Update()
    {
        RotateAsteroid();
    }

    void RotateAsteroid()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        if (tag == "Laser")
        {
            _spawnManager.StartSpawning();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            GameObject.Find("Audio_Manager").GetComponent<AudioManager>().Explosion();
            Destroy(this.gameObject, 0.2f);
        }
    }
}
