using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class Source_secondaire : MonoBehaviour
{
    Vector3 centre = new Vector3(10,0,0);
    float rayon = 0.0f;
    float temps = 0.0f;
    int count =0;
    public float dist = 0.0f;
    //public Transform _target;
    GameObject chaud;
    // Start is called before the first frame update
    void Start()
    {
        chaud = GameObject.Find("Corps chaud");
    }

    // Update is called once per frame
    void Update()
    {   
        rayon = 1f;
        dist = GetComponent<Shape>().GetShapeDistance(chaud.transform.position);

        if(dist < rayon){
            temps += Time.deltaTime;
            GetComponent<Shape>().T_temporel = temps * GetComponent<Shape>().coef;
            
        }
        else {
            GetComponent<Shape>().T_temporel = 0;
            temps = 0.0f;
            }
    }
}
