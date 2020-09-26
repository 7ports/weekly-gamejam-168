using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCombat : MonoBehaviour
{
    public GameObject fireball;
    public float fireballSpeed;
    public float fireballCooldown;
    float cooldownLeft = 0.0f;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            fireballAttack();    
        if(cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

    void fireballAttack(){
        if (cooldownLeft <= 0)
        {
            GameObject newFireball = GameObject.Instantiate(fireball, transform.position + transform.right, Quaternion.identity);
            newFireball.GetComponent<Rigidbody2D>().velocity = transform.right * fireballSpeed;
            cooldownLeft = fireballCooldown;
        }
    }
}
