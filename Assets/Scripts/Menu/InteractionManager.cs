using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] engravingsMenus;
    [SerializeField] private GameObject[] Seleceters;
    public enum engraving
    {
        group1,
        group2,
        group3,
        group4,
        group5,
        group6,
        group7,
        group8,
        group9,
        group10,
        group11,
        group12,
        group13,
        group14,
        group15,
        group16,
        group17
    }


    public void inactiveInteractable(GameObject engravingInteractiObj)
    {
        foreach (GameObject engrav in engravingsMenus)
        {
            if(engravingInteractiObj.name != engrav.name)
            {
                Debug.Log(engravingInteractiObj.name + " || " + engrav.name);
                engrav.SetActive(false);

            }
        }
        //engravingInteractiObj.SetActive(true);
    }
    public void activeInteractable()
    {
        foreach (GameObject engrav in engravingsMenus)
        {
            engrav.SetActive(true);
        }
    }

    private void Update()
    {
        foreach (GameObject selecter in Seleceters)
            selecter.GetComponent<Collider>().enabled = true;
    }
}
