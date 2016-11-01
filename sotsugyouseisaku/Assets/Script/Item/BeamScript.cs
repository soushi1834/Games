using UnityEngine;
using System.Collections;

public class BeamScript : Item
{
    public float desTime;
    public float speed;

    public AudioClip beamSE;
    private AudioSource source;

    // Use this for initialization
    protected override void Start()
    {
        desTime = 3.0f;
        speed = 2500;
        damage = 5;

        this.transform.position += new Vector3(30 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 10, 30 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
        this.gameObject.rigidbody.velocity = force * speed;
        //追加
        source = transform.GetComponent<AudioSource>();
        //追加
        source.PlayOneShot(beamSE);

        Destroy(gameObject, desTime);
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.gameObject.rigidbody.velocity = rigidbody.velocity.normalized * speed;
        this.gameObject.transform.Rotate(0, 0, Random.Range(-179, 180));
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "wall")
            Destroy(this.gameObject);
    }

    public override void itemAbility(Machine target)
    {
        target.spin();
    }
}
