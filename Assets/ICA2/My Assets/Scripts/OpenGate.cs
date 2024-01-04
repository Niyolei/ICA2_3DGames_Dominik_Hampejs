using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenGate : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }
    
    public void Open()
    {
        obstacle.carving = false;
    }
}
