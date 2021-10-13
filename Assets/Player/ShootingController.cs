using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public float fireDelay;
    private float currentFireDelay = 0;

    public GameObject bullet;
    public Transform bulletSpawnPosition;

    private float verticalDirectionInput;

    public float bulletImpulseForce;

    private void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            Fire();
        }

        verticalDirectionInput = Input.GetAxisRaw("Vertical");

        if (currentFireDelay > 0)
        {
            currentFireDelay -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (currentFireDelay <= 0)
        {
            currentFireDelay = fireDelay;
            GameObject shot = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity);

            if (Mathf.Abs(verticalDirectionInput) > 0.5f)
            {
                if (verticalDirectionInput > 0)
                {
                    shot.GetComponent<PlayerBullet>().movementDirection = Vector2.up;
                }
                else
                {
                    shot.GetComponent<PlayerBullet>().movementDirection = Vector2.down;
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletImpulseForce, ForceMode2D.Impulse);
                }

            }
            else
            {
                shot.GetComponent<PlayerBullet>().movementDirection = new Vector2(transform.localScale.x, 0);
            }
            
            Destroy(shot);
        }
    }
}
