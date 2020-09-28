using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class axeCombat : MonoBehaviour
{
    public Transform axeAttackPoint1, axeAttackPoint2, axeAttackPoint3;
    public float attackRange = 0.5f;
    float initAttackTime = 0f;
    public LayerMask enemyLayers;
    float nextAttackTime = 0f;
    public GameObject coolDownBar;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                GetComponent<Animator>().SetTrigger("axeAttack");
                Invoke("axeAttack", 0.7f); //attack will come out after a small delay, TODO: start attack animation here
                nextAttackTime = Time.time + 3f;
                initAttackTime = Time.time;

            }
        }
        coolDownBar.GetComponent<Slider>().value = Mathf.Lerp(0, 1, (Time.time-initAttackTime)/(nextAttackTime-initAttackTime));
    }

    void axeAttack(){
        Transform[] axeAttackPoints = { axeAttackPoint1, axeAttackPoint2, axeAttackPoint3 };
        foreach (Transform axeAttackPoint in axeAttackPoints)
        {
            //Detect Enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(axeAttackPoint1.position, attackRange, enemyLayers);
            //Damage enemies
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<enemyHealthTracker>().Damage(1.0f);
                enemy.GetComponent<SpriteRenderer>().color = Color.red;
                enemy.GetComponent<enemyHealthTracker>().spriteReset();
            }
        }
    }


    private void OnDrawGizmosSelected() {
        Transform[] axeAttackPoints = { axeAttackPoint1, axeAttackPoint2, axeAttackPoint3 };
        foreach (Transform axeAttackPoint in axeAttackPoints){
            if(axeAttackPoint != null)
                Gizmos.DrawWireSphere(axeAttackPoint.position, attackRange);
        }
    }
}
