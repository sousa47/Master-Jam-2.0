using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroneBehaviour : MonoBehaviour, Vision.ITrigger
{
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
        float angle = Vector2.Angle(this.player.transform.position, this.transform.position);
        Debug.Log(angle);
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));//firePoint.rotation);
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
}
