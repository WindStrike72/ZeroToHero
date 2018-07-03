using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEx : MonoBehaviour
{
    public GameObject whiteSmoke;
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
                //activate smoke
                whiteSmoke.SetActive(true);
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
                //deactivate smoke
                whiteSmoke.SetActive(false);

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
            if (hitObject.transform.tag == "Computer")
            {
                if (hitObject.GetComponent<ComputerManager>().GetFire() == true)
                {
                    hitObject.GetComponent<ComputerManager>().Extinguish();
                }
            }

        }
    }

}
