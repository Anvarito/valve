using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rotator;
    private Vector3 startDirection;
    public Light hightlight;
    void Start()
    {
        startDirection = rotator.forward;
    }

    bool capchureVector = false;
    Vector2 valveToMouseOrigin = Vector2.zero;
    private Quaternion oldFingerRotation;
    Vector2 mousePosPrev;
    private float angle;
    private float anglePrev;

    public float MaxAngle = 720;
    public float AccumulateAngle;
    public bool more = false;
    // Update is called once per frame
    void Update()
    {
        if (IsAllowRotate())
        {
            Vector2 valvePos = Camera.main.WorldToScreenPoint(rotator.transform.position);
            Vector2 mousePos = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                float a0 = AngleBetweenVector2(valvePos, mousePosPrev);
                float a1 = AngleBetweenVector2(valvePos, mousePos);

                angle += a0 - a1; // for right valve += a1 - a0;

                if (AccumulateAngle != 0 && AccumulateAngle < MaxAngle)
                {
                    rotator.transform.rotation = Quaternion.Euler(0, angle, 0);
                }

                if (angle > anglePrev)
                {
                    AccumulateAngle += angle - anglePrev;
                }
                if (angle < anglePrev)
                {
                    AccumulateAngle -= angle - anglePrev;
                }
                    print(AccumulateAngle);
                AccumulateAngle = Mathf.Clamp(AccumulateAngle, 0, MaxAngle);
            }

            anglePrev = angle;
            mousePosPrev = mousePos;
        }

        hightlight.enabled = IsAllowRotate();
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        //print("valve: " + vec1.y + " mouse: " + vec2.y + " sign: " + sign + " rotator: " + rotator.eulerAngles);
        return Vector2.Angle(transform.right, diference) * sign;
    }

    private bool IsAllowRotate()
    {
        Transform camera = Camera.main.transform;

        Vector3 camToValve = transform.position - camera.position;
        Vector3 camForward = camera.forward;
        var visible = Vector3.Dot(camToValve.normalized, camForward.normalized);

        float distance = Vector3.SqrMagnitude(camToValve);

        bool canRotate = distance < 37.0f && visible > 0.97f;

        return canRotate;
    }
}
