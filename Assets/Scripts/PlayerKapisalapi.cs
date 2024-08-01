using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKapisalapi : MonoBehaviour
{
    [SerializeField] private KapisalapiGameManager kapiGM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleteAnimator(){
        this.GetComponent<Animator>().enabled = false;
        this.kapiGM.playAudio2();
    }

    
    public void starGame(){
        this.kapiGM.starGame();
        Destroy(this.gameObject);
    }
}
