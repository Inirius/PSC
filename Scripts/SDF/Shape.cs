using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{

    public enum ShapeType {Sphere,Cube,Torus,Prism,Cylinder,Cone};
    public enum Operation {None, Blend, Cut, Mask, Hide, Show, Forget, Fusion}

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
            return transform.localEulerAngles;
        }
    }

    public float T_temporel;

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

private float PrismDistance(Vector3 eye, Vector3 center, Vector3 h, Vector3 rot)
{
    Vector3 neye = Tourne(rot, eye - center);
    Vector3 q = new Vector3(Mathf.Abs(neye.x), Mathf.Abs(neye.y), Mathf.Abs(neye.z));
    return Mathf.Max(q.z - h.y, Mathf.Max(q.x * Mathf.Cos(h.z) + neye.y * Mathf.Sin(h.z), -neye.y) - h.x * Mathf.Sin(h.z));
}

private float CylinderDistance(Vector3 eye, Vector3 center, Vector2 h, Vector3 rot)
{
    Vector3 neye = Tourne(rot, eye - center);
    float dxz = Mathf.Sqrt(neye.x * neye.x + neye.z * neye.z);
    Vector2 d = new Vector2(dxz, Mathf.Abs(neye.y)) - h;
    return Mathf.Sqrt(Mathf.Max(d.x * d.x + Mathf.Max(d.y, 0f) * Mathf.Max(d.y, 0f), 0f)) + Mathf.Max(Mathf.Min(d.x, 0f), Mathf.Min(d.y, 0f));
}

float ConeDistance(Vector3 eye, Vector3 centre, Vector3 h, Vector3 rot)
{
    Vector2 q = h.z * new Vector2(h.x / h.y, -1.0f);
    Vector3 p = Tourne(rot, eye - centre);
    Vector2 w = new Vector2(Mathf.Sqrt(p.x * p.x + p.z * p.z), p.y);
    Vector2 a = w - q * Mathf.Clamp(Vector2.Dot(w, q) / Vector2.Dot(q, q), 0.0f, 1.0f);
    Vector2 b = w - q * new Vector2(Mathf.Clamp(w.x / q.x, 0.0f, 1.0f), 1.0f);
    float k = Mathf.Sign(q.y);
    float d = Mathf.Min(Vector2.Dot(a, a), Vector2.Dot(b, b));
    float s = Mathf.Max(k * (w.x * q.y - w.y * q.x), k * (w.y - q.y));
    return Mathf.Sqrt(d) * Mathf.Sign(s);
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
        return PrismDistance(eye, Position, Scale, rotation);
    }
    else if (shapeType == ShapeType.Cylinder) {
        return CylinderDistance(eye, Position, new Vector2(Scale.x, Scale.y) , rotation);
    }
    else if (shapeType == ShapeType.Cone) {
        return ConeDistance(eye, Position, Scale, rotation);
    }
    else {
        return 0.0f;
    }
}

}
