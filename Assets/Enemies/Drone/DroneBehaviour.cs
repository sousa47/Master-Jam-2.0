using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour, Vision.ITrigger
{
    public Animator animator;
    public Animator bodyAnimator;
    public Vision vision;
    GameObject player;
    bool faceRight = false;

    // Start is called before the first frame update
    void Start()
    {
         this.player = GameObject.FindWithTag("Player");
         this.vision.iTrigger = this;
    }

    // Update is called once per frame
    void Update()
    {

        if(!vision.onSight) return;

        if( (player.transform.position.x < this.transform.position.x && faceRight) ||
            (player.transform.position.x > this.transform.position.x && !faceRight) ) 
        {
            faceRight = !faceRight;
            bodyAnimator.SetTrigger("Turn");
        }
    }

    void Vision.ITrigger._OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("Attention");
    }

    void Vision.ITrigger._OnTriggerExit2D(Collider2D other)
    {

    }
}
