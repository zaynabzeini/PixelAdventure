using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim; 
    private Transform _transform;
    [SerializeField] private AudioSource deathSoundEffect;  

    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        _transform = GetComponent<Transform> (); 
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        { 
            Die(); 
            Wait(); 
            if (GameManager.gm) // if the gameManager is available, tell it to reset the game
            {  
                GameManager.gm.ResetGame(); 
            } 
                
        }
    }

    private void Die()
    { 
        deathSoundEffect.Play();
        // rb.bodyType = RigidbodyType2D.Static;
        rb.velocity = new Vector2(0,0);
		rb.isKinematic = true;

        anim.SetTrigger("death"); 
    }

    /*public void RestartLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // gets the name of the active scene and passes it into be loaded
         
    } */

    public void Respawn(Vector3 spawnloc) 
    {  
	    _transform.parent = null;
		_transform.position = spawnloc; 
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        MovementState state;
        state = MovementState.idle;
        anim.SetInteger("state", (int)state); 
	}

    IEnumerator Wait()
    {
        // wait
		yield return new WaitForSeconds(2.0f);
    }
 
}
