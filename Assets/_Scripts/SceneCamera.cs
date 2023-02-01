using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    [Header("Speed")] public float m_YawRotationalSpeed;
    public float m_PitchRotationalSpeed;
    [Range(1f, 25f)] public float m_ScrollSensitivity = 25f;
    [Header("Pitch")] public float m_MinPitch;
    public float m_MaxPitch;
    public float m_OffsetOnCollision;

    [Header("Distance")] public float m_MaxDistanceToLookAt = 10f;
    public float m_DistanceToLookAt;
    public float m_MinDistanceToLookAt;

    [Header("Raycasts")] public LayerMask m_RaycastLayerMask;

    public Transform targetPoint;

    private bool m_AngleLocked = false;

    private void Update()
    {
        m_DistanceToLookAt = Mathf.Lerp(m_DistanceToLookAt,
            m_DistanceToLookAt - Input.mouseScrollDelta.y,
            m_ScrollSensitivity * Time.deltaTime);
        
        m_DistanceToLookAt = Mathf.Clamp(m_DistanceToLookAt, m_MinDistanceToLookAt, m_MaxDistanceToLookAt);
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        //..
        float l_MouseAxisX = Input.GetAxis("Mouse X");
        float l_MouseAxisY = Input.GetAxis("Mouse Y");

        Vector3 l_Direction = targetPoint.transform.position -
                              this.gameObject.transform.position;
        float l_Distance = l_Direction.magnitude;
        //..
        Vector3 l_DesiredPosition = transform.position;

        if (!m_AngleLocked && (l_MouseAxisX > 0.01f || l_MouseAxisX < -0.01f || l_MouseAxisY > 0.01f ||
                               l_MouseAxisY < -0.01f))
        {
            Vector3 l_EulerAngles = transform.eulerAngles;
            float l_Yaw = (l_EulerAngles.y + 180.0f);
            float l_Pitch = l_EulerAngles.x;

            l_Yaw += m_YawRotationalSpeed * l_MouseAxisX * Time.deltaTime;
            l_Yaw *= Mathf.Deg2Rad;
            if (l_Pitch > 180.0f)
                l_Pitch -= 360.0f;
            l_Pitch += m_PitchRotationalSpeed * (-l_MouseAxisY) * Time.deltaTime;
            l_Pitch = Mathf.Clamp(l_Pitch, m_MinPitch, m_MaxPitch);
            l_Pitch *= Mathf.Deg2Rad;
            l_DesiredPosition = targetPoint.transform.position + new Vector3(
                Mathf.Sin(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance, Mathf.Sin(l_Pitch) * l_Distance,
                Mathf.Cos(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance);
            l_Direction = targetPoint.transform.position - l_DesiredPosition;
        }

        l_Direction /= l_Distance;

        if (l_Distance > m_DistanceToLookAt)
        {
            l_DesiredPosition = targetPoint.transform.position -
                                l_Direction * m_DistanceToLookAt;
            l_Distance = m_DistanceToLookAt;
        }
        else
        {
            float l_YPosition = l_DesiredPosition.y;
            l_DesiredPosition = targetPoint.transform.position -
                                l_Direction * m_DistanceToLookAt;
            l_Distance = m_DistanceToLookAt;

            // l_DesiredPosition.y = l_YPosition;
        }

        RaycastHit l_RaycastHit;
        Ray l_Ray = new Ray(targetPoint.transform.position, -l_Direction);
        if (Physics.Raycast(l_Ray, out l_RaycastHit, l_Distance, m_RaycastLayerMask.value))
            l_DesiredPosition = l_RaycastHit.point + l_Direction * m_OffsetOnCollision;

        transform.forward = l_Direction;
        transform.position = l_DesiredPosition;
    }

    //End of moving characters.
}