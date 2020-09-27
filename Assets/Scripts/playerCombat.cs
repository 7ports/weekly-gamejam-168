using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDelay = 0.4f;
    float nextAttackTime = 0f;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                GetComponent<Animator>().SetTrigger("attack");
                Invoke("basicAttack", attackDelay); //attack will come out after a small delay, TODO: start attack animation here
                nextAttackTime = Time.time + attackDelay;
            }
        }
    }

    void basicAttack(){
        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach (Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHealthTracker>().Damage(1.0f);
        }
    }


    private void OnDrawGizmosSelected() {
        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
