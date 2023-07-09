using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shape;

public class MovementOfCamera : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distanceFromTarget = 3.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    [SerializeField]
    private float velocityOfCamera =10;

    float pi = 3.1415926535F;

    bool isMoving = false;

    void Start()
    {
          
    }

    void Update()
    {
        var epee = GameObject.Find("Four");
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = _target.position - transform.forward * _distanceFromTarget;
        
        float r2 = _rotationY*pi/180;
        float r1 = _rotationX*pi/180;
        float cos_a = Mathf.Cos(r1);
        float sin_a = Mathf.Sin(r1);
        float cos_b = Mathf.Cos(r2);
        float sin_b = Mathf.Sin(r2);

        // Move sword
        if (isMoving) {epee.transform.position = transform.position + new Vector3(Mathf.Sin(r2),-Mathf.Sin(r1)-0.1f,Mathf.Cos(r2)) *2;
        //epee.transform.eulerAngles = new Vector3 (-Mathf.Asin(sin_a * cos_b),-Mathf.Atan2(sin_a, cos_a),-Mathf.Atan2(cos_b, sin_b));
        if (r1>0) {epee.transform.eulerAngles = new Vector3 (-3*pi/5-r1,0,0);}
        else {epee.transform.eulerAngles = new Vector3 (-r1,0,0);}
        //epee.transform.eulerAngles = new Vector3 (-r1,0,0);
        }

        if(Input.GetKey("mouse 1")){
            isMoving = !isMoving;
        }

        if(Input.GetKey("up")){
            transform.position = transform.position + new Vector3(Mathf.Sin(r2),0,Mathf.Cos(r2))*velocityOfCamera*5*Time.deltaTime;
        }
        if(Input.GetKey("down")){
            transform.position = transform.position - new Vector3(Mathf.Sin(r2),0,Mathf.Cos(r2))*velocityOfCamera*5*Time.deltaTime;
        }
        if(Input.GetKey("right")){
            transform.position = transform.position - new Vector3(-Mathf.Cos(r2),0,Mathf.Sin(r2))*velocityOfCamera*5*Time.deltaTime;
        }
        if(Input.GetKey("left")){
            transform.position = transform.position + new Vector3(-Mathf.Cos(r2),0,Mathf.Sin(r2))*velocityOfCamera*5*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Space)){
             transform.position = transform.position + new Vector3(0,1,0)*velocityOfCamera*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            transform.position = transform.position - new Vector3(0,1,0)*velocityOfCamera*Time.deltaTime;
        }
        //if(Input.GetKey("mouse 0")){
          //  var gameObject = new GameObject("Bouboule"+count);
            //count++;
            //gameObject.AddComponent<Shape>();
           // gameObject.transform.parent = GameObject.Find("Bac").transform;
            //gameObject.GetComponent<Shape>().shapeType = ShapeType.Cube;
            //gameObject.transform.position = transform.position + new Vector3(Mathf.Sin(r2),-Mathf.Sin(r1)-0.125f,Mathf.Cos(r2)) *2;
            //gameObject.GetComponent<Shape>().operation = Operation.Cut;
           // gameObject.transform.localEulerAngles = new Vector3(epee.transform.localEulerAngles.x,0f,0f);
            //gameObject.transform.localScale = new Vector3(0.3f,0.05f,0.4f);
            //gameObject.GetComponent<Shape>().colour = Color.yellow;
            
             //     }
    }
}
