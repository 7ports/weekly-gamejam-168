using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springBehaviour : MonoBehaviour
{
    public Vector2 springForce = new Vector2(0.0f, 3.0f);
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("Enemy")){
            other.GetComponent<Rigidbody2D>().AddForce(springForce, ForceMode2D.Impulse);
            GetComponent<Animator>().SetTrigger("isSpring");
        }
    }
}
