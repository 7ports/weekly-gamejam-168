using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthTracker : MonoBehaviour
{
    public float health;
    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Fireball"){
            Damage(3.0f);
            Destroy(other.gameObject);
        }
    }
    public void Damage(float damageValue){
        health -= damageValue;
    }
}
