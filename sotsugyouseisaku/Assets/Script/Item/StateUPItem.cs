using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateUPItem : Item{

    private Vector3 vec;
    private Machine machine;
    private float recoveryTime;
    string upState;

    //追加
    private AudioSource source;
    public AudioClip stateSE;

    string[] str =
        {
            "Accelerate",
            "Speed",
            "Grip"
        };


    // Use this for initialization
    protected override void Start()
    {
        vec = new Vector3(Mathf.Sin(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad));
        recoveryTime = 10;
        machine = mMachine.transform.GetComponent<Machine>();
        upState = str[Random.Range(0, 2)];
        machine.nowState[upState] += 2;

        //追加
        source = transform.GetComponent<AudioSource>();
        //追加
        source.PlayOneShot(stateSE);

        ItemEffect = Instantiate((GameObject)Resources.Load("StateUPEffect"), this.transform.position, this.transform.rotation) as GameObject;
        ItemEffect.transform.parent = this.transform;
        ItemEffect.SetActive(true);
        effectTime = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        recoveryTime -= Time.deltaTime;
        if (recoveryTime <= 0)
        {
            machine.nowState[upState] = machine.MachineState[upState];
            Destroy(this.gameObject);
        }

        this.transform.position = mMachine.transform.position;
        effectTime += Time.deltaTime;
        if (effectTime >= 1)
        {
            ItemEffect.SetActive(false);
        }
    }
}
