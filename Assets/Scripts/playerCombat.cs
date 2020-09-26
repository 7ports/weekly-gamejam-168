using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Invoke("basicAttack", 0.2f); //attack will come out after a small delay, TODO: start attack animation here
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
