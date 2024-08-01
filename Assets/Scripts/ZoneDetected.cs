using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetected : MonoBehaviour
{
    [SerializeField] private GameObject petroglifo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger launched");
        if (other.tag == "MainCamera")
        {
            this.petroglifo.GetComponent<Animator>().Play("Petro Expose");
            this.petroglifo.GetComponent<AudioSource>().Play();
        }

    }
}
