using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class ConductiviteTemps : MonoBehaviour
{
    Vector3 centre = new Vector3(10,0,0);
    float rayon = 0.0f;
    float temps = 0.0f;
    public float dist = 0.0f;
    int nbrchild;
    GameObject chaud;
    // Start is called before the first frame update
    void Start()
    {
        chaud = GameObject.Find("Corps chaud");
        nbrchild = transform.childCount;
        rayon = 1f;
    }

    // Update is called once per frame
    void Update()
    {   
        dist = GetComponent<Shape>().GetShapeDistance(chaud.transform.position);

        if(dist < rayon){
            temps += Time.deltaTime;
            GetComponent<Shape>().T_temporel = temps * GetComponent<Shape>().coef;
            for (int i = 0; i < nbrchild-2; i++)
            {
                transform.GetChild(i).GetComponent<Shape>().T_temporel = temps * transform.GetChild(i).GetComponent<Shape>().coef;
            }
            
        }
        else {
            temps = 0.0f;
            }
    }
}
