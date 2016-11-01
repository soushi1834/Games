using UnityEngine;
using System.Collections;

public class TechnologyAbility : Ability {

    private Vector3 vec;
    private float speed;
    //追加
    private AudioSource source;
    public AudioClip technoSE;

	// Use this for initialization
	void Start () {
        speed = 0;
        time = 0;
        waitTime = 20;
        vec = new Vector3(Mathf.Sin(this.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad));
        //追加
        source = transform.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        vec = new Vector3(Mathf.Sin(this.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(this.transform.eulerAngles.y * Mathf.Deg2Rad));
        this.rigidbody.velocity += vec * speed;
        if (this.GetComponentInChildren<Driver>().Name == "Player1")
        {
            SpeedUI.SpeedUp(speed);
        }
        if (this.GetComponentInChildren<Driver>().Name == "Player2")
        {
            SpeedUI2P.SpeedUp(speed);
        }
        speed -= 250 * Time.deltaTime;
        if (speed <= 0)
        {
            speed = 0;
            time += Time.deltaTime;
        }
	}

    public void Dash()
    {
        if (time >= waitTime)
        {
            //追加
            audio.PlayOneShot(technoSE);
            speed = 500;
            time = 0;
            if (this.GetComponent<Machine>().speed < 0)
            {
                this.GetComponent<Machine>().speed = 0;
            }
        }
    }
}
