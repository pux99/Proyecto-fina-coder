using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamaraShake : MonoBehaviour
{
    public static CamaraShake Instance { get; private set; }
    private CinemachineVirtualCamera cameraShaker;
    private void Awake()
    {
        Instance = this;
        cameraShaker = GetComponent<CinemachineVirtualCamera>();
    }
   public void ShakeCamera(float intencity)
    {
        cameraShaker.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intencity;
    }
}
