using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    private int bananas = 0; 
    private int oranges = 0; 
    // private int total = 0;
    // private int high = 0;

    [SerializeField] private Text cherriesText;
    [SerializeField] private Text bananasText;
    [SerializeField] private Text orangesText;
    // [SerializeField] private Text scoreText;
    // [SerializeField] private Text highScoreText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries; 
            
            if (GameManager.gm) // add the points through the game manager, if it is available
			    GameManager.gm.AddPoints(1);
        }
        else if (collision.gameObject.CompareTag("Banana"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            bananas+=2;
            bananasText.text = "Bananas: " + bananas; 

            if (GameManager.gm) // add the points through the game manager, if it is available
			    GameManager.gm.AddPoints(2);
        }
        else if (collision.gameObject.CompareTag("Orange"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            oranges+=3;
            orangesText.text = "Oranges: " + oranges; 

            if (GameManager.gm) // add the points through the game manager, if it is available
			    GameManager.gm.AddPoints(3);
        }
        // scoreText.text = "Score: " + (cherries + bananas + oranges);
        
        /*total = cherries + bananas + oranges;
        scoreText.text = "Score: " + total;

        if (total > high)
        {
            high = total;
            highScoreText.text = "Highscore: " + high; 
        }*/
 
 
    }
}
