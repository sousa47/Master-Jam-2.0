using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour, Vision.ITrigger
{
    public int health = 7;
    public int damage = 4;
    public float minShootTime = 1f;
    public float maxShootTime = 3.5f;

    public Animator bodyAnimator;
    public Vision vision;
    GameObject player;
    Rigidbody2D rb;
    bool faceRight = false;

    float timerToShoot;
    float nextShootTime;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public EnemyBullet enemyBullet;
    public AudioSource deadSound;

    // Start is called before the first frame update
    void Start()
    {
        this.deadSound = this.GetComponent<AudioSource>();
        this.player = GameObject.FindWithTag("Player");
        int pos = (int)player.transform.position.x;
        health = 5 + pos / 10;
        damage = 2 + pos / 40;
        minShootTime = 0.2f + (0.8f / pos);
        maxShootTime = 0.5f + (3.0f / pos);

        enemyBullet = bulletPrefab.GetComponent<EnemyBullet>();
        enemyBullet.damage = this.damage;
        RestartShoot();
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
            Invoke("Shoot", 0.4f + Random.Range(0.1f,0.6f));
            RestartShoot();
        }

        if( (player.transform.position.x < this.transform.position.x && faceRight) ||
            (player.transform.position.x > this.transform.position.x && !faceRight) ) 
        {
            faceRight = !faceRight;
            //bodyAnimator.SetTrigger("Turn");
            this.transform.Rotate(0f, 180f, 0f);
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
            deadSound.Play();
            bodyAnimator.SetTrigger("Die");
            bodyAnimator.SetBool("Dead", true);
            //rb.bodyType = RigidbodyType2D.Static;
        }
    }
    void Vision.ITrigger._OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            bodyAnimator.SetBool("Rest", false);
        }
    }

    void Vision.ITrigger._OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player") {
            bodyAnimator.SetBool("Rest", true);
        }
    }

    static public float EulerAngleFrom(Vector2 vector) {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
