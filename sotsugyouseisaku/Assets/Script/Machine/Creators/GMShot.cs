using UnityEngine;
using System.Collections;

public class GMShot : Ability {

    public GameObject GMShotPrefab;
    public float speed;
    public static int count;
    public bool shot;
    //追加
    public AudioClip creatersSE;

	// Use this for initialization
	void Start () {
        speed = 20000;
        time = 0;
        waitTime = 10;
        count = 30;
        shot = false;

        GMShotPrefab = (GameObject)Resources.Load("GMBullet");
	}
	
	// Update is called once per frame
	public void Update () {

        time += Time.deltaTime;
	}

    public void Shot()
    {
        if (time >= waitTime)
        {
            //追加
            audio.PlayOneShot(creatersSE);

            GameObject obj = Instantiate(GMShotPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj.transform.position += new Vector3(
                20 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad),
                10,
                20 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
            obj.transform.Rotate(0, 90, 90);

            GameObject obj2 = Instantiate(GMShotPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj2.transform.position += new Vector3(
                20 * Mathf.Sin((transform.eulerAngles.y + 90) * Mathf.Deg2Rad),
                10,
                20 * Mathf.Cos((transform.eulerAngles.y + 90) * Mathf.Deg2Rad));
            obj2.transform.Rotate(0, 90, 90);

            GameObject obj3 = Instantiate(GMShotPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj3.transform.position += new Vector3(
                20 * Mathf.Sin((transform.eulerAngles.y - 90) * Mathf.Deg2Rad),
                10,
                20 * Mathf.Cos((transform.eulerAngles.y - 90) * Mathf.Deg2Rad));
            obj3.transform.Rotate(0, 90, 90);

            Vector3 force = new Vector3(this.rigidbody.velocity.x, 0, this.rigidbody.velocity.z) * speed / 200;
            force += this.gameObject.transform.forward * speed;
            obj.rigidbody.AddForce(force);
            obj2.rigidbody.AddForce(force);
            obj3.rigidbody.AddForce(force);
            time = 0;
        }
    }
}
