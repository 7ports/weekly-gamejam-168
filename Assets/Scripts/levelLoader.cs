﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{


    public Animator transition;
    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            LoadNextLevel();
        }
    }
    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }


    IEnumerator LoadLevel(int levelIndex){

        transition.SetTrigger("Start");
        AudioManager.instance.Play("levelEnd");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        
    }
    
}
