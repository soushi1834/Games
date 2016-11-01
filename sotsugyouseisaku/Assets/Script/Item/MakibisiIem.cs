using UnityEngine;
using System.Collections;

public class MakibisiIem : Item{

    //追加
    private AudioSource source;
    public AudioClip makibishiSE;

    // Use this for initialization
    protected override void Start()
    {
        this.transform.position += new Vector3(-40 * Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad), 10, -40 * Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad));
        //追加
        source = transform.GetComponent<AudioSource>();
        source.PlayOneShot(makibishiSE);
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
    //ヒットしたらスピン
    public override void itemAbility(Machine target)
    {
        target.spin();
    }
}
