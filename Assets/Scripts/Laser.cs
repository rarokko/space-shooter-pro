using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int _speed = 8;
    private int _outOfBounds = 8;

    void Update()
    {
        MoveLaser();
        DeleteOutOfBounds();
    }

    void MoveLaser()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    void DeleteOutOfBounds()
    {
        if (transform.position.y < _outOfBounds) {
            return;
        }

        Transform parentContainer = transform.parent;

        if (parentContainer) {
            Destroy(parentContainer.gameObject);
        }

        Destroy(this.gameObject);
    }
}
