using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image empty;

    public int score = 0;
    public int life = 100;
    public int bestScore = 0;
    public bool started = false;
    public bool finished = false;
    public bool paused = false;

    public AudioSource takeDamage;
    public AudioSource gameOver;
    public AudioSource click;
    public Animator playerAnimator;

    public GameObject canvasPause;
    public Animator canvasPauseAnimator;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject buttonGun1;
    public GameObject buttonGun2;
    public GameObject gun1;
    public GameObject gun2;


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
            GameOver();
            heartImage1.sprite  = empty.sprite;
            heartImage2.sprite  = empty.sprite;
            heartImage3.sprite  = empty.sprite;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 10) 
        {
            heartImage1.sprite  = halfHeart.sprite;
            heartImage2.sprite  = empty.sprite;
            heartImage3.sprite  = empty.sprite;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 20) 
        {
            heartImage2.sprite  = empty.sprite;
            heartImage3.sprite  = empty.sprite;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 30) 
        {
            heartImage2.sprite  = halfHeart.sprite ;
            heartImage3.sprite  = empty.sprite;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 40) 
        {
            heartImage3.sprite  = empty.sprite;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 50) 
        {
            heartImage3.sprite  = halfHeart.sprite ;
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 60) 
        {
            heartImage4.sprite  = empty.sprite;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 70) 
        {
            heartImage4.sprite  = halfHeart.sprite ;
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 80) 
        {
            heartImage5.sprite  = empty.sprite;
        } 
        else if(life < 90) 
        {
            heartImage5.sprite  = halfHeart.sprite ;
        }  
    }

    public void Play() 
    {
        click.Play();
        buttonGun1.SetActive(false);
        buttonGun2.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        canvasPause.SetActive(paused);
        canvasPauseAnimator.SetTrigger("Close");
    }
    public void Settings() 
    {
        click.Play();
        Debug.Log("sss");
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        buttonGun1.SetActive(true);
        buttonGun2.SetActive(true);
        //Time.timeScale = 0;
    }
    public void Exit() 
    {
        click.Play();
        Application.Quit();
    }
    public void Gun1() 
    {
        click.Play();
        playerAnimator.SetInteger("gun", 1);
        gun1.SetActive(true);
        gun2.SetActive(false);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        buttonGun1.SetActive(false);
        buttonGun2.SetActive(false);
    }
    public void Gun2() 
    {
        click.Play();
        playerAnimator.SetInteger("gun", 2);
        gun1.SetActive(false);
        gun2.SetActive(true);
        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        buttonGun1.SetActive(false);
        buttonGun2.SetActive(false);
    }

    public void GameOver() {
        finished = true;
        gameOver.Play();
        gun1.SetActive(false);
        gun2.SetActive(false);
        Invoke("RestartLevel", 3f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
