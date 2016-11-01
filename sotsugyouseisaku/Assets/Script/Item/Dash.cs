using UnityEngine;
using System.Collections;

public class Dash : Item {

    private Vector3 vec;
    private float speed;
    //追加
    private AudioSource source;
    public AudioClip se;

	// Use this for initialization
	protected override void Start () {
        speed = 500;
        vec = new Vector3(Mathf.Sin(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad));

        ItemEffect = Instantiate((GameObject)Resources.Load("DashEffect"), this.transform.position, this.transform.rotation) as GameObject;
        ItemEffect.transform.parent = this.transform;
        ItemEffect.SetActive(true);
        effectTime = 0;

        //追加
        source = transform.GetComponent<AudioSource>();
        audio.PlayOneShot(se);

        if (mMachine.GetComponent<Machine>().speed < 0)
        {
            mMachine.GetComponent<Machine>().speed = 0;
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
        vec = new Vector3(Mathf.Sin(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(mMachine.transform.eulerAngles.y * Mathf.Deg2Rad));
        mMachine.rigidbody.velocity += vec * speed;
        if (mMachine.GetComponentInChildren<Driver>().Name == "Player1")
        {
            SpeedUI.SpeedUp(speed);
        }
        if (mMachine.GetComponentInChildren<Driver>().Name == "Player2")
        {
            SpeedUI2P.SpeedUp(speed);
        }
        speed -= 250 * Time.deltaTime;
        if (speed <= 0)
        {
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
