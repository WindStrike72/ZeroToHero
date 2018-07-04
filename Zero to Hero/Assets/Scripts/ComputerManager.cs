using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour {

    private bool burning = false;
    public GameObject fire;

    //temporary: used as a ignition timer for 
    //private float fireTime = 10;
    //private float maxFireTime = 10;

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    */
    public void Extinguish()
    {
        burning = false;
        fire.SetActive(false);
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
