using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class Source_secondaire : MonoBehaviour
{
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
        entree = false;
    }

    // Update is called once per frame
    void Update()
    {   
        dist = Vector3.Distance(chaud.transform.position, transform.position);
        if (entree) {
            tau = 4f;
        }
        else {
            tau = 8f;
        }
        if(dist >0){
            
            if (temps<tau) {
                temps += Time.deltaTime;
                GetComponent<Shape>().T_temporel = t_h * Mathf.Exp(-temps * GetComponent<Shape>().coef);
            }
            else {
                transform.position = chaud.transform.position;
                temps = 0.0f;
            }
                   
        }
        
    }
}
