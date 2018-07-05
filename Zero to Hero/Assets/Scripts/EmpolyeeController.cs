using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmpolyeeController : MonoBehaviour
{

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

    private GameObject hole;//show sthe howl this ai is hanging from
    private bool falling = false;
    private bool hanging = false;
    private bool climbing = false;

    private float hangTime = 0.5f;
    private float hangTimeMax = 0;
    private float fallTime = 0;
    private float fallTimeMax = 1;
    private float climbTime = 0;
    private float climbTimeMax = 0.75f;

    public float groundLevel = -0.5f;


    private static int DeathCount = 0;

    public GameObject sandwich;
    public GameObject poison;
    private bool isPoisoned = false;
    private bool hasSandwich = false;



    // Use this for initialization
    void Start()
    {
        navAgent = transform.parent.GetComponent<NavMeshAgent>();

        navAgent.SetDestination(deskLoc.position);
    }

    // Update is called once per frame
    void Update()
    {

        //hanndles time
        if (hanging == true)
        {
            lifeTime = lifeTime - Time.deltaTime;
            if (hangTime < hangTimeMax)
            {
                hangTime = hangTime + Time.deltaTime;
            }
            HandleHanging();
        }

        if (falling == true)
        {
            if (fallTime < fallTimeMax)
            {
                fallTime = fallTime + Time.deltaTime;
            }
            else
            {
                falling = false;//ends the fall
            }
            HandleFalling();
        }

        if (climbing == true)
        { 
            HandleClimb();
        
            if (climbTime < climbTimeMax)
            {
                climbTime = climbTime + Time.deltaTime;
            }
            else
            {
                EndClimb();
            }
        }

        if (burning == true)
        {
            lifeTime = lifeTime - Time.deltaTime;

        }
        else
        {
            if (fireCD > 0)
            {
                fireCD = fireCD - Time.deltaTime;
            }
        }

        if (isPoisoned == true)
        {
            lifeTime = lifeTime - Time.deltaTime;

        }

        if (lifeTime <= 0)
        {
            die();
        }
        //end handling time
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
                if(hasSandwich == true)
                {
                    EatSandwich();
                }
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
            if (waitTime <= 0)
            {
                returning = true;
                navAgent.SetDestination(deskLoc.position);
            }
        }
    }

    public void die()
    {
        DeathCount++;
        dead = true;
        navAgent.enabled = false;
        GetComponent<Collider>().enabled = false;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        if(hanging == true)
        {
            falling = true;
            hole.GetComponent<HoleController>().SetVictim(null);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Hole")
        {
            if (other.gameObject.GetComponent<HoleController>().HasVictim() == false && other.gameObject.GetComponent<HoleController>().GetOpen() == true)
            {
                other.gameObject.GetComponent<HoleController>().SetVictim(gameObject);
                hole = other.gameObject;
                navAgent.enabled = false;
                hanging = true;
            }
        }
        else if (other.transform.tag == "Fridge")
        {
            GetSandwich();
        }
    }

    public void SetPath(Transform destination, float wait)
    {
        leaving = true;
        isHome = false;
        //test returing = false
        returning = false;
        waitTime = wait;
        navAgent.SetDestination(destination.position);
    }

    public bool GetIsHome()
    {
        return (isHome);
    }

    private void HandleFalling()
    {
        float newY = -8f;
        if (fallTime < fallTimeMax)
        {
            newY = ((fallTime / fallTimeMax) * -6f) - 2f;
        }
        transform.position = new Vector3(transform.position.x, newY + groundLevel, transform.position.z);
    }

    private void HandleHanging()
    {
        float newY = -2f;
        if (hangTime < hangTimeMax)
        {
            newY = (hangTime / hangTimeMax) * -2f;
        }
        transform.position = new Vector3(transform.position.x, newY + groundLevel, transform.position.z);
    }

    private void HandleClimb()
    {
        float newY = 0;
        if (climbTime < climbTimeMax)
        {
            newY = ((climbTime / climbTimeMax) * 2f) -2f;
        }
        transform.position = new Vector3(transform.position.x, newY + groundLevel, transform.position.z);
    }

    public void Climb()
    {
        climbing = true;
        hanging = false;
        hole.GetComponent<HoleController>().CloseIgnoringVictim();
        hole.GetComponent<HoleController>().SetVictim(null);
        hole = null;
        lifeTime = lifeTimeMax;
    }

    private void EndClimb()
    {
        climbing = false;
        hangTime = 0;
        climbTime = 0;
        navAgent.enabled = true;
        //navAgent.SetDestination(navAgent.destination);
    }

    public bool GetHanging()
    {
        return hanging;
    }

    public void GetSandwich()
    {
        sandwich.SetActive(true);
        sandwich.GetComponent<SandwichSettings>().GenerateRandom();
        hasSandwich = true;

    }

    public void EatSandwich()
    {
        if(sandwich.GetComponent<SandwichSettings>().GetPoison() == true)
        {
            ActivatePoison();
        }
        hasSandwich = false;
        sandwich.SetActive(false);
    }

    public void ActivatePoison()
    {
        poison.SetActive(true);
        isPoisoned = true;
    }

    public void DeactivatePoison()
    {
        poison.SetActive(false);
        isPoisoned = false;
        lifeTime = lifeTimeMax;
    }

    public bool GetPoison()
    {
        return isPoisoned;
    }
}
