using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class gameOver : MonoBehaviour
{


    public Button continueButton;
    public GameObject player;
    public GameObject[] graveYard = {};
    public GameObject gameOverUI;

    void Update()
    {
        GameObject savedPoint = GameObject.FindGameObjectWithTag("savedPoint");
        if(savedPoint != null){
            continueButton.enabled = true;
        } else {
            continueButton.enabled = false;
        }
    }


    public void continueFromCheckPoint(){
        GameObject savedPoint = GameObject.FindGameObjectWithTag("savedPoint");
        player.transform.position = savedPoint.transform.position;
        GameObject HUDUI = player.GetComponent<playerBehaviour>().HUDUI;
        HUDUI.GetComponent<healthUI>().Health = 3;
        foreach(GameObject enemy in graveYard){
            enemy.GetComponent<enemyHealthTracker>().resetHealth();
            Array.Resize(ref graveYard, 0);
        }
        Time.timeScale = 1.0f;
        gameOverUI.SetActive(false);
    }
}
