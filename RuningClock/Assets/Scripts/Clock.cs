using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hours;
    public Transform minutes;
    public Transform seconds;
    public bool Continuous;

    private void Awake() {
        Debug.Log(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
        UpdateTime();
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        DateTime now = DateTime.Now;
        float second = (Continuous ? (now.Second + now.Millisecond / 1000f) : now.Second) / 60 * 360f;
        float minute = (now.Minute + (now.Second / 60f)) * 6f;
        float hour = (now.Hour + (now.Minute / 60f)) / 12f * 360f;
        seconds.localRotation = Quaternion.Euler(0,second , 0);
        minutes.localRotation = Quaternion.Euler(0, minute, 0);
        hours.localRotation = Quaternion.Euler(0, hour, 0);
    }
}
