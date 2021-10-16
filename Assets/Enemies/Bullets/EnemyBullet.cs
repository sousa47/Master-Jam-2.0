using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Shoot");
        rb.velocity = transform.up * speed;
        Destroy(gameObject, 2.8f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Player") {

        }
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Explode");
        
    }
}
