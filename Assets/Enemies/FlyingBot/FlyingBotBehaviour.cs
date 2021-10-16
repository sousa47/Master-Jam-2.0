using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBotBehaviour : MonoBehaviour, Vision.ITrigger
{
    public int health = 7;

    public Animator bodyAnimator;
    public Vision vision;
    GameObject player;
    Rigidbody2D rb;
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
        this.rb = this.GetComponent<Rigidbody2D>();
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
        bodyAnimator.SetTrigger("Shoot");
        float angle = Vector2.SignedAngle(this.transform.position, this.player.transform.position);
        Vector2 newPoint = this.player.transform.position - this.transform.position;
        angle = EulerAngleFrom(newPoint) - 90;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
    }

    public void GetHit(int damage)
    {
        if(health <= 0) return;

        health -= damage;
        if(health > 0) 
        {
            bodyAnimator.SetTrigger("Hit");
        }
        else 
        {
            bodyAnimator.SetTrigger("Die");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    void Vision.ITrigger._OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            bodyAnimator.SetTrigger("Attention");
        }
    }

    void Vision.ITrigger._OnTriggerExit2D(Collider2D other)
    {

    }

    static public float EulerAngleFrom(Vector2 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
