using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f;  // enemy move speed when moving
	// public int damageAmount = 10; // probably deal a lot of damage to kill player immediately

	// public GameObject stunnedCheck; // what gameobject is the stunnedCheck

	// public float stunnedTime = 3f;   // how long to wait at a waypoint
	
	// public string stunnedLayer = "StunnedEnemy";  // name of the layer to put enemy on when stunned
	// public string playerLayer = "Player";  // name of the player layer to ignore collisions with when stunned
	
	// public bool isStunned = false;  // flag for isStunned
	
	public GameObject[] myWaypoints; // to define the movement waypoints
	
	public float waitAtWaypointTime = 1f;   // how long to wait at a waypoint
	
	public bool loopWaypoints = true; // should it loop through the waypoints
	
	  
	// private variables below
	
	// store references to components on the gameObject
	Transform _transform;
	Rigidbody2D _rigidbody;
	Animator _animator;
	// AudioSource _audio;
	
	// movement tracking
	int _myWaypointIndex = 0; // used as index for My_Waypoints
	float _moveTime; 
	float _vx = 0f;
	bool _moving = true;
	
	// store the layer number the enemy is on (setup in Awake)
	int _enemyLayer;

	 
	
	void Awake() {
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		
		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");
		
		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");
		
		/*_audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		} 

		if (stunnedCheck==null) {
			Debug.LogError("stunnedCheck child gameobject needs to be setup on the enemy");
		}*/
		
		// setup moving defaults
		_moveTime = 0f;
		_moving = true;
		
		// determine the enemies specified layer
		_enemyLayer = this.gameObject.layer;

        /*
		// determine the stunned enemy layer number
		_stunnedLayer = LayerMask.NameToLayer(stunnedLayer);
        */

		// make sure collision are off between the playerLayer and the stunnedLayer
		// which is where the enemy is placed while stunned
		// Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayer), _stunnedLayer, true); 
	}
	
	// if not stunned then move the enemy when time is > _moveTime
	void Update () {
		//if (!isStunned)
		// {
			if (Time.time >= _moveTime) {
				EnemyMove();
			} /*else {
				_animator.SetBool("Moving", false);
			}*/
		// }
	}
	
	// Move the enemy through its rigidbody based on its waypoints
	void EnemyMove() {
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (_moving)) {
			
			// make sure the enemy is facing the waypoint (based on previous movement)
			Flip (_vx);
			
			// determine distance between waypoint and enemy
			_vx = myWaypoints[_myWaypointIndex].transform.position.x-_transform.position.x;
			
			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if (Mathf.Abs(_vx) <= 0.05f) {
				// At waypoint so stop moving
				_rigidbody.velocity = new Vector2(0, 0);
				
				// increment to next index in array
				_myWaypointIndex++;
				
				// reset waypoint back to 0 for looping
				if(_myWaypointIndex >= myWaypoints.Length) {
					if (loopWaypoints)
						_myWaypointIndex = 0;
					else
						_moving = false;
				}
				
				// setup wait time at current waypoint
				_moveTime = Time.time + waitAtWaypointTime;
			} else {
				// enemy is moving
				// _animator.SetBool("Moving", true);
				
				// Set the enemy's velocity to moveSpeed in the x direction.
				_rigidbody.velocity = new Vector2(_transform.localScale.x * moveSpeed, _rigidbody.velocity.y);
			}
			
		}
	}
	
	// flip the enemy to face torward the direction he is moving in
	void Flip(float _vx) {
		
		// get the current scale
		Vector3 localScale = _transform.localScale;
		
		if ((_vx>0f)&&(localScale.x<0f))
			localScale.x*=-1;
		else if ((_vx<0f)&&(localScale.x>0f))
			localScale.x*=-1;
		
		// update the scale
		_transform.localScale = localScale;
	}
}
