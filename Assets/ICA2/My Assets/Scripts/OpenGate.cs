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
    private AudioPlayer audioPlayer;
    
    private bool gateOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        audioPlayer = GetComponent<AudioPlayer>();
    }
    
    public void Open()
    {
        if (gateOpen)
        {
            return;
        }
        StartCoroutine(OpenWithSound());
    }
    
    private IEnumerator OpenWithSound()
    {
        audioPlayer.PlayAudio(1);
        yield return new WaitForSeconds(0.8f);
        audioPlayer.PlayAudio(2);
        obstacle.enabled = false;
        leftGate.transform.DORotate(new Vector3(0,-90,0),1.8f);
        rightGate.transform.DORotate(new Vector3(0, 90, 0), 1.8f);
        gateOpen = true;
        
    }
}
