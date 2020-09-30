using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fireballCombat : MonoBehaviour
{
    public GameObject fireball;
    public float fireballSpeed;
    float nextAttackTime = 0.0f;
    float initAttackTime = 0.0f;
    public GameObject coolDownBar;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            fireballAttack();    
        
        coolDownBar.GetComponent<Slider>().value = Mathf.Lerp(0, 1, (Time.time-initAttackTime)/(nextAttackTime-initAttackTime));
    }

    void fireballAttack(){
        if (Time.time >= nextAttackTime)
        {
            GameObject newFireball = GameObject.Instantiate(fireball, transform.position + transform.right, Quaternion.identity);
            AudioManager.instance.Play("specialAttack");
            GetComponent<Animator>().SetTrigger("fireball");
            newFireball.GetComponent<Rigidbody2D>().velocity = transform.right * fireballSpeed;
            nextAttackTime = Time.time + 5.0f;
            initAttackTime = Time.time;
        }
    }
}
