﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.LookAt(player.transform.position);
	}
}
