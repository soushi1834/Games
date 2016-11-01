using UnityEngine;
using System.Collections;

public class UIRight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position -= new Vector3(1.1f * (16 - ScreenSize.aspect.x), 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
