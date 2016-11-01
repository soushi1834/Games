using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrier : Item
{
    public float Barriercount = 0;
    private Machine machine;
    //追加
    private AudioSource source;
    public AudioClip barrierSE;

	// Use this for initialization
    protected override void Start()
    {
        this.transform.position = mMachine.transform.position + new Vector3(0, this.transform.localScale.y / 4.0f, 0);
        Barriercount = 8;
        machine = mMachine.transform.GetComponent<Machine>();
        machine.Barrier = true;
        //追加
        source = transform.GetComponent<AudioSource>();
        //追加
        source.PlayOneShot(barrierSE);
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        this.transform.position = mMachine.transform.position + new Vector3(0, this.transform.localScale.y / 4.0f, 0);
        Barriercount -= Time.deltaTime;
        if (Barriercount <= 0)
        {
            machine.Barrier = false;
            Destroy(this.gameObject);
        }
        else
        {
            machine.Barrier = true;
        }
	}
}