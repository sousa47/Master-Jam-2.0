using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTile : MonoBehaviour
{
    public Color offColor, onColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().dashTilePosition = transform.position;
            GetComponent<SpriteRenderer>().color = onColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().dashTilePosition = Vector3.zero;
            GetComponent<SpriteRenderer>().color = offColor;
        }
    }
}
