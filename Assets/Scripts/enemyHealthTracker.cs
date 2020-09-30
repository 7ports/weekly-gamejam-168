﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemyHealthTracker : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool wasDamaged = false;
    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            gameObject.SetActive(false);
            gameOver gameOverCanvas = GameObject.FindGameObjectWithTag("gameOverCanvas").GetComponent<gameOver>();
            Array.Resize(ref gameOverCanvas.graveYard, gameOverCanvas.graveYard.Length + 1);
            gameOverCanvas.graveYard[gameOverCanvas.graveYard.GetUpperBound(0)] = gameObject;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Fireball" && (this.enabled == true)){
            Damage(3.0f);
            Destroy(other.gameObject);
            GetComponent<SpriteRenderer>().color = Color.red;
            spriteReset();
        }
    }
    public void Damage(float damageValue){
        health -= damageValue;
        wasDamaged = true;
    }

    public void spriteReset(){
        Invoke("resetSpriteColor", 0.25f);
    }
    void resetSpriteColor(){

        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void resetHealth(){
        health = 1.0f;
        wasDamaged = false;
        gameObject.SetActive(true);
    }
}
