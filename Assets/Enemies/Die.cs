using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public GameObject gameObject;

    public void Destroy() {
        Destroy(gameObject);
    }
}
