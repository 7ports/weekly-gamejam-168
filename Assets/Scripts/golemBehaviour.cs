using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golemBehaviour : MonoBehaviour
{
    public float attackDelay = 1f;
    public Transform attackPoint;
    public float attackRange = 2.0f;
    public float moveSpeed = 1f;
    float waitForMove = 0f;

    public float moveWaitMin = 1f;
    public float moveWaitMax = 4f;
    public float moveDuration = 1f;
    bool isMoving = false;
    float facing = 1f;
    public float visionDistance = 5f;
    float nextAttackTime = 0f;
    public float aggroMoveSpeed = 2f;
    public LayerMask searchForPlayerLayers;

    private void Start()
    {
        //make it so that the player can walk through the orc
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>());
    }
    bool aggro = false;
    void Update()
    {
        if (!aggro)
        {
            RaycastHit2D checkForPlayer = Physics2D.Raycast(transform.position - new Vector3(0, 0.5f, 0), transform.right, visionDistance, searchForPlayerLayers);
            if (checkForPlayer.collider != null)
            {
                if (checkForPlayer.collider.tag == "Player")
                {
                    aggro = true;
                }
            }
            if(GetComponent<enemyHealthTracker>().wasDamaged)
                aggro = true;

            if ((Time.time >= waitForMove))
            {
                isMoving = true;
                GetComponent<Animator>().SetBool("isWalking", true);
            }
            if (isMoving && moveDuration > 0)
            {
                transform.Translate(transform.right * Time.deltaTime * moveSpeed * facing);
                moveDuration -= Time.deltaTime;
            }
            else if (moveDuration <= 0)
            {
                moveDuration = Random.Range(0.5f, 1f); //in seconds
                isMoving = false;
                GetComponent<Animator>().SetBool("isWalking", false);
                waitForMove = Time.time + Random.Range(moveWaitMin, moveWaitMax);
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    transform.right *= -1f;
                    facing *= -1f;
                }
            }
        } else if(aggro){
            //play aggro animation
            //move in the direction of the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player.transform.position.x < transform.position.x){
                transform.right = Vector3.left;
                facing = -1f;
            }
            else{
                transform.right = Vector3.right;
                facing = 1f;
            }
            transform.Translate(transform.right * Time.deltaTime * moveSpeed * facing);
            //check if the player is in range to be hit, if yes then swing club and play club swinging animation
            Collider2D[] checkTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, searchForPlayerLayers);
            foreach(Collider2D target in checkTargets){
                if(target.tag == "Player" && (Time.time >= nextAttackTime)){
                    GetComponent<Animator>().SetTrigger("attack");
                    Invoke("attack", 0.4f);
                    nextAttackTime = Time.time + attackDelay;
                }
            }


        }




        if(aggro){
            GetComponent<Animator>().SetTrigger("aggrod");
            moveSpeed = aggroMoveSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Wall"){
            transform.right *= -1f;
            facing *= -1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "EnemyTurn"){
            transform.right *= -1f;
            facing *= -1f;
        }
    }
    void attack(){
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, searchForPlayerLayers);
        foreach(Collider2D target in hitTargets){
            if(target.tag == "Player"){
                target.GetComponent<playerBehaviour>().takeDamage(1);
            }
        }
    }
    private void OnDrawGizmosSelected() {
        if(attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
