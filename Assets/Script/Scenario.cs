using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    // Start is called before the first frame update
    public SFPSC_PlayerMovement player;
    public Cistern cistern;
    public GameObject issueMessage;
    public GameObject startButton;
    public GameObject workProcessScreen;
    private IPad ipad;
    public Transform workIPadPos;

    private bool moveIPad = false;
    public Valve leftValve;
    public Valve rightValve;

    public Tube leftTube;
    public Tube rightTube;
    private bool isComplete;

    void Start()
    {
        player.DisableMovement();
        ipad = GetComponent<IPad>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveIPad)
        {
            transform.localPosition = VInterp(transform.localPosition, workIPadPos.localPosition, Time.deltaTime, 10);
            if (transform.localPosition.x <= workIPadPos.localPosition.x)
                moveIPad = false;
        }

        if (!isComplete)
            GoalMonitoring();
    }

    private void GoalMonitoring()
    {
        bool bothValveStop = leftValve.GetValvePower() == 0 && rightValve.GetValvePower() == 0;

        if ((cistern.GetTankFull() > 80 && bothValveStop) || cistern.GetTankFull() >= 100)
        {
            isComplete = true;

            leftTube.StopFlow();
            rightTube.StopFlow();

            bool goal = cistern.GetBlueRatio() <= 35 && cistern.GetBlueRatio() >= 25;

            ipad.ShowScoreBoard(goal, cistern.GetGreenRatio(), cistern.GetBlueRatio());
        }
    }

    public static Vector3 VInterp(Vector3 Current, Vector3 Target, float DeltaTime, float InterpSpeed)
    {
        if (InterpSpeed <= 0.0f) return Target;
        float Alpha = Mathf.Clamp(DeltaTime * InterpSpeed, 0.0f, 1.0f);
        return Target * Alpha + Current * (1.0f - Alpha);
    }

    //Invoke in inspector
    public void Begin()
    {
        player.EnableMovement();
        issueMessage.SetActive(false);
        startButton.SetActive(false);
        workProcessScreen.SetActive(true);
        //transform.localPosition = workIPadPos.localPosition;
        moveIPad = true;
    }
}
