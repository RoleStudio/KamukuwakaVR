using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShow : MonoBehaviour
{

    public GeneralMenu genMenu;
    public ActionButton actButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deployMenu()
    {
        this.genMenu.deployMenu();
    }

    public void playVideo()
    {
        //this.actButton.playVideo();
    }
}
