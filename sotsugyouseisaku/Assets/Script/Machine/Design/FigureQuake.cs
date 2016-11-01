using UnityEngine;
using System.Collections;

public class FigureQuake : Ability {

    public GameObject DesignPrefab;
    public float speed;
    public static int count;
    
    public float vec_y;

    // Use this for initialization
    void Start()
    {
        speed = 150000;
        time = 0;
        waitTime = 15;

        count = 30;
        vec_y = 0.01f;


        DesignPrefab = (GameObject)Resources.Load("DesignBullet");
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;

    }

    public void Figure()
    {
        if (time >= waitTime)
        {
            GameObject obj = Instantiate(DesignPrefab, this.transform.position, this.transform.rotation) as GameObject;
            obj.transform.position += new Vector3(20 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 30, 20 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
            obj.transform.Rotate(0, 180, 0);
            Vector3 force;
            force = (this.gameObject.transform.forward + new Vector3(0, vec_y, 0)) * speed;
            obj.rigidbody.AddForce(force);
            time = 0;
        }
    }
}
