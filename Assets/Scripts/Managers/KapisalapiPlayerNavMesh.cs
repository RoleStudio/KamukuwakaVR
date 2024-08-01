using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KapisalapiPlayerNavMesh : MonoBehaviour
{

    private NavMeshAgent kapisalapiNavMesh;

    [SerializeField] private GameObject destination;
    // Start is called before the first frame update

    private void Awake()
    {
        this.kapisalapiNavMesh = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destination)
            this.kapisalapiNavMesh.destination = destination.transform.position;
    }

    public void setDestinationToKapisalapi(GameObject dest)
    {
        this.destination = dest;
    }
}
