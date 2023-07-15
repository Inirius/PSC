using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    private Text text;
    int smoothed_fps = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int fps = (int)(1/Time.deltaTime);
        float ALPHA = 0.01f;
        smoothed_fps = (int)(ALPHA * fps + (1-ALPHA) * smoothed_fps);
        text.text = $"{smoothed_fps} FPS";
    }
}
