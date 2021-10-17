using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGenerator : MonoBehaviour
{
    public List<GameObject> possibleUpgrades;

    private void Start()
    {
        Instantiate(possibleUpgrades[Random.Range(0, possibleUpgrades.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
