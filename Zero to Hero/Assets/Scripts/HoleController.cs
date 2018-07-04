using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {

    private bool active = false;
    private float activeTime = 0;
    private float activeTimeMax = 1.5f;
    private float closingTimeMax = 1.5f;
    private float closingTime = 1.5f;
    public GameObject Cover;

    private GameObject victim = null; 


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandAnimation();

    }

    public void HandAnimation()
    {
        if(active == true)
        {
            float tileZ = -0.06f;
            if(activeTime < activeTimeMax)
            {
                activeTime = activeTime + Time.deltaTime;
                tileZ = (activeTime/ activeTimeMax) * -0.06f;
            }

            Cover.transform.localPosition = new Vector3(0, 0, tileZ);
        }
        else
        {
            float tileX = 0;
            if (closingTime < closingTimeMax)
            {
                closingTime = closingTime + Time.deltaTime;
                tileX = -0.02f + ((closingTime / closingTimeMax) * 0.02f);
            }

            Cover.transform.localPosition = new Vector3(tileX, 0, 0);

        }
    }

    public void Close()
    {
        if (victim != null)
        {
            victim.GetComponent<EmpolyeeController>().Climb();
        }
        else
        {
            active = false;
            activeTime = 0;
            closingTime = 0;
        }
    }


    public void CloseIgnoringVictim()
    {
        active = false;
        activeTime = 0;
        closingTime = 0;
    }

    public void Open()
    {
        active = true;
        activeTime = 0;
        closingTime = 0;
    }

    public bool GetOpen()
    {
        return (active);
    }

    public bool HasVictim()
    {
        bool hasVictim = true;
        if(victim == null)
        {
            hasVictim = false;
        }
        return hasVictim;
    }

    public void SetVictim(GameObject newVictim)
    {
        victim = newVictim;
    }
}
