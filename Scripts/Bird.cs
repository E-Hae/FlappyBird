using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public Animator animator;

    public float upForce = 200f;
    private SpriteRenderer spriteRenderer;

    private int _life;

    // Use this for initialization
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;

        _life = 3;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                return;
            }

            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * upForce);
            animator.SetTrigger("Click");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.IsGameOver)
        {
            return;
        }

        if (collision.gameObject.GetComponent<RepeatBackground>() != null)
        {
            Die();
        }
    }

    public void Hit()
    {
        _life--;

        GameManager.instance.lifeText.text = "LIFE : " + _life.ToString();

        if (_life == 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(SturnBirdCoroutine());
        }
    }

    public void Die()
    {
        animator.SetTrigger("SetDie");
        GameManager.instance.IsGameOver = true;
    }

    public IEnumerator SturnBirdCoroutine()
    {
        int i = 2;
        while (i > 0)
        {
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            i--;
        }
    }
}