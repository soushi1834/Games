using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    protected GameObject mMachine;
    protected GameObject ItemEffect;
    protected float effectTime;
    protected Vector3 force;
    protected int damage;

	// Use this for initialization
	protected virtual void Start () {
        force = Vector3.zero;
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {

	}

    public void setData(GameObject obj, Vector3 force)
    {
        mMachine = obj;
        this.force = force;
    }

    public int getDamage()
    {
        return damage;
    }

    public virtual void itemAbility(Machine target)
    {

    }
}
