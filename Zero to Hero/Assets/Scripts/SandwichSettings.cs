using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichSettings : MonoBehaviour {

    public GameObject poison;
    private bool isPoison = false;

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */

    public void GenerateRandom()
    {
        int randomNum = Random.Range(0, 2);
        if(randomNum == 0)
        {
            AddPoison();
        }
        else
        {
            RemovePoison();
        }
    }

    public void AddPoison()
    {
        isPoison = true;
        poison.SetActive(true);
    }

    public void RemovePoison()
    {
        isPoison = false;
        poison.SetActive(false);
    }

    public bool GetPoison()
    {
        return isPoison;
    }
}
