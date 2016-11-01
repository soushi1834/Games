using UnityEngine;
using System.Collections;

public class SportAbility : Ability {

    public GameObject SportPrefab;
    public float speed;
    public static int count;
    //追加
    private AudioSource source;
    public AudioClip sportsSE;
    

    // Use this for initialization
    void Start()
    {
        speed = 20000;
        time = 0;
        waitTime = 15;
        count = 30;
        

        SportPrefab = (GameObject)Resources.Load("SportBullet");
        //追加
        source = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {

        time += Time.deltaTime;
    }

    public void Punch(GameObject machine)
    {
        if (time >= waitTime)
        {
            GameObject obj = Instantiate(SportPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj.transform.position += new Vector3(30 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 0, 30 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
            obj.transform.Rotate(0, 0, 0);
            PunchBullet punch = obj.GetComponent<PunchBullet>();
            punch.setMachine(machine.gameObject);
            time = 0;
            //追加
            source.PlayOneShot(sportsSE);
        }
    }
}
