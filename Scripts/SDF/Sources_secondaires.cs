using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class Sources_secondaires : MonoBehaviour
{
    float temps = 0.0f; // temps écoulé depuis la sortie de la source
    public float dist = 0.0f; // distance à la source du centre
    float t_h = 0.0f;
    GameObject chaud;
    GameObject barre;
    bool entree = false; // true si la barre est dans la source
    float epsilon = 0.05f; // température minimale

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
        dist = chaud.GetComponent<Shape>().GetShapeDistance(transform.position);
        entree = (barre.GetComponent<ConductiviteTemps>().dist < 1f);

        if (entree) {
            if(dist > 0){
                if (temps < - Mathf.Log(epsilon) / GetComponent<Shape>().coef) {
                    // l'objet est dans chaud, la source est en dehors, la température est élevée
                    temps += Time.deltaTime;
                    GetComponent<Shape>().T_temporel = t_h * Mathf.Exp(-temps * GetComponent<Shape>().coef);
                }
                else {
                    // l'objet est dans chaud, la source est en dehors, la température est minimale
                    temps = 0.0f;
                    GetComponent<Shape>().T_temporel = t_h;
                    GetComponent<Shape>().operation = Operation.Forget;
                }
                
            }
            else {
                // l'objet et la source sont dans chaud
                    temps = 0.0f;
                    GetComponent<Shape>().T_temporel = t_h;
                    GetComponent<Shape>().operation = Operation.Hide;
                }
        }
        else {
            // l'objet et la source sont en dehors de chaud
            temps += Time.deltaTime;
            if (GetComponent<Shape>().T_temporel > epsilon) {
                GetComponent<Shape>().T_temporel = GetComponent<Shape>().T_temporel * Mathf.Exp(-temps * GetComponent<Shape>().coef);
        }
            else {
                GetComponent<Shape>().T_temporel = 0.0f;
                GetComponent<Shape>().operation = Operation.Forget;
            }
        }

        
    }
}
