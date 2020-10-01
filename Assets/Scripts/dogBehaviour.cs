using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogBehaviour : MonoBehaviour
{
    public float visionDistance = 5f;
    public LayerMask searchForPlayerLayers;
    public float moveSpeed;
    public bool aggro = false;
    float facing = 1f;
    public float aggroMoveSpeed = 3f;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!aggro)
        {
            transform.Translate(transform.right * Time.deltaTime * moveSpeed * facing);
            RaycastHit2D checkForPlayer = Physics2D.Raycast(transform.position, transform.right, visionDistance, searchForPlayerLayers);
            if (checkForPlayer.collider != null)
            {
                if (checkForPlayer.collider.tag == "Player")
                {
                    aggro = true;
                }
            }
            if(GetComponent<enemyHealthTracker>().wasDamaged)
                aggro = true;
        }
        else if(aggro){
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

            RaycastHit2D checkFace= Physics2D.Raycast(transform.position, transform.right, 2f, searchForPlayerLayers);
            if (checkFace.collider != null && isGrounded())
            {
                if (checkFace.collider.tag == "Player" || checkFace.collider.tag == "Wall")
                {
                    rb.AddForce(Vector2.up * 0.28f, ForceMode2D.Impulse);
                }
            }

            transform.Translate(transform.right * Time.deltaTime * moveSpeed * facing);
        }
        if(aggro){
            moveSpeed = aggroMoveSpeed;
            GetComponent<Animator>().SetTrigger("aggro");
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Wall"){
            transform.right *= -1f;
            facing *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "EnemyTurn"){
            transform.right *= -1f;
            facing *= -1;
        }
    }
    bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position + new Vector3(0, -0.53f, 0), Vector2.down, 0.001f); //raycasting to try to hit a platform
        if ((hitGround.collider != null))
        {
            if ((hitGround.transform.tag == "Wall") || (hitGround.transform.tag == "Platform"))
                return true;
            else
                return false;
        } else {
            return false;
        }
    }
}
