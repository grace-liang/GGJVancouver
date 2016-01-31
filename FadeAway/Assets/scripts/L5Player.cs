﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level4Player : MonoBehaviour {

	private readonly int speed = 5;
	bool platformEntered;
	bool deathzoneEntered;


	// Use this for initialization
	void Start () {
		platformEntered = true;
		deathzoneEntered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate(Vector3.up * speed *Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate(Vector3.down * speed *Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate(Vector3.left * speed *Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Translate(Vector3.right * speed *Time.deltaTime);
		}

		if (!platformEntered && deathzoneEntered) {
			// TODO: death here
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}



	void circleMovement(bool moveleft) {
		if (moveleft) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		} else {
			transform.Translate(Vector3.right * speed *Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		OnTriggerStay2D(other);
	}

	void OnTriggerStay2D(Collider2D other) {
		// Move our position a step closer to the target.
		string tagName = other.tag;
		if (tagName == "Platform" || tagName == "Start") {
			platformEntered = true;

			float x_value = other.GetComponent<Transform> ().position.x;
			float y_value = this.GetComponent<Transform> ().position.y;
			float z_value = this.GetComponent<Transform> ().position.z;
			Vector3 new_positions = new Vector3 (x_value, y_value, z_value);

			transform.position = Vector3.MoveTowards (transform.position, new_positions, speed * Time.deltaTime);
		} else if (tagName == "Deathzone") {
			deathzoneEntered = true;

		} else if (tagName == "Finish") {
			deathzoneEntered = false;
			platformEntered = true; 
			// TODO: go to next level
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Platform" || other.tag == "Start") {
			platformEntered = false;
		}
	}
}
