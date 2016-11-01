using UnityEngine;
using System.Collections;

public class ElectricGuitar : Ability {

    private GameObject MusicPrefab;
    public float speed;
    public static int count;
    //追加
    private AudioSource source;
    public AudioClip musicSE;


    // Use this for initialization
    void Start()
    {
        speed = 50000;
        time = 0;
        waitTime = 8;
        count = 30;


        MusicPrefab = (GameObject)Resources.Load("MusicBullet");
        //追加
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

        
    }

    public void Electric()
    {
        if (time >= waitTime)
        {
            //追加
            source.PlayOneShot(musicSE);
            GameObject obj = Instantiate(MusicPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj.transform.position += new Vector3(20 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 10, 20 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
            obj.transform.Rotate(0, 90, 90);
            Vector3 force;
            force = this.gameObject.transform.forward * speed;
            obj.rigidbody.AddForce(force);
            time = 0;
        }
    }
}
