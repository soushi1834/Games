using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedicalAbility : Ability {

    //追加
    private AudioSource source;
    public AudioClip medicalSE;


    // Use this for initialization
    void Start()
    {
        time = 0;
        waitTime = 20;
        //追加
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public Dictionary<string, float> Repair(Dictionary<string, float> now, Dictionary<string, float> state, Dictionary<string, float> breakParts)
    {
        if (time >= waitTime)
        {
            List<string> keys = new List<string>(breakParts.Keys);
            foreach (var key in keys)
            {
                if (breakParts[key] == -1)
                {
                    //追加
                    source.PlayOneShot(medicalSE);
                    now[key] = state[key];
                }
            }

            GameObject obj = Instantiate(Resources.Load("Barrier"), this.transform.position, this.transform.rotation) as GameObject;
            Item i = obj.GetComponent<Item>();
            Vector3 force;
            force = this.gameObject.transform.forward;
            obj.rigidbody.AddForce(force);
            i.setData(this.transform.gameObject, force);

            time = 0;
        }
        return new Dictionary<string,float>(now);
    }

    public bool BreakDownOff(Dictionary<string, float> breakParts)
    {
        if (time >= waitTime)
        {
            List<string> keys = new List<string>(breakParts.Keys);
            foreach (string key in keys)
            {
                breakParts[key] = -1; //REPAIRED
            }
            return true;
        }
        return false;
    }

}