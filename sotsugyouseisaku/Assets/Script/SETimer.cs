using UnityEngine;
using System.Collections;

public class SETimer : MonoBehaviour {
    private AudioSource source;
    public AudioClip countSE;

    private float timer;
    private int counter;

	// Use this for initialization
	void Start () {
        timer = -1;
        counter = 0;
        source = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (counter >= 3)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 0)
        {
            counter++;
            source.PlayOneShot(countSE);
            timer--;
        }
	}
}
