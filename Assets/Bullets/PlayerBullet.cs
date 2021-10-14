using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector2 movementDirection;
    public float movementSpeed;
    public int damage;
    public int bulletBounces;
    public bool piercingShot;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = movementDirection * movementSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bulletBounces--;
            if (bulletBounces < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
