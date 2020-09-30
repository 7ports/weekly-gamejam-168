using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class checkPoint : MonoBehaviour
{

    public Sprite savedSprite;
    public Sprite defaultSprite;
    
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player") && gameObject.CompareTag("checkPoint")){
            Debug.Log("saving");
            GetComponent<SpriteRenderer>().sprite = savedSprite;
            GameObject previousCheckPoint = GameObject.FindGameObjectWithTag("savedPoint"); 
            if (previousCheckPoint != null) {
                previousCheckPoint.tag = "checkPoint";
                previousCheckPoint.GetComponent<SpriteRenderer>().sprite=defaultSprite;
            }
            gameObject.tag = "savedPoint";
        }
    }
}
