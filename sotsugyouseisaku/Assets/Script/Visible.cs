using UnityEngine;
using System.Collections;

public class Visible : MonoBehaviour {

    enum LAYERNAME
    {
        DEFAULT = 0,
        INVISIBLE = 10
    };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.parent.parent.gameObject.layer = (int)LAYERNAME.INVISIBLE;
	}
    /*  くっそ重いよ！
    void OnWillRenderObject()
    {
        transform.parent.parent.gameObject.layer = (int)LAYERNAME.DEFAULT;
        Debug.Log("visible");
    }
    */

    void OnBecameVisible()
    {
        transform.parent.parent.gameObject.layer = (int)LAYERNAME.DEFAULT;
        transform.parent.parent.FindChild("TriggerObject").gameObject.layer = (int)LAYERNAME.DEFAULT;
    }

    void OnBecameInvisible()
    {
        transform.parent.parent.gameObject.layer = (int)LAYERNAME.INVISIBLE;
        transform.parent.parent.FindChild("TriggerObject").gameObject.layer = (int)LAYERNAME.INVISIBLE;
    }
    
    
}
