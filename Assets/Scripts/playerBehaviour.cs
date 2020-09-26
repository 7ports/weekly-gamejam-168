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
    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        facing = Vector3.right;
        moveSpeed = 5.0f;
        jumpMult = 5.0f;
        fallMultiplier = 2.0f;
        jumpWeight = 3.0f;

    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxisRaw("Horizontal");
       isJump = Input.GetKeyDown(KeyCode.Space);
       Debug.Log(isGrounded());
       if(isJump && isGrounded()){
            Debug.Log("jumping");
            rb.AddForce(Vector2.up * jumpMult, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() {
        //moving player left and right
        if(horizontalInput > 0)
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        if(horizontalInput < 0)
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        
        if (rb.velocity.y < jumpWeight)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //makes the player fall faster after they've reached the peak of their jump, will wait until the true peak if jumpWeight is set to 0.0f
    }
    bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position + Vector3.down + new Vector3 (0, 0.25f, 0), Vector2.down, 0.001f); //raycasting to try to hit a platform

        if ((hitGround.collider != null) && (hitGround.transform.tag == "Platform"))
            return true;
        else
            return false;
    }
    private void OnCollisionEnter2D(Collision2D collider) {

        if (collider.gameObject.tag == "Spike")
            Destroy(gameObject); //This will straight up destroy the player, can replace this line with any code we want to execute when player touches spikes
        
    }
}
