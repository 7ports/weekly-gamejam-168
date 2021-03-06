﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class chestBehaviour : MonoBehaviour
{
    SpriteRenderer chestSprite;
    public GameObject blueGem, redGem, heart; 
    bool isOpen = false;
    bool isMimic = false;
    public float moveSpeed = 2.0f;
    public float dropChance = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        chestSprite = GetComponent<SpriteRenderer>();
        GetComponent<enemyHealthTracker>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMimic){
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "EnemyTurn"){
            moveSpeed *= -1;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player") && Input.GetKey(KeyCode.Q) && !isOpen && !isMimic){
            if(UnityEngine.Random.Range(0f,1f) < dropChance){
                AudioManager.instance.Play("openChest");
            GetComponent<Animator>().SetTrigger("isOpen");
            GameObject[] possibleObjects = { blueGem, redGem, heart };
            if (other.GetComponent<playerBehaviour>().HUDUI.GetComponent<healthUI>().Health == 3)
                Array.Resize(ref possibleObjects, possibleObjects.Length - 1);
            int i = UnityEngine.Random.Range(0, possibleObjects.Length);
            if(other.transform.position.x > transform.position.x)
                GameObject.Instantiate(possibleObjects[i], transform.position + Vector3.left, Quaternion.identity);
            else if(other.transform.position.x <= transform.position.x)
                GameObject.Instantiate(possibleObjects[i], transform.position + Vector3.right, Quaternion.identity);
            isOpen = true;
            } else {
                AudioManager.instance.Play("mimicLaugh");
                Invoke("makeEnemy", 2.0f);
                GetComponent<Animator>().SetTrigger("isMimic");
                
            }
        }
    }

    private void makeEnemy(){
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<enemyHealthTracker>().enabled = true;
        gameObject.layer = 8;
        gameObject.tag = "Enemy";
        isMimic = true;

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Wall"){
            moveSpeed *= -1;
        }
        if(other.transform.tag == "EnemyTurn"){
            moveSpeed *= -1;
        }
    }
}
