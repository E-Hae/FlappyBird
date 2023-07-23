using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatBackground : MonoBehaviour
{
    private BoxCollider2D _box;
    private float _groundHorizontalLength;

    // Use this for initialization
    private void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _groundHorizontalLength = _box.size.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < -_groundHorizontalLength)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        Vector2 _move = new Vector2(_groundHorizontalLength * 2, 0f);
        transform.position = (Vector2)transform.position + _move;
    }
}