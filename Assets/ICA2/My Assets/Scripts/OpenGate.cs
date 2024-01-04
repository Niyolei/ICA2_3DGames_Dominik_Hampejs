using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class OpenGate : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    public GameObject leftGate;
    public GameObject rightGate;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }
    
    public void Open()
    {
        obstacle.enabled = false;
        leftGate.transform.DORotate(new Vector3(0,-90,0),1);
        rightGate.transform.DORotate(new Vector3(0, 90, 0), 1f);
    }
}
