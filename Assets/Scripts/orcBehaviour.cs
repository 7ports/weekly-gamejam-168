using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orcBehaviour : MonoBehaviour
{
    public float moveSpeed;
    float waitForMove = 0f;

    public float moveWaitMin = 1f;
    public float moveWaitMax = 4f;
    public float moveDuration = 1f;
    bool isMoving = false;
    void Update()
    {
        if((Time.time >= waitForMove)){
            isMoving = true;
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        if(isMoving && moveDuration > 0){
            transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            moveDuration -= Time.deltaTime;
        } else if(moveDuration <= 0){
            moveDuration = Random.Range(0.5f, 1f); //in seconds
            isMoving = false;
            GetComponent<Animator>().SetBool("isWalking", false);
            waitForMove = Time.time + Random.Range(moveWaitMin, moveWaitMax);
            Debug.Log(waitForMove);
            if(Random.Range(0f,1f) > 0.5f) {
                transform.right *= -1f;
                moveSpeed *= -1f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Wall"){
            transform.right *= -1f;
            moveSpeed *= -1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "EnemyTurn"){
            transform.right *= -1f;
            moveSpeed *= -1f;
        }
    }
}
