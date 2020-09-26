using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraBox;
    void Update()
    {
        //this will cause the camera to follow the player within the bounds of the "box" defined by the cameraBox variable
        //the box is cameraBox.x*2 wide and cameraBox.y*2 tall
        float x = transform.position.x;
        float y = transform.position.y;
       if(player.position.x > transform.position.x + cameraBox.x)
            x = player.position.x - cameraBox.x;
       else if(player.position.x < transform.position.x - cameraBox.x)
            x = player.position.x + cameraBox.x;

       if(player.position.y > transform.position.y + cameraBox.y)
            y = player.position.y - cameraBox.y;
       else if(player.position.y < transform.position.y - cameraBox.y)
            y = player.position.y + cameraBox.y;

        transform.position = new Vector3(x, y, -10);
    }
}
