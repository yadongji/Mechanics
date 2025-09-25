using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataControl : MonoBehaviour
{
    public TextMeshProUGUI vilocityA;
    public TextMeshProUGUI vilocityB;
    public TextMeshProUGUI distance;
    public void SetData(string vilocityA,string vilocityB,string distance) 
    {
        this.vilocityA.text = vilocityA;
        this.vilocityB.text = vilocityB;
        this.distance.text = distance;
    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
