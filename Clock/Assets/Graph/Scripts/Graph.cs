using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform prefab;
    [Range(10, 100)]
    public int resolution = 10;
    Transform[] points;
    private void Awake() {
        points = new Transform[resolution];
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 pos = Vector3.zero;
        int i = 0;
        while (i < resolution)
        {
            Transform p = Instantiate(prefab);
            points[i] = p;
            pos.x = (i + 0.5f) * step - 1f;
            p.localPosition = pos;
            p.localScale = scale;
            p.SetParent(transform, false);
            i++;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 pos = point.localPosition;
            pos.y = Mathf.Sin(Mathf.PI * (pos.x + Time.time));
            point.localPosition = pos;
        }
    }
}
