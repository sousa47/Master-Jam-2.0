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
        } 
        else if(collision.gameObject.CompareTag("Enemy") ) 
        {
            bulletBounces--;
            
            DroneBehaviour droneBehaviour = collision.gameObject.GetComponent<DroneBehaviour>();
            if(droneBehaviour != null) 
            {
                droneBehaviour.GetHit(damage);
            }
            TurretBehaviour turretBehaviour = collision.gameObject.GetComponent<TurretBehaviour>();
            if(turretBehaviour != null) 
            {
                turretBehaviour.GetHit(damage);
            }
        }

        if (bulletBounces < 0)
        {
            Destroy(gameObject);
        }

    }
}
