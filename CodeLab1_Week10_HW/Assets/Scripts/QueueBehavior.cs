using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QueueBehavior : MonoBehaviour
{
    //calls the DepopulateQueue function in the GameManager Script when the object is clicked on
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            GameManager.instance.DepopulateQueue(gameObject);
        }
    }
}
