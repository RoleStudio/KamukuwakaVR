using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teletransportation : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private GameObject teletransportationPoint;
    [SerializeField] private GameObject player;

    [Header("Image")]
    [SerializeField] private Image imagefaded;


    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
    private void fadeIn()
    {
        StartCoroutine(this.FadeImage(true));
    }

    private void fadeOut()
    {
        StartCoroutine(this.FadeImage(false));
    }


    IEnumerator changeToPoint()
    {
        this.fadeOut();

        while (isFading)
        {
            yield return null;
        }

        this.changePosition();
        this.fadeIn();
    }


    private void changePosition()
    {

        Debug.Log("Hemos cambiado la posici�n");
        this.player.transform.position = this.teletransportationPoint.transform.position;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        isFading = true;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                imagefaded.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                imagefaded.color = new Color(0, 0, 0, i);
                yield return null;
            }
       }
        isFading = false;
    }

        private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cambió");
        StartCoroutine(changeToPoint());
    }

    public void changeToPointP(){
        StartCoroutine(changeToPoint());
    }
}
