using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour {
    
    private Holdable holdable;
    private bool used;

    // Use this for initialization
    void Start()
    {
        holdable = GetComponent<Holdable>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckUse();
    }

    private void CheckUse()
    {
        if (holdable.GetUse() == true)
        {

            if (used == false)
            {
                used = true;
            }

            if (holdable.GetParented() == true)
            {
                CheckExstinguish();
            }
        }
        else
        {
            if (used == true)
            {
                used = false;

            }
        }

    }

    private void CheckExstinguish()
    {

        Ray ray = new Ray(transform.parent.transform.position, transform.parent.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.transform.position, transform.parent.transform.forward, out hit, 4))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.transform.tag == "Empolyee")
            {
                if (hitObject.GetComponent<EmpolyeeController>().GetPoison() == true)
                {
                    hitObject.GetComponent<EmpolyeeController>().DeactivatePoison();
                }
            }

        }
    }

}
