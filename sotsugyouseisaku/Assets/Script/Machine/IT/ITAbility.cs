using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ITAbility : Ability
{
    private float upTime;
    private bool up;
    public GameObject AbilityEffect;

    private AudioSource source;
    public AudioClip itSE;

    // Use this for initialization
    void Start()
    {
        time = 0;
        waitTime = 30;
        upTime = 0;
        up = false;
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!up)
        {
            time += Time.deltaTime;
        }
        upTime -= Time.deltaTime;
    }

    public Dictionary<string, float> StateUP(Dictionary<string, float> state)
    {
        if (time >= waitTime && !up)
        {
            source.PlayOneShot(itSE);
            state["Accelerate"] += 3;
            state["Speed"] += 6;
            state["Grip"] += 3;
            up = true;
            upTime = 10;
            time = 0;

            AbilityEffect = Instantiate((GameObject)Resources.Load("StateUPEffect"), this.transform.position, this.transform.rotation) as GameObject;
            AbilityEffect.transform.parent = this.transform;
            AbilityEffect.SetActive(true);
        }

        return state;
    }



    public bool End()
    {
        if (upTime <= 0 &&
            up == true)
        {
            up = false;
            AbilityEffect.SetActive(false);
            return true;
        }
        return false;
    }
}
