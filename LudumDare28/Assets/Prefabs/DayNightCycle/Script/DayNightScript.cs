using UnityEngine;
using System;
using System.Collections;

public class DayNightScript : MonoBehaviour
{
    public Color MorningColor;
    public float MorningTime = 0.0f;

    public Color MiddayColor;
    public float MiddayTime = 0.2f;

    public Color AfternoonColor;
    public float AfternoonTime = 0.4f;

    public Color EveningColor;
    public float EveningTime = 0.6f;

    public Color NightColor;
    public float NightTime = 0.8f;

    public float DayLengthInSeconds = 50.0f;
    public float SecondsElapsed { get; private set; }
    
    public float SecondsRemaining 
    { 
        get 
        {
            return DayLengthInSeconds - SecondsElapsed;
        }
    }

    private Texture2D overlayTexture;
    private bool hasStarted = false;

    private int numTimesOfDay = 5;
    private Color[] colors;
    private float[] times;

	void Start() 
    {
        Initialize();
        overlayTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
        overlayTexture.filterMode = FilterMode.Point;

        colors = new Color[numTimesOfDay + 1];
        colors[0] = MorningColor;
        colors[1] = MiddayColor;
        colors[2] = AfternoonColor;
        colors[3] = EveningColor;
        colors[4] = NightColor;
        colors[5] = MorningColor;

        times = new float[numTimesOfDay + 1];
        times[0] = MorningTime;
        times[1] = MiddayTime;
        times[2] = AfternoonTime;
        times[3] = EveningTime;
        times[4] = NightTime;
        times[5] = 1.0f;
	}
	
	void Update()
    {
        if (hasStarted)
        {
            SecondsElapsed += Time.deltaTime;

            if (SecondsElapsed > DayLengthInSeconds)
            {
                SecondsElapsed -= DayLengthInSeconds;
            }
        }

        float timeOfDay = SecondsElapsed / DayLengthInSeconds;
        
        int timeIndex = 0;
        while (timeIndex < numTimesOfDay && timeOfDay > times[timeIndex + 1])
        {
            timeIndex++;
        }

        float timeOfDayFraction = (timeOfDay - times[timeIndex]) / (times[timeIndex + 1] - times[timeIndex]);
        Color currentOverlay = timeOfDayFraction * colors[timeIndex + 1] + (1.0f - timeOfDayFraction) * colors[timeIndex];

        overlayTexture.SetPixel(0, 0, currentOverlay);
        overlayTexture.Apply();
	}

    public void Initialize()
    {
        hasStarted = true;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlayTexture);
        GUI.Label(new Rect(10, 10, 200, 200), SecondsRemaining.ToString() + " / " + DayLengthInSeconds.ToString());
    }
}
