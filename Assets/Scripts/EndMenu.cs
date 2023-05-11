using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverSoundEffect; 

    void Start()
    {
        gameOverSoundEffect.Play();
    }
    
    public void Quit()
    {
        Application.Quit();
    } 

    /*public void GameOverSound() {  
	    gameOverSoundEffect.Play();
	} */
}
