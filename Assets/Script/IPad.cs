using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;
    public GameObject canvas;
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
    }

    public void ClickHomeButton()
    {
        model.SetActive(false);
        canvas.SetActive(false);
    }
}
