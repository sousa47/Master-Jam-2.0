using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    private Transform playerPosition;

    public float movementSpeed;
    public float maxDistanceFromPlayer;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector2(playerPosition.position.x - maxDistanceFromPlayer, 0);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        if (playerPosition.position.x - transform.position.x > maxDistanceFromPlayer)
        {
            transform.position = new Vector2(playerPosition.position.x - maxDistanceFromPlayer, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // lose game
            print("GAME OVER");
        }
    }
}
