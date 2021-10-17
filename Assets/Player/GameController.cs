using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Text scoreText;
    public Image heartImage1;
    public Image heartImage2;
    public Image heartImage3;
    public Image heartImage4;
    public Image heartImage5;

    public Image halfHeart;

    public int score = 0;
    public int life = 100;
    public int bestScore = 0;
    public bool started = false;
    public bool paused = false;

    public AudioSource takeDamage;
    public Animator playerAnimator;

    public GameObject canvasPause;
    public Animator canvasPauseAnimator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            paused = !paused;
            canvasPause.SetActive(paused);
            if (paused)
            {
                canvasPauseAnimator.SetTrigger("Open");
                //Time.timeScale = 0;
            } 
            else 
            {
                Play();
            }
        }
        int pos = (int)Mathf.Floor(this.transform.position.x);

        if (pos > score) 
        {
            score = pos;
            scoreText.text = "Distance: " + score;
            if (score > bestScore) 
            {
                bestScore = score;
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        life -= damage;
        takeDamage.Play();
        playerAnimator.SetTrigger("Hit");

        if(life < 0) 
        {
            //die;
            heartImage1.sprite  = null;
            heartImage2.sprite  = null;
            heartImage3.sprite  = null;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 10) 
        {
            heartImage1.sprite  = halfHeart.sprite;
            heartImage2.sprite  = null;
            heartImage3.sprite  = null;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 20) 
        {
            heartImage2.sprite  = null;
            heartImage3.sprite  = null;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 30) 
        {
            heartImage2.sprite  = halfHeart.sprite ;
            heartImage3.sprite  = null;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 40) 
        {
            heartImage3.sprite  = null;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 50) 
        {
            heartImage3.sprite  = halfHeart.sprite ;
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 60) 
        {
            heartImage4.sprite  = null;
            heartImage5.sprite  = null;
        } 
        else if(life < 70) 
        {
            heartImage4.sprite  = halfHeart.sprite ;
            heartImage5.sprite  = null;
        } 
        else if(life < 80) 
        {
            heartImage5.sprite  = null;
        } 
        else if(life < 90) 
        {
            heartImage5.sprite  = halfHeart.sprite ;
        }  
    }

    public void Play() 
    {
        Time.timeScale = 1;
        paused = false;
        canvasPause.SetActive(paused);
        canvasPauseAnimator.SetTrigger("Close");
    }
    public void Settings() 
    {

    }
    public void Exit() 
    {
        Application.Quit();
    }
}
