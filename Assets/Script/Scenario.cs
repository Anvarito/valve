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
    public GameObject workProcessScreen;
    private IPad ipad;
    public Transform workIPadPos;

    private bool moveIPad = false;
    private bool isInProcess = false;
    public Valve leftValve;
    public Valve rightValve;

    private float elapsedTime;
    private string gameTimer;
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

        if (isInProcess)
        {
            elapsedTime += Time.deltaTime;
            var timePlaying = TimeSpan.FromSeconds(elapsedTime);
            gameTimer = timePlaying.ToString("mm':'ss'.'ff");

            GoalMonitoring();
        }
    }

    private void GoalMonitoring()
    {
        bool bothValveStop = leftValve.GetValvePower() == 0 && rightValve.GetValvePower() == 0;

        if ((cistern.GetTankFull() > 80 && bothValveStop) || cistern.GetTankFull() >= 100)
        {
            isInProcess = false;

            bool goal = cistern.GetBlueRatio() <= 35 && cistern.GetBlueRatio() >= 25;

            ipad.ShowScoreBoard(goal, cistern.GetGreenRatio(), cistern.GetBlueRatio(), gameTimer);
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
        workProcessScreen.SetActive(true);
        moveIPad = true;
        isInProcess = true;
    }
}
