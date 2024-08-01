using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GeneralMenu : MonoBehaviour
{
    // Start is called before the first frame update

    //private AudioSource audioS;
    //private VideoPlayer videoP;
    //private Texture imageTexture;
    private InteractionManager intMan;

    [SerializeField] private InteractionManager.engraving engravingGroup;

    [Header("Focos")]
    [SerializeField] private GameObject focusSelectable;
    [SerializeField] private GameObject focusSelected;

    [Header("Botones")]
    [SerializeField] Transform[] positions;
    ActionButton[] actionButtons;
    //[SerializeField] private Animator animPantalla;
    //[SerializeField] private Animator animImagePlane;

    [SerializeField] private Material pantallaMat;


    private bool selectable = true;
    private bool inInteraction = false;

    private void Awake()
    {
    }

    void Start()
    {
        this.actionButtons = this.GetComponentsInChildren<ActionButton>();
        //this.videoP = this.gameObject.GetComponentInChildren<VideoPlayer>();

        if (this.actionButtons.Length <= this.positions.Length)
        {
            for (int i = 0; i < this.actionButtons.Length; i++)
            {
                this.actionButtons[i].setPositions(positions[i]);
            }
        }
        else
        {
            Debug.LogError("No hay suficientes posiciones para los botones que existen");
        }


        this.intMan = FindObjectOfType<InteractionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Enter " + other.tag);
        if (!other.tag.Equals("Selecter"))
            return;


        Debug.Log("The Enter is a Selecter");

        if (!this.inInteraction)
        {
            this.inInteraction = true;
            if (this.selectable)
            {
                deployMenu();            
            }
            else if (!this.selectable)
            {
                this.selectable = !this.selectable;
                this.resetMenu();
            }
        }
     
    }

    private void OnTriggerExit(Collider other)
    {

        this.inInteraction = false;
    }

    public void deployMenu()
    {
        //this.GetComponent<Animator>().Play("Despliegue");
        foreach(ActionButton actionButton in this.actionButtons)
        {
            actionButton.deployMenu();
        }

        this.focusSelectable.SetActive(false);
        this.focusSelected.SetActive(true);
        this.selectable = !this.selectable;
        this.intMan.inactiveInteractable(this.transform.parent.gameObject);

    }

    public void playAudio(string audio)
    {
        stopImages();
        stopVideo();    
    }

    public void playVideo(string video)
    {
        stopAudio();
        stopImages();
    }

    
    public void showImage(string image)
    {
        stopVideo();
        stopAudio();
    }

    public void stopVideo()
    {
        
    }

    public void stopAudio()
    {
      
    }

    public void stopImages()
    {
        
    }

    public void stopInteractionsActive()
    {
        foreach (ActionButton actionButton in this.actionButtons)
        {
            actionButton.deployMenu();
        }
        //Debug.Log("stop interactions called");
    }
    private void resetMenu()
    {
        foreach (ActionButton actionButton in this.actionButtons)
        {
            actionButton.hideMenu();
        }
        this.stopVideo();
        this.stopImages();
        //this.GetComponent<Animator>().Play("CerrarMenu");
        this.focusSelectable.SetActive(true);
        this.focusSelected.SetActive(false);
        this.intMan.activeInteractable();
        this.stopAudio();
    }

    public void resetMenuButton()
    {
        resetMenu();
    }

}
