using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nakedPlayerBehaviour : MonoBehaviour
{

    float horizontalInput;

    float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = 0.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
    }


    private void FixedUpdate() {
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
    }
}
