using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IPad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;
    public GameObject canvas;

    public Cistern cistern;

    public TextMeshPro greenText;
    public TextMeshPro blueText;
    public TextMeshPro totalText;

    public GameObject cameraScreen;
    private bool camShow = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvas.SetActive(true);
            model.SetActive(true);
        }

        var leftPersent = cistern.GetLeftTubePersent();
        var rightPersent = cistern.GetRightTubePersent();
        greenText.text = "blue: " + leftPersent + "%";
        blueText.text = "green: " + rightPersent + "%";
        var sum = leftPersent + rightPersent;
        totalText.text = "Total: " + sum + "%";

    }

    public void ClickHomeButton()
    {
        model.SetActive(false);
        canvas.SetActive(false);
    }

    public void ScreenClick()
    {
        camShow = !camShow;
        cameraScreen.SetActive(camShow);
    }
}
