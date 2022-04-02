using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem tubeFlow;
    private Material material;
    private Color color;

    public float maxValue = 0;
    public float currentValve = 0;
    public Valve valve;

    private float alpha = 0;
    private bool isStopFlow;

    void Start()
    {
        maxValue = valve.MaxAngle;
        material = tubeFlow.GetComponent<ParticleSystemRenderer>().material;
        color = material.GetColor("_TintColor");
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
        return alpha;
    }

    public void StopFlow()
    {
        //isStopFlow = true;
        ParticleSystem.EmissionModule particleEmission = tubeFlow.emission;
        particleEmission.enabled = false;
    }
    void Update()
    {
        currentValve = Mathf.Clamp(currentValve, 0, maxValue);
        alpha = currentValve / maxValue;
        var emission = Mathf.Lerp(0, 150, alpha);
        var size = Mathf.Lerp(0.2f, 1, alpha);

        ParticleSystem.EmissionModule particleEmission = tubeFlow.emission;
        ParticleSystem.MainModule particleMain =  tubeFlow.main;

        particleEmission.rateOverTime = emission;
        particleMain.startSize = size;
    }
}
