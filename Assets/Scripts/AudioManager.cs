using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _explosion;
    [SerializeField]
    private AudioSource _powerUp;

    public void Explosion()
    {
        _explosion.Play();
    }

    public void PowerUp()
    {
        _powerUp.Play();
    }
}
