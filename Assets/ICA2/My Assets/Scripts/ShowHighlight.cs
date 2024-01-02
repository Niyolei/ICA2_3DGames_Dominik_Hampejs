using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowHighlight : MonoBehaviour
{
    [FormerlySerializedAs("description")] [SerializeField]
    private GameObject highlight;
    // Start is called before the first frame update
    
    void Start()
    {
        DescriptionOff();
    }
   
    
    public void DescriptionOn()
    {
        highlight.SetActive(true);
    }
    
    public void DescriptionOff()
    {
        highlight.SetActive(false);
    }
}
