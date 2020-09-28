using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartBehaviour : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other) {
       if (other.collider.CompareTag("Player")){
           other.gameObject.GetComponent<playerBehaviour>().heal(1);
           Destroy(gameObject);
       }
   } 
}
