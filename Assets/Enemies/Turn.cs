using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public void TurnDirection() 
    { 
        this.transform.localScale = new Vector3(
            -this.transform.localScale.x,
            this.transform.localScale.y,
            this.transform.localScale.z
        );
    }
}
