using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KapisalapiGameManager : MonoBehaviour
{
    [SerializeField] private List<OVRGrabbable> sticks;
    [SerializeField] private GameObject finalPositionToKapisalapi;
    [SerializeField] private GameObject initialPositionToKapisalapi;
    [SerializeField] private GameObject player;
    private OVRGrabbable activeStick;
    private int sticksEaten = 0;
    private bool gameFinished = false;
    private bool gameStarted = false;

    [SerializeField] private KapisalapiPlayerNavMesh kapinavMesh;
    [SerializeField] private AudioSource kapisalapiSmashing;
    [SerializeField] private Teletransportation teletransportationMan;
    
    [SerializeField] private opntionsOfMovement option;
    [SerializeField] private AudioClip part2ExperienceExplanation;
    // Start is called before the first frame update
    [Header("Volume")]
    [SerializeField] float volumenDownMom = 0.05f;


    private generalAudioManager gam;
        private enum opntionsOfMovement{ animation, blink};
    void Start()
    {
        this.gam = FindObjectOfType<generalAudioManager>();



        StartCoroutine(decreaseVolume());
        StartCoroutine(raiseVolumeAfterAudio(this.GetComponent<AudioSource>().clip.length));
    }

    IEnumerator decreaseVolume()
    {
        yield return null;
        this.gam.decreaseVolume(volumenDownMom);
    }
    IEnumerator raiseVolumeAfterAudio(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        this.gam.restartVolume();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetKey(KeyCode.R));
        if(Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.T))
            SceneManager.LoadScene("Start_UNESCO");

        if(this.gameStarted){
            this.kapisalapiGame();
        }
    }


    private void eat()
    {
        if (this.activeStick)
        {
            float distance = Vector3.Distance(this.kapinavMesh.transform.position, this.activeStick.gameObject.transform.position);

            if (distance < 0.5f)
            {
                this.kapisalapiSmashing.Play(); 
                this.kapinavMesh.gameObject.GetComponent<Animator>().SetBool("eat",true);
                this.sticks.Remove(this.activeStick);
                Destroy(this.activeStick.gameObject);
                this.sticksEaten++;
                StartCoroutine(setEatingToFalse());
            }
            //Debug.Log(distance);
        }

    }

    IEnumerator moveWithKapiSalapi(float waitTime){
        yield return new WaitForSeconds(waitTime);
        if(option == opntionsOfMovement.blink)
            this.teletransportationMan.changeToPointP();
        else if(option == opntionsOfMovement.animation){
            this.player.GetComponent<Animator>().enabled = true;
            this.player.GetComponent<Animator>().Play("FollowKapisalapi");

            }
    }

    private void kapisalapiGame(){
        if (sticksEaten < 3){
            foreach (OVRGrabbable stick in sticks)
            {
                if (stick.isGrabbed)
                {
                    this.activeStick = stick;
                    //Debug.Log("Hemos cogido el stick: " + stick.gameObject.name);
                }
            }

            if (this.activeStick && !this.activeStick.isGrabbed)
            {
                this.kapinavMesh.setDestinationToKapisalapi(this.activeStick.gameObject);
            }

            this.eat();
        }else if (!this.gameFinished){
            this.kapinavMesh.setDestinationToKapisalapi(finalPositionToKapisalapi);
            this.gameFinished= true;
            StartCoroutine(moveWithKapiSalapi(2.0f));
        }
    }

    public void starGame(){
        this.kapinavMesh.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        this.kapinavMesh.setDestinationToKapisalapi(this.initialPositionToKapisalapi);
        this.gameStarted = true;
    }

    IEnumerator setEatingToFalse(){
        yield return new WaitForSeconds(1.0f);
        this.kapinavMesh.gameObject.GetComponent<Animator>().SetBool("eat",false);

    }
    public void playAudio2(){
        this.GetComponent<AudioSource>().clip = part2ExperienceExplanation;
        this.gam.decreaseVolume(volumenDownMom);
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(raiseVolumeAfterAudio(this.GetComponent<AudioSource>().clip.length));

    }
}
