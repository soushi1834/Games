using UnityEngine;
using System.Collections;

public class TriggerDamage : MonoBehaviour {

    Machine mMachine;
    GameObject Sparks;
    GameObject MusicDamege;

	// Use this for initialization
	void Start () {
        mMachine = this.transform.parent.parent.GetComponent<Machine>();
        Sparks = Instantiate((GameObject)Resources.Load("Detailed Smoke"), this.transform.position, this.transform.rotation) as GameObject;
        MusicDamege = Instantiate((GameObject)Resources.Load("Small explosion"), this.transform.position, this.transform.rotation) as GameObject;
        MusicDamege.transform.parent = this.transform;
        MusicDamege.SetActive(false);
        Sparks.transform.parent = this.transform;
        Sparks.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Sparks.activeSelf)
        {
            string partName = this.name;
            if (partName == "WheelRight" ||
                partName == "WheelLeft")
            {
                partName = "Wheel";
            }
            Sparks.SetActive(mMachine.breakParts[partName] > 0);
        }

        if (mMachine.speedDownTime <= 0)
        {
            MusicDamege.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (this.transform.parent.parent.gameObject.layer == 10) //INVISIBLE
            return;

        if (col.gameObject.tag == "item")
        {
            Item item = col.GetComponent<Item>();
            if (mMachine.Damage(this.name, item.getDamage()))
            {
                Sparks.SetActive(true);
            }
            item.itemAbility(mMachine);
            Destroy(col.gameObject);
            Destroy(col);
        }

        if (col.tag == "GMBullet")
        {
            GMBullet blt = col.GetComponent<GMBullet>();
            if (mMachine.Damage(this.name, blt.getDamage()))
            {
                Sparks.SetActive(true);
            }
            mMachine.spin();
            Destroy(col.gameObject);
            Destroy(col);
        }
        if (col.tag == "MusicBullet")
        {
            mMachine.speedDownTime = 10;
            Destroy(col.gameObject);
            MusicDamege.SetActive(true);
            Destroy(col);
        }
        if (col.tag == "DesignBullet")
        {
            DesignBullet blt = col.GetComponent<DesignBullet>();
            if (mMachine.Damage(this.name, blt.getDamage()))
            {
                Sparks.SetActive(true);
            }
        }
        if (col.tag == "SportBullet")
        {
            PunchBullet blt = col.GetComponent<PunchBullet>();
            if (mMachine.Damage(this.name, blt.getDamage()))
            {
                Sparks.SetActive(true);
            }
            mMachine.spin();
            Destroy(col.gameObject);
        }

        if (col.tag == "wall")
        {
            if (mMachine.Damage(this.name, 2))
            {
                Sparks.SetActive(true);
            }
        }

        if (col.tag == "CPU" || col.tag == "Player")
        {
            if (col.gameObject != this.transform.parent.gameObject)
            {
                if (mMachine.Damage(this.name, 0.5f))
                {
                    Sparks.SetActive(true);
                }
                if (col.GetComponent<Machine>().Barrier)
                {
                    mMachine.spin();
                }
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "DashPanel")
        {
            mMachine.dashspeed = 250;
            //Debug.Log("aaa");
        }
    }
}
