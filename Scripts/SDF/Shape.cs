using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{

    public enum ShapeType {Sphere,Cube,Torus,Prism,Cylinder};
    public enum Operation {None, Blend, Cut,Mask}

    public ShapeType shapeType;
    public Operation operation;
    public Color colour = Color.white;
    [Range(0,1)]
    public float blendStrength;
    [HideInInspector]
    public int numChildren;

    //
    //

    public Vector3 Position {
        get {
            return transform.position;
        }
    }

    public Vector3 Scale {
        get {
            Vector3 parentScale = Vector3.one;
            if (transform.parent != null && transform.parent.GetComponent<Shape>() != null) {
                parentScale = transform.parent.GetComponent<Shape>().Scale;
            }
            return Vector3.Scale(transform.localScale, parentScale);
        }
    }
    public Vector3 rotation {
        get {
            return transform.eulerAngles;
        }
    }

    public float conductivity;

    public float coef;

    private Vector3 Tourne(Vector3 rot,Vector3 coor) {
    return (new Vector3(coor.x*(Mathf.Cos(rot.y)*Mathf.Cos(rot.z)-Mathf.Sin(rot.y)*Mathf.Cos(rot.x)*Mathf.Sin(rot.z))-coor.y*(Mathf.Cos(rot.y)*Mathf.Sin(rot.z)+Mathf.Sin(rot.y)*Mathf.Cos(rot.z)*Mathf.Cos(rot.x))+coor.z*Mathf.Sin(rot.y)*Mathf.Sin(rot.x),
    coor.x*(Mathf.Sin(rot.y)*Mathf.Cos(rot.z)+Mathf.Cos(rot.y)*Mathf.Cos(rot.x)*Mathf.Sin(rot.z))+coor.y*(-Mathf.Sin(rot.y)*Mathf.Sin(rot.z)+Mathf.Cos(rot.y)*Mathf.Cos(rot.z)*Mathf.Cos(rot.x))-coor.z*Mathf.Cos(rot.y)*Mathf.Sin(rot.x),
    coor.x*Mathf.Sin(rot.x)*Mathf.Sin(rot.z)+coor.y*Mathf.Sin(rot.x)*Mathf.Cos(rot.z)+coor.z*Mathf.Cos(rot.x)));
}
    private float SphereDistance(Vector3 eye, Vector3 centre, float radius) {
    return Vector3.Distance(eye, centre) - radius;
}

 ///........................
    private float CubeDistance(Vector3 eye, Vector3 center, Vector3 size, Vector3 rot)
{
    Vector3 o = Tourne(rot, eye - center);
    o = new Vector3(Mathf.Abs(o.x), Mathf.Abs(o.y), Mathf.Abs(o.z)) - size;
    float ud = Mathf.Sqrt(Mathf.Max(o.x * o.x, 0) + Mathf.Max(o.y * o.y, 0) + Mathf.Max(o.z * o.z, 0));
    float n = Mathf.Max(Mathf.Max(Mathf.Min(o.x, 0), Mathf.Min(o.y, 0)), Mathf.Min(o.z, 0));
    return ud + n;
}


    private float TorusDistance(Vector3 eye, Vector3 center, float r1, float r2, Vector3 rot)
{
    Vector3 coor = Tourne(rot, eye - center);
    Vector2 q = new Vector2(Vector2.Distance(new Vector2(coor.x, coor.z), Vector2.zero) - r1, coor.y);
    return q.magnitude - r2;
}

private float PrismDistance(Vector3 eye, Vector3 center, Vector2 h, Vector3 rot)
{
    Vector3 neye = Tourne(rot, eye - center);
    Vector3 q = new Vector3(Mathf.Abs(neye.x), Mathf.Abs(neye.y), Mathf.Abs(neye.z));
    return Mathf.Max(q.z - h.y, Mathf.Max(q.x * 0.866025f + neye.y * 0.5f, -neye.y) - h.x * 0.5f);
}

private float CylinderDistance(Vector3 eye, Vector3 center, Vector2 h, Vector3 rot)
{
    Vector3 neye = Tourne(rot, eye - center);
    float dxz = Mathf.Sqrt(neye.x * neye.x + neye.z * neye.z);
    Vector2 d = new Vector2(dxz, Mathf.Abs(neye.y)) - h;
    return Mathf.Sqrt(Mathf.Max(d.x * d.x + Mathf.Max(d.y, 0f) * Mathf.Max(d.y, 0f), 0f)) + Mathf.Max(Mathf.Min(d.x, 0f), Mathf.Min(d.y, 0f));
}
public float GetShapeDistance( Vector3 eye) {
   
    if (shapeType == ShapeType.Sphere) {
        return SphereDistance(eye, Position, Scale.x);
    }
    else if (shapeType == ShapeType.Cube) {
        return CubeDistance(eye, Position, Scale, rotation);
    }
    else if (shapeType == ShapeType.Torus) {
        return TorusDistance(eye, Position, Scale.x, Scale.y, rotation);
    }
    else if (shapeType == ShapeType.Prism) {
        return PrismDistance(eye, Position, new Vector2(Scale.x, Scale.y), rotation);
    }
    else if (shapeType == ShapeType.Cylinder) {
        return CylinderDistance(eye, Position, new Vector2(Scale.x, Scale.y) , rotation);
    }
    else {
        return 0.0f;
    }
}

}
