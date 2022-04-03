using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IPad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;
    public GameObject canvas;
    public GameObject topViewCameraScreen;

    public Cistern cistern;
    public Valve leftValve;
    public Valve rightValve;

    [Header("Work process screen")]
    public GameObject workProcessPanel;
    public TextMeshPro greenText;
    public TextMeshPro greenTextPower;
    public TextMeshPro blueText;
    public TextMeshPro blueTextPower;
    public TextMeshPro totalText;

    public TextMeshPro blueRatio;
    public TextMeshPro greenRatio;

    [Space(1)]
    [Header("Total screen")]
    public GameObject resultPanel;
    public TextMeshPro ScoreMessage;
    public TextMeshPro blueTotal;
    public TextMeshPro greenTotal;

    private bool camShow = true;
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
        var sum = cistern.GetTankFull();
        totalText.text = "Full tank: " + sum + "%";

        var leftPower = leftValve.GetValvePower();
        var rightPower = rightValve.GetValvePower();
        greenTextPower.text = "pw: " + rightPower;
        blueTextPower.text = "pw: " + leftPower;

        blueRatio.text = "Blue ratio: " + cistern.GetBlueRatio();
        greenRatio.text = "Green ratio: " + cistern.GetGreenRatio();
    }
    //Invoke in inspector
    public void ClickHomeButton()
    {
        model.SetActive(false);
        canvas.SetActive(false);
    }
    //Invoke in inspector
    public void ScreenClick()
    {
        camShow = !camShow;
        topViewCameraScreen.SetActive(camShow);
    }

    public void ShowScoreBoard(bool goal, float persentGreen, float persentBlue)
    {
        model.SetActive(true);
        canvas.SetActive(true);
        resultPanel.SetActive(true);
        workProcessPanel.SetActive(false);
        topViewCameraScreen.SetActive(false);

        ScoreMessage.text = goal ? "success!" : "fail..";
        blueTotal.text = "blue: " + persentBlue;
        greenTotal.text = "green: " + persentGreen;
    }
}
