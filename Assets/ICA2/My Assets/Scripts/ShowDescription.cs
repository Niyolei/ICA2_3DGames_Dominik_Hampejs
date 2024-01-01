using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDescription : MonoBehaviour
{
    [SerializeField]
    private GameObject description;
    // Start is called before the first frame update
    
    void Start()
    {
        DescriptionOff();
    }
   
    
    public void DescriptionOn()
    {
        description.SetActive(true);
    }
    
    public void DescriptionOff()
    {
        description.SetActive(false);
    }
}
