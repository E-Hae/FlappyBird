using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorzScroll : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }
}