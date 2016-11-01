using UnityEngine;
using System.Collections;

public class RightScript : MonoBehaviour {
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("music");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "plane")
            player.SendMessage("turnLeft", false);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "plane")
            player.SendMessage("turnLeft", true);
    }
}
