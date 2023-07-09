using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class Sources_secondaires : MonoBehaviour
{
    Vector3 centre = new Vector3(10,0,0);
    float temps = 0.0f;
    public float dist = 0.0f;
    float t_h = 0.0f;
    GameObject chaud;
    GameObject barre;
    bool entree = false;
    float tau = 8f;
    // Start is called before the first frame update
    void Start()
    {
        chaud = GameObject.Find("Corps chaud");
        barre = GameObject.Find("Barre");
        t_h = chaud.GetComponent<Shape>().T_temporel;
        entree = (barre.GetComponent<ConductiviteTemps>().dist < 1f);
    }

    // Update is called once per frame
    void Update()
    {   
        dist = chaud.GetComponent<Shape>().GetShapeDistance(transform.position);
        
        if(dist > 0){
            temps += Time.deltaTime;
            GetComponent<Shape>().T_temporel = t_h * Mathf.Exp(-temps * GetComponent<Shape>().coef);
        }
        else {
                temps = 0.0f;
                GetComponent<Shape>().T_temporel = t_h;
            }
                      
            
                
    }
}
