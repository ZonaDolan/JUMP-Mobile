using UnityEngine;
using System.Collections;

public class ChasingCamera : MonoBehaviour {
	public GameObject player;

	public float distX; // TODO adjust distance
	public float distY; // TODO size per grid (tiap 1 tingkat platform)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ChasePlayer ();
	}

	private void ChasePlayer() {
		Vector2 playerPos = new Vector2 (player.transform.position.x, transform.position.y);
		Vector2 camPos = new Vector2 (transform.position.x, transform.position.y);
		float currentDistanceX = Mathf.Abs (playerPos.x - camPos.x);
		if (currentDistanceX > distX) {
			if (player.transform.position.x > this.transform.position.x) {
				// TODO adjust speed
				transform.position = Vector3.Lerp (transform.position, new Vector3 ((player.transform.position.x - distX), transform.position.y, transform.position.z), Time.deltaTime);
			} else {
				// TODO adjust speed
				transform.position = Vector3.Lerp (transform.position, new Vector3 ((player.transform.position.x + distX), transform.position.y, transform.position.z), Time.deltaTime);
			}
		}
	}
}
