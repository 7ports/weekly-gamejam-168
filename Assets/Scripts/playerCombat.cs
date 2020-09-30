using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDelay = 0.15f;
    float nextAttackTime = 0f;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Animator>().SetTrigger("attack");
                Invoke("basicAttack", 0.3f); //attack will come out after a small delay, TODO: start attack animation here
                nextAttackTime = Time.time + 0.3f;
            }
        }
    }

    void basicAttack(){
        //Detect Enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage enemies
        foreach (Collider2D enemy in hitEnemies){
            enemy.GetComponent<enemyHealthTracker>().Damage(1.0f);
            enemy.GetComponent<Rigidbody2D>().AddForce(transform.right*6.0f);
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
            enemy.GetComponent<enemyHealthTracker>().spriteReset();
        }
    }


    private void OnDrawGizmosSelected() {
        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
