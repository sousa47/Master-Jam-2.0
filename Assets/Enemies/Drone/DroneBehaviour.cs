using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroneBehaviour : MonoBehaviour, Vision.ITrigger
{

    public int health = 100;

    public Animator animator;
    public Animator bodyAnimator;
    public Vision vision;
    GameObject player;
    bool faceRight = false;

    float timerToShoot;
    float nextShootTime;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RestartShoot();
        this.player = GameObject.FindWithTag("Player");
        this.vision.iTrigger = this;
    }

    private void RestartShoot()
    {
        timerToShoot = 0f;
        nextShootTime = Random.Range(1f,3.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if(!vision.onSight) return;

        timerToShoot +=Time.deltaTime;
        if(timerToShoot > nextShootTime) 
        {
            Shoot();
            Invoke("Shoot", 0.2f);
            Invoke("Shoot", 0.4f);
            RestartShoot();
        }

        if( (player.transform.position.x < this.transform.position.x && faceRight) ||
            (player.transform.position.x > this.transform.position.x && !faceRight) ) 
        {
            faceRight = !faceRight;
            bodyAnimator.SetTrigger("Turn");
        }
    }

    void Shoot()
    {
        float angle = Vector2.SignedAngle(this.transform.position, this.player.transform.position);
        Vector2 newPoint = this.player.transform.position - this.transform.position;
        angle = EulerAngleFrom(newPoint) - 90;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
    }

    public void GetHit(int damage)
    {
        health -= damage;
        if(health > 0) 
        {

        }
        else 
        {
            bodyAnimator.SetTrigger("Die");
        }
    }
    void Vision.ITrigger._OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            animator.SetTrigger("Attention");
        }
    }

    void Vision.ITrigger._OnTriggerExit2D(Collider2D other)
    {

    }

    static public float EulerAngleFrom(Vector2 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
