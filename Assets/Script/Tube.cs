using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem tubeFlow;

    private Material material;
    private Color color;

    private float maxValue = 0;
    private float currentValve = 0;

    private float alphaPressure = 0;

    void Start()
    {
        material = tubeFlow.GetComponent<ParticleSystemRenderer>().material;
        color = material.GetColor("_TintColor");
    }

    public void SetMaxValue(float max)
    {
        maxValue = max;
    }

    public void SetCurrentValue(float current)
    {
        currentValve = current;
    }

    public Color GetColor()
    {
        return color;
    }
    /// <summary>
    /// from 0 to 1
    /// </summary>
    public float GetPressure()
    {
        return alphaPressure;
    }

    public void StopFlow()
    {
        ParticleSystem.EmissionModule particleEmission = tubeFlow.emission;
        particleEmission.enabled = false;
    }
    void Update()
    {
        currentValve = Mathf.Clamp(currentValve, 0, maxValue);
        alphaPressure = currentValve / maxValue;
        var emission = Mathf.Lerp(0, 150, alphaPressure);
        var size = Mathf.Lerp(0.2f, 1, alphaPressure);

        ParticleSystem.EmissionModule particleEmission = tubeFlow.emission;
        ParticleSystem.MainModule particleMain =  tubeFlow.main;

        particleEmission.rateOverTime = emission;
        particleMain.startSize = size;
    }
}
