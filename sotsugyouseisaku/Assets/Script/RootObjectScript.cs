using UnityEngine;
using System.Collections;

public class RootObjectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    public int getFlags()
    {
        return transform.childCount;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
