using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmpolyeeController : MonoBehaviour {

    private bool burning = false;
    public GameObject fire;
    private float fireCD = 0;//stops the character form chtching fire as soon as its put out
    private float fireCDMax = 3;

    private float lifeTime = 15;
    private float lifeTimeMax = 15;

    private bool dead = false;

    public Transform deskLoc;
    private NavMeshAgent navAgent;

    //uesd for pathing
    private bool returning = true;
    public bool leaving = false;
    private float waitTime = 0;//shows how long it should wait before returning
    public bool isHome = false;

    // Use this for initialization
    void Start () {
        navAgent = transform.parent.GetComponent<NavMeshAgent>();

        navAgent.SetDestination(deskLoc.position);
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

        handleTravel();
    }

    private void handleTravel()
    {

        if (returning == true)
        {
            //if (navAgent.remainingDistance <= 0.1)

            //Debug.Log(Vector3.Distance(transform.position, navAgent.destination));
            //if (navAgent.remainingDistance <= 0.1)
            if (Vector3.Distance(transform.position, navAgent.destination) <= 0.1)
            {
                returning = false;
                isHome = true;
            }
        }


        if (leaving == true)
        {
            //if (navAgent.remainingDistance <= 0.1)
             if (Vector3.Distance(transform.position, navAgent.destination) <= 0.1)
            {
                leaving = false;
            }
        }

        if (leaving == false && returning == false)
        {
            waitTime = waitTime - Time.deltaTime;
            if(waitTime <= 0)
            {
                returning = true;
                navAgent.SetDestination(deskLoc.position);
            }
        }
    }

    public void die()
    {
        dead = true;
        navAgent.enabled = false;
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

    public bool GetDead()
    {
        return (dead);
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

    public void SetPath(Transform destination, float wait)
    {
        leaving = true;
        isHome = false;
        waitTime = wait;
        navAgent.SetDestination(destination.position);
    }

    public bool GetIsHome()
    {
        return(isHome);
    }
}
