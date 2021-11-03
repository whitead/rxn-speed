using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour
{

    private float waitTime = -3;
    private float elapsed = 0;
    private float totalElapsed = 0;
    bool waiting = false;
    bool measuring = false;
    private int attempt = 0;

    public Text output;

    // Start is called before the first frame update
    void Start()
    {

    }

    private bool touchStart()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool touchStop()
    {
        return Input.GetMouseButtonUp(0);
    }

    // Update is called once per frame
    void Update()
    {

        elapsed += Time.deltaTime;

        if (touchStart())
        {
            BeginWait();
        }

        if (waiting)
        {
            if (elapsed >= waitTime)
            {
                BeginMeasure();
            }
            else if (touchStop())
            {
                log("Failed");
                ResetMeasure();
            }

        }

        if (measuring)
        {
            if (touchStop())
            {
                log((elapsed * 100).ToString("F3") + "ms");
                ResetMeasure();
            }
            else if (elapsed > 2)
            {
                log("Failed");
                ResetMeasure();
            }
        }
    }

    void log(string m)
    {
        var minutes = Time.realtimeSinceStartup / 60;
        output.text += attempt.ToString("F3") + "@" + minutes.ToString("F3") + "min | " + m + "\n";
    }

    void BeginWait()
    {
        waiting = true;
        elapsed = 0;
        waitTime = Random.Range(0.75f, 2);
        ChangeColor(Color.green);
    }

    void BeginMeasure()
    {
        ChangeColor(Color.red);
        measuring = true;
        waiting = false;
        elapsed = 0;
        waitTime = -1;
        attempt++;
    }

    void ResetMeasure()
    {
        measuring = false;
        waiting = false;
        elapsed = 0;
        ChangeColor(Color.white);
    }


    void ChangeColor(Color c)
    {
        var cubeRenderer = gameObject.GetComponent<SpriteRenderer>();

        cubeRenderer.color = c;
    }
}
