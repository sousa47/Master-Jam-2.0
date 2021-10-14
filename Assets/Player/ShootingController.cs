using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public float fireDelay;
    private float currentFireDelay = 0;

    public int bulletsPerShot;
    public float shootSpreadAngle;

    public GameObject bullet;
    public Transform bulletSpawnPosition;

    private float verticalDirectionInput;

    public float bulletImpulseForce;

    [Header("Bullet Parameters")]
    public int bulletDamage;
    public float bulletSpeed;
    public bool piercingShots;
    public int bulletBounces;

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

            List<PlayerBullet> bulletsFired = new List<PlayerBullet>(); // Instantiate bullets and add them to list
            for(int i = 0; i < bulletsPerShot; i++)
            {
                PlayerBullet newBullet = Instantiate(bullet, bulletSpawnPosition.position, Quaternion.identity).GetComponent<PlayerBullet>();
                bulletsFired.Add(newBullet);

                // set parameters
                newBullet.damage = bulletDamage;
                newBullet.movementSpeed = bulletSpeed;
                newBullet.piercingShot = piercingShots;
                newBullet.bulletBounces = bulletBounces;
            }

            // set movement angle
            if (bulletsFired.Count == 1)
            {
                bulletsFired[0].movementDirection = Vector2.right;
            }
            else
            {
                float angleDif = shootSpreadAngle / (bulletsFired.Count - 1);
                for (int i = 0; i < bulletsFired.Count; i++)
                {
                    bulletsFired[i].movementDirection = Quaternion.Euler(0, 0, angleDif * (i) + 90 - (shootSpreadAngle / 2)) * Vector2.down;
                }
            }

            foreach(PlayerBullet pb in bulletsFired)
            {
                if (Mathf.Abs(verticalDirectionInput) > 0.5f)
                {
                    if (verticalDirectionInput > 0) // shoot up
                    {
                        pb.movementDirection = Quaternion.Euler(0, 0, 90) * pb.movementDirection;
                        
                    }
                    else    // shoot down
                    {
                        pb.movementDirection = Quaternion.Euler(0, 0, -90) * pb.movementDirection;
                        GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletImpulseForce / bulletsFired.Count, ForceMode2D.Impulse);
                    }
                }
                else    // shoot straight
                {
                    pb.movementDirection = new Vector2(pb.movementDirection.x * transform.localScale.x, pb.movementDirection.y);
                }
            }
        }
    }
}
