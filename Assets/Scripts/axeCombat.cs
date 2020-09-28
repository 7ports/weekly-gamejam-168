using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeCombat : MonoBehaviour
{
    public Transform axeAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDelay = 0.7f;
    float nextAttackTime = 0f;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<Animator>().SetTrigger("axeAttack");
                Invoke("axeAttack", attackDelay); //attack will come out after a small delay, TODO: start attack animation here
                nextAttackTime = Time.time + attackDelay;
            }
        }
    }

    void axeAttack(){
        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(axeAttackPoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach (Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHealthTracker>().Damage(2.0f);
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            enemy.GetComponent<enemyHealthTracker>().spriteReset();
        }
    }


    private void OnDrawGizmosSelected() {
        if(axeAttackPoint != null)
            Gizmos.DrawWireSphere(axeAttackPoint.position, attackRange);
    }
}
