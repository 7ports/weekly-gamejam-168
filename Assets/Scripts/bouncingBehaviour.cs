using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingBehaviour : MonoBehaviour
{
    Rigidbody2D rb;

    float jumpForce = 4.0f;
    float nextJumpTime = 0.0f;
    public float jumpDelay = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextJumpTime){
            if(Random.Range(0f, 1f) > 0.5f){
                rb.AddForce(new Vector2(jumpForce, jumpForce), ForceMode2D.Impulse);
            } else {
                rb.AddForce(new Vector2(-jumpForce, jumpForce), ForceMode2D.Impulse);
            }
            nextJumpTime = Time.time + jumpDelay;
        }
        
    }
}
