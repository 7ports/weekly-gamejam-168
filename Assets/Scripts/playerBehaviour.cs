using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour
{
    float moveSpeed;
    Rigidbody2D rb;
    Vector3 facing;
    float horizontalInput;
    bool isJump;
    float jumpMult;
    float fallMultiplier;
    float jumpWeight;
    public GameObject HUDUI;
    bool invincible;
    float invincibilityDuration;
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        facing = Vector3.right;
        moveSpeed = 5.0f;
        jumpMult = 5.0f;
        fallMultiplier = 2.0f;
        jumpWeight = 3.0f;
        invincible = false;
        invincibilityDuration = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxisRaw("Horizontal");
       isJump = Input.GetKeyDown(KeyCode.Space);
       GetComponent<Animator>().SetBool("isGrounded", isGrounded());
       if(isJump && isGrounded()){
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() {
        //moving player left and right
        if(horizontalInput > 0){
            transform.right = Vector3.right;
            transform.Translate(transform.right * Time.deltaTime * moveSpeed);
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else if(horizontalInput < 0){
            transform.right = Vector3.left;
            transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
            GetComponent<Animator>().SetBool("isWalking", true);
        } else {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        if (rb.velocity.y < jumpWeight)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //makes the player fall faster after they've reached the peak of their jump, will wait until the true peak if jumpWeight is set to 0.0f
    }
    bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position + Vector3.down + new Vector3 (0, 0.25f, 0), Vector2.down, 0.001f); //raycasting to try to hit a platform
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
    private void OnCollisionStay2D(Collision2D collider) {
        if (!invincible)
        {
            if (collider.gameObject.tag == "Spike")
                HUDUI.GetComponent<healthUI>().Health = 0;
            if (collider.gameObject.tag == "Enemy"){
                takeDamage(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "blueGem") {
            //TODO: disable other special combat scripts when picking up a particular gem
            GetComponent<fireballCombat>().enabled = true;
            other.GetComponent<Animator>().SetTrigger("break");
            Destroy(other.gameObject, other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }

    private void resetInvincibility(){
        invincible = false;
        CancelInvoke("flickerSprite");
        GetComponent<SpriteRenderer>().enabled = true;
    }
    private void flickerSprite(){
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }
    public void takeDamage(int damage){
        HUDUI.GetComponent<healthUI>().Health -= damage;
        invincible = true;
        Invoke("resetInvincibility", invincibilityDuration);
        InvokeRepeating("flickerSprite", 0.0f, 0.3f);
    }
}
