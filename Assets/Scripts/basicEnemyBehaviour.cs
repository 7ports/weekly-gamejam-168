using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemyBehaviour : MonoBehaviour
{
    public float moveSpeed;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Wall"){
            moveSpeed *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "EnemyTurn"){
            moveSpeed *= -1;
        }
    }
}
