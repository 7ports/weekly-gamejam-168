using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class heartBehaviour : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D other) {
       if (other.collider.CompareTag("Player") && (other.gameObject.GetComponent<playerBehaviour>().HUDUI.GetComponent<healthUI>().Health < 3)){
            other.gameObject.GetComponent<playerBehaviour>().heal(1);
            AudioManager.instance.Play("playerHeal");
            gameOver gameOverCanvas = GameObject.FindGameObjectWithTag("gameOverCanvas").GetComponent<gameOver>();
            Array.Resize(ref gameOverCanvas.graveYard, gameOverCanvas.graveYard.Length + 1);
            gameOverCanvas.graveYard[gameOverCanvas.graveYard.GetUpperBound(0)] = gameObject;
            gameObject.SetActive(false);
       }
   } 
}
