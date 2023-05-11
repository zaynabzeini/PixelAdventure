using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>(); 
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f); // invokes a method (name is the first arguement) after a few seconds (second arguement) 
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
