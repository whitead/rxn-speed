using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour
{

    private float waitTime = -3;
    private List<float> values = new List<float>();
    private float elapsed = 0;
    private float totalElapsed = 0;
    bool waiting = false;
    bool measuring = false;
    private int attempt = 0;
    private Color waitColor = new Color32(0x1b, 0x9e, 0x77, 0xff);
    private Color measureColor = new Color32(0xd9, 0x5f, 0x02, 0xff);


    public Text output;
    public Text summary;

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
            summary.text = (elapsed * 100).ToString("F3") + " ms";
            if (touchStop())
            {
                log((elapsed * 100).ToString("F3") + "ms");
                values.Add(elapsed * 100);
                ResetMeasure();
            }
            else if (elapsed > 2)
            {
                log("Failed");
                ResetMeasure();
            }
        }
        else
        {
            if (values.Count > 0)
            {
                // get median
                values.Sort();
                summary.text = (values[values.Count / 2]).ToString("F3") + " ms";
            }
        }
    }

    void log(string m)
    {
        var minutes = Time.realtimeSinceStartup / 60;
        output.text += attempt.ToString("D3") + "@" + minutes.ToString("F3") + "min | " + m + "\n";
    }

    void BeginWait()
    {
        waiting = true;
        elapsed = 0;
        waitTime = Random.Range(0.75f, 2);
        ChangeColor(waitColor);
    }

    void BeginMeasure()
    {
        ChangeColor(measureColor);
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
