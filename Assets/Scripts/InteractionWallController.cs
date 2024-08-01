using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWallController : MonoBehaviour
{

    [SerializeField] private AudioSource audioPetroglifo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioPlay()
    {
        //this.audioPetroglifo.clip = ;
        this.audioPetroglifo.Play();
    }
}
