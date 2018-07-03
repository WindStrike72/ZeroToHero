using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpolyeeController : MonoBehaviour {

    private bool burning = false;
    public GameObject fire;
    private float fireCD = 0;//stops the character form chtching fire as soon as its put out
    private float fireCDMax = 3;

    private float lifeTime = 15;
    private float lifeTimeMax = 15;

    private bool dead = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(burning == true)
        {
            lifeTime = lifeTime - Time.deltaTime;
            
        }
        else
        {
            if(fireCD > 0)
            {
                fireCD = fireCD - Time.deltaTime;
            }
        }

        if (lifeTime <= 0)
        {
            die();
        }
    }

    public void die()
    {
        GetComponent<Collider>().enabled = false;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void Extinguish()
    {
        burning = false;
        fire.SetActive(false);
        fireCD = fireCDMax;
        lifeTime = lifeTimeMax;
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

    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Computer")
        {
            if (other.gameObject.GetComponent<ComputerManager>().GetFire() == true)
            {
                if (fireCD <= 0)
                {
                    Ignite();
                }
            }
        }
    }
}
