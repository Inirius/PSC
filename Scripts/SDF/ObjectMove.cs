using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private float time;
    private const float AMPLITUDE = 4;
    private const float FREQUENCY = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        time = 0;
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        Vector3 new_pos = transform.position;
        new_pos.z = initialPosition.z + 0.5f * AMPLITUDE * (1 + Mathf.Sin(-0.5f * Mathf.PI + FREQUENCY * time));
        transform.position = new_pos;
    }

    // Update is called once per frame
    void Update()
    {
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

        if(Input.GetKey("q")){
            transform.position = transform.position + new Vector3(0,1,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("e")){
            transform.position = transform.position - new Vector3(0,2,0)*2*Time.deltaTime;
            
        }

        //if(Input.GetKey("r")){ transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,1)*Time.deltaTime;}

        //if(Input.GetKey("t")){            transform.eulerAngles = transform.eulerAngles - new Vector3(0,0,1)*Time.deltaTime;        }

        //if(Input.GetKey("f")){            transform.eulerAngles = transform.eulerAngles + new Vector3(0,1,0)*Time.deltaTime;        }

        //if(Input.GetKey("g")){            transform.eulerAngles = transform.eulerAngles - new Vector3(0,1,0)*Time.deltaTime;        }

        //if(Input.GetKey("v")){            transform.eulerAngles = transform.eulerAngles + new Vector3(1,0,0)*Time.deltaTime;        }

        //if(Input.GetKey("b")){            transform.eulerAngles = transform.eulerAngles - new Vector3(1,0,0)*Time.deltaTime;        }
    
}
}
