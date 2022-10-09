using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin noise;
    float shakeTimer;

    private void Awake()
    {
        CinemachineVirtualCamera cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        shakeTimer = Mathf.Max(shakeTimer - Time.deltaTime, 0);
        noise.m_AmplitudeGain = shakeTimer * shakeTimer * 50;
    }

    public void Shake(float magnitude)
    {
        shakeTimer = magnitude;
    }
}
