using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestBehaviour : MonoBehaviour
{
    SpriteRenderer chestSprite;
    public GameObject blueGem, redGem, heart; 
    bool isOpen = false;
    bool isMimic = false;
    public float moveSpeed = 2.0f;
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
            if(Random.Range(0f,1f) < 0.9){
            GetComponent<Animator>().SetTrigger("isOpen");
            GameObject[] possibleObjects = { blueGem, redGem, heart };
            int i = Random.Range(0, possibleObjects.Length);
            if(other.transform.position.x > transform.position.x)
                GameObject.Instantiate(possibleObjects[i], transform.position + Vector3.left, Quaternion.identity);
            else if(other.transform.position.x <= transform.position.x)
                GameObject.Instantiate(possibleObjects[i], transform.position + Vector3.right, Quaternion.identity);
            isOpen = true;
            } else {
                Invoke("makeEnemy", 1.0f);
                GetComponent<Animator>().SetTrigger("isMimic");
                GetComponent<BoxCollider2D>().isTrigger = false;
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                GetComponent<Rigidbody2D>().freezeRotation = true;
                GetComponent<enemyHealthTracker>().enabled = true;
                
            }
        }
    }

    private void makeEnemy(){
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
