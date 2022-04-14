using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GargLight : MonoBehaviour
{
    public Transform JohnLemon;
    public PostProcessVolume Volume;
    private readonly ColorParameter cpBlack = new ColorParameter { value = Color.black }; private readonly ColorParameter cpRed = new ColorParameter { value = Color.red }; Vignette vigLay = null;
    private readonly float minIntensity = 0.4f; private readonly float maxIntensity = 1.0f; private readonly float lightrange = 6.0f;

    void Start()
    {
        Volume.profile.TryGetSettings(out vigLay);
    }

    void Update()
    {
        Vector3 direction = JohnLemon.position - transform.position + Vector3.up;
        float dis = direction.magnitude;
        if (lightrange>dis)
        {
            Ray ray = new Ray(transform.position, direction);
            float starring = Vector3.Dot(direction, transform.forward);
            float looking = Vector3.Dot(-direction, JohnLemon.transform.forward);
            vigLay.enabled.value = true;
            if (starring > 0 && looking > 0 && Physics.Raycast(ray, out RaycastHit raycastHit)
                && raycastHit.collider.transform == JohnLemon)
            { 
                vigLay.color.value = cpRed;
                float disNd = dis / lightrange;
                float lightbright = (1 - disNd) * maxIntensity + disNd * minIntensity; vigLay.intensity.value = lightbright;
            }
            else
            { 
                vigLay.color.value = cpBlack; vigLay.intensity.value = 0.4f;
            }
        }
    }
}
