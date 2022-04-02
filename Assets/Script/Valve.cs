using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public Transform rotator;
    public Light hightlight;
    public Tube tube;

    Vector2 mousePosPrev;
    private float angle;

    public float MaxAngle = 720;

    void Start()
    {
        tube.SetMaxValue(MaxAngle);
    }

    void Update()
    {
        if (IsInWorkArea())
        {
            Vector2 valvePos = Camera.main.WorldToScreenPoint(rotator.transform.position);
            Vector2 mousePos = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                float a0 = AngleBetweenVector2(valvePos, mousePosPrev);
                float a1 = AngleBetweenVector2(valvePos, mousePos);

                angle += a1 - a0; // for right valve += a1 - a0;
                angle = Mathf.Clamp(angle, 0, MaxAngle);
                print(angle);
                rotator.rotation = Quaternion.Euler(new Vector3(rotator.rotation.z, angle, rotator.rotation.z));
                tube.SetCurrentValue(angle);
            }

            mousePosPrev = mousePos;
        }

        hightlight.enabled = IsInWorkArea();
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        return Vector2.Angle(transform.right, diference);
    }

    private bool IsInWorkArea()
    {
        Transform camera = Camera.main.transform;

        Vector3 camToValve = rotator.position - camera.position;
        Vector3 camForward = camera.forward;
        var visible = Vector3.Dot(camToValve.normalized, camForward.normalized);

        float distance = Vector3.SqrMagnitude(camToValve);
        bool canRotate = distance < 6.0f && visible > 0.75f;

        return canRotate;
    }
}
