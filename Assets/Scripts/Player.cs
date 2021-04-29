using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _verticalBoundary = -3.8f;
    private float _horizontalBoundary = 11.3f;
    private float _fireCooldownTimer = -1f;
    private float _fireRate = 0.3f;
    [SerializeField]
    private int _lives = 3;
    private bool _tripleShotActive = false;
    private bool _speedUpActive = false;
    private bool _shieldActive = false;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private int _score;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftWing;
    [SerializeField]
    private GameObject _rightWing;
    [SerializeField]
    private AudioSource _laserSound;
    [SerializeField]
    private PlayerInput _input;
    private Vector3 _movementAxis;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        CalculateMovement();
    }

    void FireLaser()
    {
        if (Time.time > _fireCooldownTimer)
        {
            _fireCooldownTimer = Time.time + _fireRate;
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + 1.0f, 0);
            GameObject laserObject = _tripleShotActive ? _tripleShotPrefab : _laserPrefab;
            Instantiate(laserObject, laserPosition, Quaternion.identity);
            _laserSound.Play();
        }
    }

    public void GetAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            FireLaser();
        }
    }

    public void GetInput(InputAction.CallbackContext value)
    {
        Vector2 _direction = value.ReadValue<Vector2>();
        _movementAxis = new Vector3(_direction.x, _direction.y, 0);
    }

    

    void CalculateMovement()
    {
        Vector3 direction = _speedUpActive ? _movementAxis * 2 : _movementAxis;
        transform.Translate(direction * _speed * Time.deltaTime);

        float horizontalPosition = transform.position.x;
        float verticalPosition = transform.position.y;
        float verticalValue = Mathf.Clamp(verticalPosition, _verticalBoundary, 0);

        transform.position = new Vector3(horizontalPosition, verticalValue, 0);
        
        if (horizontalPosition >= _horizontalBoundary) 
        {
            transform.position = new Vector3(-_horizontalBoundary, verticalPosition, 0);
        } 
        else if (horizontalPosition <= -_horizontalBoundary) 
        {
            transform.position = new Vector3(_horizontalBoundary, verticalPosition, 0);
        }
    }

    public void Damage() 
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shield.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2) {
            _leftWing.SetActive(true);
        } else if (_lives == 1) {
            _rightWing.SetActive(true);
        }

        if (_lives < 0) {
            _spawnManager.StopSpawning();
            GameObject.Find("Audio_Manager").GetComponent<AudioManager>().Explosion();
            _uiManager.SetGameOver();
            Destroy(this.gameObject);
        } else {
            _uiManager.UpdateLives(_lives);
        }
    }

    public void GetTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(RemoveTripleShotRoutine());
    }

    IEnumerator RemoveTripleShotRoutine()
    {
        yield return new WaitForSeconds(5);
        _tripleShotActive = false;
    }

    public void GetSpeedUp()
    {
        _speedUpActive = true;
        StartCoroutine(RemoveSpeedUpRoutine());
    }

    IEnumerator RemoveSpeedUpRoutine()
    {
        yield return new WaitForSeconds(5);
        _speedUpActive = false;
    }

    public void GetShield()
    {
        _shield.SetActive(true);
        _shieldActive = true;
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }
}
