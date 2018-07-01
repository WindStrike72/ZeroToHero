using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour {

    public float parentX = 0;
    public float parentY = 0;
    public float parentZ = 0;
    public float parentRotX = 0;
    public float parentRotY = 0;
    public float parentRotZ = 0;
    public float throwSpeed = 5;
    private Transform parentTransform = null;
    private bool use = false;

    /*
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */

    public void ParentTo(Transform newParent)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        transform.SetParent(newParent);
        transform.localPosition = new Vector3(parentX, parentY, parentZ);
        transform.localRotation = Quaternion.Euler(parentRotX, parentRotY, parentRotZ);

    }

    public void UnParent()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        transform.parent = null;
        
    }

    public void Throw()
    {
        Vector3 dir = transform.parent.forward;
        UnParent();
        GetComponent<Rigidbody>().velocity = dir * throwSpeed;
    }

    public void SetUse(bool setting)
    {
        use = setting;
    }

    public bool GetUse()
    {
        return (use);
    }

}
