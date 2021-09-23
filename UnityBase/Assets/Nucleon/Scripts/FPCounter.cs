using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCounter : MonoBehaviour
{
    public int FPS { get; private set; }

    void Update()
    {
        FPS = (int) (1f / Time.unscaledDeltaTime);
    }
}
