using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public GameObject[] empolyees;
    public Transform[] randomLocs;
    public GameObject[] holes;
    public GameObject[] computers;
    public Transform fridgeLoc;

    private float timerMax = 10;
    private float timerMin = 1;
    private float timer = 0;

    private int eventCount = 0;
    private int eventCountChange = 1;


    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        timer = timer + Time.deltaTime;

        if (timer >= timerMax)
        {
            StartRandomEvent();
        }
    }

    public void StartRandomEvent()
    {
        timer = 0;
        if (timerMax > timerMin)//accelerates the rate that events happen
        {
            eventCount++;
            if (eventCount >= eventCountChange)
            {
                eventCount = 0;
                eventCountChange++;
                timerMax--;
            }
        }

        int randomNum = Random.Range(0,4);

        if(randomNum == 0)
        {
            IgniteComp();
        }
        else if(randomNum == 1)
        {
            AIWonder();
        }
        else if (randomNum == 2)
        {
            AIGetFood();
        }
        else if (randomNum == 3)
        {
            OpenHole();
        }


    }

    public void IgniteComp()
    {
        int tries = 0;
        while(tries < 4)
        {
            int randomNum = Random.Range(0, computers.Length);

            if(computers.Length != 0)
            {
                ComputerManager compCon = computers[randomNum].GetComponent<ComputerManager>();
                if(compCon.GetFire() == false)
                {
                    compCon.Ignite();
                    tries = 4;
                }
            }
            tries++;
        }
    }

    public void AIWonder()
    {
        int tries = 0;
        while (tries < 4)
        {
            int randomNum = Random.Range(0, empolyees.Length);

            if (computers.Length != 0)
            {
                EmpolyeeController compCon = empolyees[randomNum].GetComponent<EmpolyeeController>();
                if (compCon.GetIsHome() == true && compCon.GetDead() == false)
                {
                    int randomNum2 = Random.Range(0, randomLocs.Length);
                    int randomWaitTime = Random.Range(0, 4);
                    compCon.SetPath(randomLocs[randomNum2], randomWaitTime);
                    tries = 4;
                }
            }
            tries++;
        }
    }

    public void AIGetFood()
    {
        int tries = 0;
        while (tries < 4)
        {
            int randomNum = Random.Range(0, empolyees.Length);

            if (computers.Length != 0)
            {
                EmpolyeeController compCon = empolyees[randomNum].GetComponent<EmpolyeeController>();
                if (compCon.GetIsHome() == true && compCon.GetDead() == false)
                {
                    compCon.SetPath(fridgeLoc, 0);
                    tries = 4;
                }
            }
            tries++;
        }
    }

    public void OpenHole()
    {
        int tries = 0;
        while (tries < 4)
        {
            int randomNum = Random.Range(0, holes.Length);

            if (holes.Length != 0)
            {
                HoleController compCon = holes[randomNum].GetComponent<HoleController>();
                if (compCon.GetOpen() == false)
                {
                    compCon.Open();
                    tries = 4;
                }
            }
            tries++;
        }
    }
}
