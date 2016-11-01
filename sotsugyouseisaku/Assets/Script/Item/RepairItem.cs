using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepairItem : Item {
    private Machine machine;

    private AudioSource source;
    public AudioClip repairSE;

    // Use this for initialization
    protected override void Start()
    {
        machine = mMachine.transform.GetComponent<Machine>();

        List<string> keys = new List<string>(machine.breakParts.Keys);
        for(int i = keys.Count - 1; i >= 0; i--)
        {
            if(machine.breakParts[keys[i]] == -1)
            {
                keys.Remove(keys[i]);
            }
        }

        if (keys.Count != 0)
        {
            int number;
            number = Random.Range(0, keys.Count - 1);
            if (machine.nowState[keys[number]] != machine.MachineState[keys[number]])
            {
                machine.nowState[keys[number]] = machine.MachineState[keys[number]];
                machine.breakParts[keys[number]] = -1;
            }
        }
        Destroy(this.gameObject, 2);

        //追加
        source = transform.GetComponent<AudioSource>();
        //追加
        source.PlayOneShot(repairSE);

        ItemEffect = Instantiate((GameObject)Resources.Load("RepairEffect"), this.transform.position, this.transform.rotation) as GameObject;
        ItemEffect.transform.parent = this.transform;
        ItemEffect.SetActive(true);
        effectTime = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.transform.position = mMachine.transform.position;
        effectTime += Time.deltaTime;
        if(effectTime >= 1)
        {
            ItemEffect.SetActive(false);
        }
    }
}
