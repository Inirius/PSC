using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public int number;
    private int chosen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    if(Input.GetKey("0")) {
              chosen = 0;
       }
    if(Input.GetKey("1")) {
              chosen = 1;
       }
    if(Input.GetKey("2")) {
                  chosen = 2;
        }
    if(Input.GetKey("3")) {
                  chosen = 3;
        }
    if(Input.GetKey("4")) {
                  chosen = 4;
        }

    if(chosen == number) {

        if(Input.GetKey("w")){
            transform.position = transform.position + new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("s")){
            transform.position = transform.position - new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("a")){
            transform.position = transform.position + new Vector3(0,0,1)*2*Time.deltaTime;
        }

        if(Input.GetKey("d")){
            transform.position = transform.position - new Vector3(0,0,1)*2*Time.deltaTime;
        }

        //if(Input.GetKey("r")){ transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,1)*Time.deltaTime;}

        //if(Input.GetKey("t")){            transform.eulerAngles = transform.eulerAngles - new Vector3(0,0,1)*Time.deltaTime;        }

        //if(Input.GetKey("f")){            transform.eulerAngles = transform.eulerAngles + new Vector3(0,1,0)*Time.deltaTime;        }

        //if(Input.GetKey("g")){            transform.eulerAngles = transform.eulerAngles - new Vector3(0,1,0)*Time.deltaTime;        }

        //if(Input.GetKey("v")){            transform.eulerAngles = transform.eulerAngles + new Vector3(1,0,0)*Time.deltaTime;        }

        //if(Input.GetKey("b")){            transform.eulerAngles = transform.eulerAngles - new Vector3(1,0,0)*Time.deltaTime;        }
    }
}
}
