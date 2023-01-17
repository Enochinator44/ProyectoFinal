using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PreubaTrack : MonoBehaviour
{
    [Header("Condiciones")]

    public int eleccionTrack;

    [Header("Referencias")]
    public GameObject cam1;
    public GameObject cam2;
    public CameraStyle currentState;
    public CinemachineVirtualCamera cmv;
    public CinemachineTrackedDolly dTrack1;
    public CinemachineTrackedDolly dTrack2;

    
    public enum CameraStyle
    {
        Track1,
        Track2

    }
    private void Update()
    {
        if (eleccionTrack==1)
        {
            //SwitchCameraStyle(CameraStyle.Track1);

            //cmv.GetComponent<CinemachineTrackedDolly>().m_Path = dTrack2;
            
        }

        if (eleccionTrack == 2)
        {
            //SwitchCameraStyle(CameraStyle.Track2);
        }
    }
    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        cam1.SetActive(false);
        cam2.SetActive(false);

        if (newStyle == CameraStyle.Track1)
        {
            cam1.SetActive(true);
        }
        if (newStyle == CameraStyle.Track2)
        {
            cam2.SetActive(true);
        }

        currentState = newStyle;


    }
}

