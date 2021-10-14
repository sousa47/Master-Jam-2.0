using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [Header("Player Upgrades")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Ranged Upgrades")]
    public float fireRate;
    public int rangedWeaponDamage;
    public float bulletSpeed;
    public bool piercingShots;
    public int bulletBounces;
    public int bulletsPerShot;

    [Header("Melee Upgrades")]
    public int meleeWeaponDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpgradePlayer(collision.GetComponent<PlayerController>(), collision.GetComponent<ShootingController>());
            Destroy(gameObject);
        }
    }

    private void UpgradePlayer(PlayerController pc, ShootingController sc)
    {
        // Player upgrades
        pc.moveSpeed += movementSpeed;
        pc.jumpForce += jumpForce;

        // Ranged weapons upgrades
        sc.fireDelay *= fireRate;
        sc.bulletDamage += rangedWeaponDamage;
        sc.bulletSpeed += bulletSpeed;
        if (piercingShots) sc.piercingShots = true;
        sc.bulletBounces += bulletBounces;
        sc.bulletsPerShot = bulletsPerShot;

        // Melee weapon upgrades


    }
}
