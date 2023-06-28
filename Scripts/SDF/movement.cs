using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
   
    bool show = false;

    void Start()
    {
        
    }
    
    void Update()
    {     
        if(Input.GetKey(KeyCode.Return)){
            if (! show) {
                GetComponent<Shape>().operation = Operation.None;
                show = true;
            }
            else {
                GetComponent<Shape>().operation = Operation.Show;
                show = false;
            }
                
        }              
        
        if(Input.GetKey("o")){
            transform.position = transform.position + new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("l")){
            transform.position = transform.position - new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("k")){
            transform.position = transform.position + new Vector3(0,0,1)*2*Time.deltaTime;
        }

        if(Input.GetKey(";")){
            transform.position = transform.position - new Vector3(0,0,1)*2*Time.deltaTime;
        }

        if(Input.GetKey("i")){
            transform.position = transform.position + new Vector3(0,1,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("p")){
            transform.position = transform.position - new Vector3(0,1,0)*2*Time.deltaTime;
        }
        
    }

}
