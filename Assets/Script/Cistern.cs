using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cistern : MonoBehaviour
{
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
    private float ratio;

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

    /// <summary>
    /// 0 - 100 persents
    /// </summary>
    public int GetTankFull()
    {
        return GetLeftTubePersent() + GetRightTubePersent();
    }

    //ratio of green liquid to total liquid
    public float GetGreenRatio()
    {
        return (float)Math.Round(ratio, 1);
    }
    //ratio of blue liquid to total liquid
    public float GetBlueRatio()
    {
        if (GetTankFull() == 0)
            return 0;
        return (float)Math.Round(100 - ratio, 1);
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

        if (GetTankFull() >= 100)
            tankIsFull = true;

        ColorBlending();
        CheckRatio();
    }

    private void ColorBlending()
    {
        var alphaBlend = leftPersent / GetTankFull() * 100;
        alphaBlend = Mathf.Clamp(alphaBlend, 0, 100);

        Color blendColor = Color.Lerp(tubeRight.GetColor(), tubeLeft.GetColor(), alphaBlend / 100);
        Color newColor = new Color(blendColor.r, blendColor.g, blendColor.b, 0.8f);
        fluidMeshRenderer.material.color = newColor;
        currentColor = newColor;

    }
    private void CheckRatio()
    {
        if (GetTankFull() == 0)
            return;
        float r = GetRightTubePersent();
        float t = GetTankFull();
        ratio = r / t * 100;
    }
}
