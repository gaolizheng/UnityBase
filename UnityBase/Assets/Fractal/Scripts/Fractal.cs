using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{

    public Mesh[] mesh;
    public Material material;
    public int maxDepths;
    public float childScale;
    private int depth;
    public float spawnProbability;
    private static Vector3[] childDirection = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] childRotation = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

    private Material[] materials;

    void Start()
    {
        if (materials == null)
        {
            InitializeMaterial();
        }
        gameObject.AddComponent<MeshFilter>().mesh = mesh[Random.Range(0, mesh.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth];
        if (depth < maxDepths)
        {
            StartCoroutine(CreateChild());
        }
    }

    private IEnumerator CreateChild()
    {
        for (int i = 0; i < childDirection.Length; i++)
        {
            if (Random.value < spawnProbability)
            {
                yield return new WaitForSeconds(1f);
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);   
            }
        }
    }

    private void Initialize(Fractal parent, int childIndex){
        mesh = parent.mesh;
        maxDepths = parent.maxDepths;
        childScale = parent.childScale;
        materials = parent.materials;
        depth = parent.depth + 1;
        spawnProbability = parent.spawnProbability;
        transform.SetParent(parent.transform, false);
        transform.localScale = Vector3.one * parent.childScale;
        transform.localPosition = childDirection[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childRotation[childIndex];
    }

    private void InitializeMaterial()
    {
        materials = new Material[maxDepths + 1];
        for (int i = 0; i < maxDepths; i++)
        {
            float t = i / (maxDepths - 1f);
            t *= t;
            materials[i] = new Material(material);
            materials[i].color = Color.Lerp(Color.white, Color.yellow, t);
        }
    }

    void Update()
    {
        transform.Rotate(0f, 30f * Time.deltaTime, 0f);
    }
}
