using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    private float waitTime = -3;
    private float elapsed = 0;
    bool waiting = false;
    bool measuring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        elapsed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            waiting = true;
            elapsed = 0;
            waitTime = Random.Range(1, 2);
            Debug.Log(waitTime);
        }

        if (waitTime > 0 && elapsed >= waitTime)
        {
            ChangeColor(Color.red);
            measuring = true;
            waiting = false;
            elapsed = 0;
            waitTime = -1;
        }

        if(measuring && Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Reaction Speed = " + elapsed);
            measuring = false;
            elapsed = 0;
            ChangeColor(Color.white);
        }
    }

    void ChangeColor(Color c)
    {
        var cubeRenderer = gameObject.GetComponent<SpriteRenderer>();
       
        cubeRenderer.color = c;
    }
}
