using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audiosEnviro;
    private List<float> volumes = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
       this.audiosEnviro = GetComponents<AudioSource>();

        foreach (AudioSource audio in this.audiosEnviro)
        {
            this.volumes.Add(audio.volume);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseVolume(float audioVolume)
    {
        foreach (AudioSource audio in this.audiosEnviro)
        {
            audio.volume = audioVolume;
        }

    }

    public void restartVolume ()
    {
        for (int i = 0; i<this.audiosEnviro.Length; i++)
        {
            this.audiosEnviro[i].volume = this.volumes[i];
        }
    }
}
