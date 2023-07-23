using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Score : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.IsGameOver)
        {
            return;
        }

        Bird _bird = other.GetComponent<Bird>();

        if (_bird != null)
        {
            GameManager.score += 10;
            GameManager.instance.scoreText.text = "SCORE : " + GameManager.score.ToString();
        }
    }
}