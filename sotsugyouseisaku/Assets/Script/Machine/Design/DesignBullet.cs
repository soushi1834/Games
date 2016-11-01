using UnityEngine;
using System.Collections;

public class DesignBullet : MonoBehaviour {

    public float frame;
    //追加
    private AudioSource source;
    public AudioClip designSE;

	// Use this for initialization
	void Start () {
        frame = 0;
        this.gameObject.transform.position += new Vector3(20 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 100, 20 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
        //追加
        source = transform.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        frame += Time.deltaTime;
        if (frame >= 0.25f &&
            !rigidbody.isKinematic)
        {
            rigidbody.velocity = new Vector3(0,-1000,0);
        }

        if (frame >= 10.0f)
        {
            Destroy(gameObject);
        }
        
	}

    void OnCollisionEnter(Collision col)
    {
        //追加
        if (col.transform.tag == "plane" || col.transform.tag == "offroad")
        {
            source.PlayOneShot(designSE);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "plane" || col.transform.tag == "offroad")
        {
            if (!rigidbody.isKinematic)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;
            }
        }
    }

    public int getDamage()
    {
        return 3;
    }
}
