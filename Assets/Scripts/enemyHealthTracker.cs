using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthTracker : MonoBehaviour
{
    public float health;
    public bool wasDamaged = false;
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
}
