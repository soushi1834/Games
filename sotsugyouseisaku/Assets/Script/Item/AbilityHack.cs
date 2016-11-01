using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityHack : Item {

    public float HackCount;
    private Machine[] machines;
    //追加
    private AudioSource source;
    public AudioClip hackSE;

	// Use this for initialization
    protected override void Start()
    {
        HackCount = 10;
        machines = GameObject.Find("Drivers").GetComponentsInChildren<Machine>();
            //mMachine.transform.GetComponent<Machine>();
        for (var i = 0; i < machines.Length; i++)
        {
            if (machines[i] != mMachine.GetComponent<Machine>())
            {
                if (Random.Range(0, 2) == 0)
                    machines[i].Abilitylock(true);
            }
        }
        Destroy(this.gameObject, 2);

        //追加
        source = transform.GetComponent<AudioSource>();
        //追加
        source.PlayOneShot(hackSE);

        ItemEffect = Instantiate((GameObject)Resources.Load("AblityLockEffect"), this.transform.position, this.transform.rotation) as GameObject;
        ItemEffect.transform.parent = this.transform;
        ItemEffect.SetActive(true);
        effectTime = 0;
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        this.transform.position = mMachine.transform.position;
        effectTime += Time.deltaTime;
        if (effectTime >= 1)
        {
            ItemEffect.SetActive(false);
        }
	}
}
