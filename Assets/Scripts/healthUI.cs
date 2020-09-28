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

    public int Health { get { return health; } set { if(health != value) { int diff = health - value; health = value; if(diff > 0){mainCamera.GetComponent<shakeBehaviour>().TriggerShake();} } updateHealthUI(); } }
    public GameObject gameOverCanvas;

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

        if (health <= 0){
            
            gameOverCanvas.SetActive(true);
            mainCamera.GetComponent<shakeBehaviour>().stopShake();
            Time.timeScale = 0f;


        }
    }
}
