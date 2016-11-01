using UnityEngine;
using System.Collections;

public class LeftScript : MonoBehaviour {
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
            player.SendMessage("turnRight", false);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "plane")
            player.SendMessage("turnRight", true);
    }
}
