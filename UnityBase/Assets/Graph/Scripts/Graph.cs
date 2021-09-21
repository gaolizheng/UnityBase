using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    const float PI = Mathf.PI;
    public Transform prefab;
    [Range(10, 100)]
    public int resolution = 10;
    Transform[] points;
    public GraphFunctionName func;
    GraphFunction[] functions = { 
        SinFunction, 
        Sin2DFunction, 
        MultiSinFunction, 
        MultiSin2DFunction, 
        Ripple, 
        Cylinder,
        Sphere
    };
    private void Awake() {
        points = new Transform[resolution * resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        for (int i = 0; i < points.Length; i++)
        {
            Transform p = Instantiate(prefab);
            p.localScale = scale;
            p.SetParent(transform, false);
            points[i] = p;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        float t = Time.time;
        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = functions[(int)func](u, v, t);
            }
        }
    }

    static Vector3 SinFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 MultiSinFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        float y = Mathf.Sin(PI * (x + t));
        y += Mathf.Sin(2f* PI * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        p.y = y;
        p.z = z;
        return p;
    }

    static Vector3 Sin2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + z+ t));
        p.z = z;
        return p;
    }

    static Vector3 MultiSin2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        float y = 4f * Mathf.Sin(PI * (x + z + t * 0.5f));
        y += Mathf.Sin(PI * (x + t));
        y += Mathf.Sin(2f* PI * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        p.y = y;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        float d = Mathf.Sqrt(x * x + z * z);
        float y = Mathf.Sin(PI * (4f * d - t));
        y /= 1f + 10f * d;
        p.y = y;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u,float v, float t)
    {
        float r = 0.8f + Mathf.Sin(PI * (6f *u + 2f * v + t)) * 0.2f;
        Vector3 p;
        p.x = r * Mathf.Sin(PI * u);
        p.y = v;
        p.z = r* Mathf.Cos(PI * u);
        return p;
    }

    static Vector3 Sphere(float u,float v, float t)
    {
        float r = 0.8f + Mathf.Sin(PI * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(PI * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(PI * 0.5f * v);
        Vector3 p;
        p.x = s * Mathf.Sin(PI * u);
        p.y = r * Mathf.Sin(PI * 0.5f * v);
        p.z = s * Mathf.Cos(PI * u);
        return p;
    }
}
