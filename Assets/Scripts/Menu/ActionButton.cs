using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ActionButton : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    [SerializeField] [Range(0f, 4f)] float lerpTime;
    [SerializeField] float scaleMultiplier;


    [Header("Materiales")]
    [SerializeField] private Material materialInactive;
    [SerializeField] private Material materialActive;


    [Header("Acciones")]
    [SerializeField] private action actionToDo;
    [SerializeField] private string targetName;
    private GeneralMenu gMenu;
    private Transform positionHide;
    private Transform positionEnabled; //Posicion de despliegue en el menu
    //private Transform positionOnScreen; //Posicion cuando esta en grande a la derecha
    //private float t = 0f;

    private Transform activePosition;
    private bool isActive = false;


    [SerializeField] private Transform targetToScreen;
    [SerializeField] private Transform Origin;


    [Header("Audio")]
    [SerializeField] private AudioClip clipAudio;
    private AudioSource audioSource;


    [Header("Video")]
    [SerializeField] private VideoPlayer videoP;

    [Header("Volume")]
    [SerializeField] float volumenDownMom = 0.05f;

    private bool videoActivefirstFrame = false;


    private generalAudioManager gam;

    private enum action
    {
        video,
        audio,
        image
    }
    void Start()
    {
        this.gMenu = this.gameObject.GetComponentInParent<GeneralMenu>();
        //this.positionHide = GetComponentInParent<GeneralMenu>().transform;
        //this.positionHide.localScale = new Vector3(0, 0, 0);
        //this.positionHide = new Transform(new Vector3(), new Vector3(), new Quaternion());
        if(Origin)
            this.positionHide = Origin;
        else
        {
            this.positionHide = GetComponentInParent<GeneralMenu>().transform;
            this.positionHide.localScale = new Vector3(0, 0, 0);
        }
        this.activePosition = this.positionHide;

        if (this.clipAudio)
        {
            this.audioSource = GetComponent<AudioSource>();
            this.audioSource.clip = clipAudio;
        }

        this.gam = FindObjectOfType<generalAudioManager>();

        StartCoroutine(stopVideosFirstFrame(0.5f));
    }


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entra en el boton " + other.tag);
        if (other.tag.Equals("Selecter"))
            if (!this.isActive)
            {
                activeMenu();
                StartCoroutine(switchActive());
            }
            else
            {
                deployMenu();
                StartCoroutine(switchActive());

            }


    }

    IEnumerator stopVideosFirstFrame(float waittime)
    {
        yield return new WaitForSeconds(waittime);

        if (this.actionToDo.Equals(action.video) && !videoActivefirstFrame)
        {
            Debug.Log("Desactivado en el primer frame");
            this.videoP.Pause();
            videoActivefirstFrame = true;


        }
    }
    private void Update()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, activePosition.localPosition, lerpTime * Time.deltaTime);
        this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x, activePosition.localScale.x * this.scaleMultiplier, lerpTime * Time.deltaTime), Mathf.Lerp(this.transform.localScale.y, activePosition.localScale.y * this.scaleMultiplier, lerpTime * Time.deltaTime), Mathf.Lerp(this.transform.localScale.z, activePosition.localScale.z * this.scaleMultiplier, lerpTime * Time.deltaTime));
        this.transform.rotation = new Quaternion(Mathf.Lerp(this.transform.rotation.x, activePosition.rotation.x, lerpTime * Time.deltaTime), Mathf.Lerp(this.transform.rotation.y, activePosition.rotation.y, lerpTime * Time.deltaTime), Mathf.Lerp(this.transform.rotation.z, activePosition.rotation.z, lerpTime * Time.deltaTime), this.transform.rotation.w);

        
        //Debug.Log(this.gameObject.name + " se mueve: " + this.transform.position);
        //this.t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        //float distance = Vector3.Distance(this.transform.position, position);
    }

    private  void playVideo()
    {
        this.gMenu.stopInteractionsActive();
        this.gam.decreaseVolume(volumenDownMom);
        videoP.Play();
    }
    private void pauseVideo()
    {
        this.gam.restartVolume();
        videoP.Pause();
    }
    private void playAudio()
    {
        this.gMenu.stopInteractionsActive();
        this.gam.decreaseVolume(volumenDownMom);
        Debug.Log("Play Audio");
        this.audioSource.Play();
    }
    private void pauseAudio()
    {
        this.gam.restartVolume();
        this.audioSource.Pause();
    }

    private void playImage()
    {
        this.gMenu.stopInteractionsActive();
    }
    private void pauseImage()
    {
        //this.deployMenu();
    }
    IEnumerator switchActive()
    {
        yield return new WaitForSeconds(0.5f);
        this.isActive = !this.isActive;
    }


    private  void stopAction()
    {
        switch (actionToDo)
        {
            case action.audio:
                print("STOP AUDIO");
                this.pauseAudio();
                break;
            case action.video:
                print("STOP VIDEO");
                this.pauseVideo();
                break;
            case action.image:
                print("STOP IMAGE");
                this.pauseImage();
                break;
        }
    }

    private void playAction()
    {
        switch (actionToDo)
        {
            case action.audio:
                //print("AUDIO played");
                playAudio();
                break;
            case action.video:
                print("VIDEO");
                this.playVideo();
                break;
            case action.image:
                print("IMAGE");
                this.playImage();
                break;
        }
    }

    public void deployMenu()
    {
        stopAction();
        this.activePosition = this.positionEnabled;
        this.changeMaterial(materialInactive);
    }
    public void hideMenu()
    {
        stopAction();
        this.activePosition = this.positionHide;
        this.changeMaterial(materialInactive);
    }
    public void activeMenu()
    {
        playAction();
        this.changeMaterial(materialActive);
        if (this.actionToDo != action.audio)
            this.activePosition = this.targetToScreen;
        else
            this.activePosition = this.positionEnabled;
    }


    public void setPositions(Transform enabledPosition)
    {
        this.positionEnabled = enabledPosition;
    }

    private void changeMaterial(Material material)
    {
        this.GetComponent<Renderer>().material = material;


    }
}
