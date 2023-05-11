using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewScene : MonoBehaviour
{
    private string levelToLoad;  //= "Level 1";

    // private void Update()
    // {
    //     transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    // }

    // load the specified Unity level
	public void LoadLevel(string levelToLoad)
	{ 
		// load the specified level
		SceneManager.LoadScene(levelToLoad);
	}
    
}
