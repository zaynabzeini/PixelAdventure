using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); 
            // sets cameras position to be the same as the player's, except for z (it wont be able to see otherwise)
    }
}
