using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [HideInInspector] public bool onSight = false;
    [HideInInspector] public ITrigger iTrigger;

    private void OnTriggerEnter2D(Collider2D other) {
        onSight = true;
        if(iTrigger != null)
        {
            iTrigger._OnTriggerEnter2D(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        onSight = false;
        if(iTrigger != null)
        {
            iTrigger._OnTriggerExit2D(other);
        }
    }

    public interface ITrigger
    {
        void _OnTriggerEnter2D(Collider2D other);
        void _OnTriggerExit2D(Collider2D other);
    }

}

