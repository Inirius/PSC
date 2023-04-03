using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("z")){
            transform.position = transform.position + new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("s")){
            transform.position = transform.position - new Vector3(1,0,0)*2*Time.deltaTime;
        }

        if(Input.GetKey("q")){
            transform.position = transform.position + new Vector3(0,0,1)*2*Time.deltaTime;
        }

        if(Input.GetKey("d")){
            transform.position = transform.position - new Vector3(0,0,1)*2*Time.deltaTime;
        }

        if(Input.GetKey("r")){
            transform.eulerAngles += new Vector3(0,0,1) * 2 * Time.deltaTime;
        }
    }
}
