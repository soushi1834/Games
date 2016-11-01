using UnityEngine;
using System.Collections;

public class GMBullet : MonoBehaviour {
    public float rotate;
    public float desTime;
    private Vector3 v;

	// Use this for initialization
	void Start () {
        rotate = 10;

        desTime = 3.0f;
        Destroy(gameObject, desTime);
	}
	
	// Update is called once per fram
	void Update () {
        rigidbody.velocity = rigidbody.velocity.normalized * 1500;
        this.gameObject.transform.Rotate(0, 0, rotate * Time.deltaTime * 60);
	}

    public int getDamage()
    {
        return 10;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "wall")
            Destroy(this.gameObject);
    }
}
