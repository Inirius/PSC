using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class ConductiviteTemps : MonoBehaviour
{
    Vector3 centre = new Vector3(10,0,0);
    float rayon = 0;
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
        rayon = transform.localScale.x;
        dist = GetComponent<Shape>().GetShapeDistance(chaud.transform.position);
        if(dist < rayon){
            temps += Time.deltaTime;
            GetComponent<Shape>().conductivity = 10*(1.0f-Mathf.Exp(-GetComponent<Shape>().coef*temps));
            if (temps >15) {
                var gameObject = new GameObject("Source_secondaire"+count);
                count++;
                gameObject.AddComponent<Shape>();
                gameObject.transform.parent = chaud.transform;
                gameObject.GetComponent<Shape>().shapeType = ShapeType.Cube;
                gameObject.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
                gameObject.transform.position = chaud.transform.position + count/15.0f * (transform.position - chaud.transform.position);
                gameObject.GetComponent<Shape>().operation = Operation.Hide;
                temps = 0.01f;
            }
        }
        else {GetComponent<Shape>().conductivity = 1;}
    }
}
