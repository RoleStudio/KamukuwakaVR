using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class startingScene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nextScene(_video.clip.length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator nextScene(double waitingTime)
    {
        Debug.Log(waitingTime);
        yield return new WaitForSeconds((float)waitingTime - 5.0f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("KapisalapiScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
