using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public GameObject mainCamera;
    public Image h1, h2, h3;
    private Image[] images;
    private int health = 3;

    public int Health { get { return health; } set { if(health != value) { health = value; mainCamera.GetComponent<shakeBehaviour>().TriggerShake(); } updateHealthUI(); } }

    void Awake()
    {
        images = new Image[] {h1, h2, h3};    
    }


    private void updateHealthUI(){
        Debug.Log("updating health");
        Debug.Log(health);
        for (int i = 0; i < images.Length; i++){
            if(i >= health)
                images[i].enabled = false;
            else
                images[i].enabled = true;
        }
    }
}
