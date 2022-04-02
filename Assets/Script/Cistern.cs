using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cistern : MonoBehaviour
{
    Vector3 waterColorAccumulator = Vector3.zero;

    private float speedMod = 0.05f;
    private float wateYpos = 0;
    private float minYpos;
    private float maxYpos;

    private float maxWaterLevel = 8500;
    private float currentWaterLevel = 0;
    private bool tankIsFull = false;

    public Transform fluid;
    public MeshRenderer fluidMeshRenderer;

    public Tube tubeLeft;
    public Tube tubeRight;

    private float leftAmount;
    private float rightAmount;

    private float leftPersent;
    private float rightPersent;

    public Color currentColor;
    void Start()
    {
        minYpos = 0.038f;
        maxYpos = 3.33f;
        fluid.position = new Vector3(fluid.position.x, minYpos, fluid.position.z);
    }

    public int GetRightTubePersent()
    {
        return Mathf.RoundToInt(rightPersent);
    }
    public int GetLeftTubePersent()
    {
        return Mathf.RoundToInt(leftPersent);
    }

    void Update()
    {
        if (tankIsFull)
        {
            tubeLeft.StopFlow();
            tubeRight.StopFlow();
            return;
        }
        var pressureSum = (tubeLeft.GetPressure() + tubeRight.GetPressure());

        currentWaterLevel += pressureSum;
        currentWaterLevel = Mathf.Clamp(currentWaterLevel, 0, maxWaterLevel);
        var alpha = currentWaterLevel / maxWaterLevel;
        wateYpos = Mathf.Lerp(minYpos, maxYpos, alpha);
        fluid.position = new Vector3(fluid.position.x, wateYpos, fluid.position.z);

        leftAmount += tubeLeft.GetPressure();
        rightAmount += tubeRight.GetPressure();

        leftPersent = leftAmount / maxWaterLevel * 100;
        rightPersent = rightAmount / maxWaterLevel * 100;

        var currentSumPersent = leftPersent + rightPersent;

        if (currentSumPersent >= 100)
            tankIsFull = true;

        var alphaBlend = leftPersent / currentSumPersent * 100;
        alphaBlend = Mathf.Clamp(alphaBlend, 0, 100);
        
        Color blendColor = Color.Lerp(tubeRight.GetColor(), tubeLeft.GetColor(), alphaBlend / 100);
        Color newColor = new Color(blendColor.r, blendColor.g, blendColor.b, 0.8f);
        fluidMeshRenderer.material.color = newColor;
        currentColor = newColor;
    }

    public static Color CInterp(Color Current, Color Target, float DeltaTime, float InterpSpeed)
    {
        if (InterpSpeed <= 0.0f) return Target;
        float Alpha = Mathf.Clamp(DeltaTime * InterpSpeed, 0.0f, 1.0f);
        return Color.Lerp(Current, Target, Alpha);
    }
}
