using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour {

    private bool burning = true;
    public GameObject fire;

    //temporary: used as a ignition timer for 
    private float fireTime = 10;
    private float maxFireTime = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(burning == false)
        {
            fireTime = fireTime - Time.deltaTime;
            if(fireTime <= 0)
            {
                Ignite();
                fireTime = maxFireTime;
            }

        }
    }

    public void Extinguish()
    {
        burning = false;
        fire.SetActive(false);
        //transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
    }

    public void Ignite()
    {
        burning = true;
        fire.SetActive(true);
    }

    public bool GetFire()
    {
        return (burning);
    }
}
