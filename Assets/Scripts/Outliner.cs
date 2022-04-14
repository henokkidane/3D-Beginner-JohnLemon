using UnityEngine;
using System.Collections;

public class Outliner : MonoBehaviour
{

    public Color mC = new Color(1f, 1f, 1f, 0.5f); public Color oC = new Color(1f, 1f, 0f, 1f);

    public void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>(); Material[] materials = meshRenderer.materials;
        int numOfMat= materials.Length;
        for (int x = 0; x < numOfMat; x++)
        {
            materials[x].shader = Shader.Find("Unlit/Transparent"); materials[x].SetColor("_color", mC);
        }

        GameObject ghostOutline = new GameObject();
        ghostOutline.transform.position = transform.position; ghostOutline.transform.rotation = transform.rotation;
        ghostOutline.AddComponent<MeshFilter>(); ghostOutline.AddComponent<MeshRenderer>();
        Mesh mesh;
        mesh = (Mesh)Instantiate(GetComponent<MeshFilter>().mesh); ghostOutline.GetComponent<MeshFilter>().mesh = mesh;
        ghostOutline.transform.parent = this.transform; materials = new Material[numOfMat];
        for (int x = 0; x < numOfMat; x++)
        {
            materials[x] = new Material(Shader.Find("Unlit/Transparent")); materials[x].SetColor("_oC", oC);
        }
        ghostOutline.GetComponent<MeshRenderer>().materials = materials;

    }

}