using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	public Transform m_LookAt;
	public float m_YawRotationalSpeed;
	public float m_PitchRotationalSpeed;
	public float m_MinPitch=-45.0f;
	public float m_MaxPitch=75.0f;
	public KeyCode m_DebugLockAngleKeyCode=KeyCode.I;
	public KeyCode m_DebugLockKeyCode=KeyCode.O;
	bool m_AngleLocked=false;
	bool m_CursorLocked=true;
	private float _idleTime = 5;
	private float _currIdleTime;
	[SerializeField] private float m_MinDistanceToLookAt;
	[SerializeField] private float m_MaxDistanceToLookAt;
	[SerializeField] private LayerMask _layers;

	void Start()
	{
		_currIdleTime = _idleTime;
		Cursor.lockState=CursorLockMode.Locked;
		m_CursorLocked=true;
	}
	void OnApplicationFocus()
	{
		if(m_CursorLocked)
			Cursor.lockState=CursorLockMode.Locked;
	}
	void LateUpdate()
	{
		float l_MouseAxisX;
		float l_MouseAxisY;
		if (Gamepad.current == null)
        {
			l_MouseAxisX = Mouse.current.delta.x.ReadValue();
			l_MouseAxisY = Mouse.current.delta.y.ReadValue();
		} else
        {
			l_MouseAxisX = Gamepad.current.rightStick.x.ReadValue();
			l_MouseAxisY = -Gamepad.current.rightStick.y.ReadValue();
		}
        


        Vector3 l_Direction = m_LookAt.position - transform.position;
        float l_Distance = l_Direction.magnitude;

        Vector3 l_DesiredPosition = transform.position;

		if((!m_AngleLocked && (l_MouseAxisX>0.01f || l_MouseAxisX<-0.01f || l_MouseAxisY>0.01f || l_MouseAxisY<-0.01f)) || _currIdleTime <= 0)
		{
			Vector3 l_EulerAngles=transform.eulerAngles;
			float l_Yaw=(l_EulerAngles.y+180.0f);
			float l_Pitch=l_EulerAngles.x;

			//TODO: Update Yaw and Pitch
			l_Yaw += m_YawRotationalSpeed * l_MouseAxisX;
			if (l_Pitch > 180.0f)
				l_Pitch -= 360.0f;
			l_Pitch += m_PitchRotationalSpeed * (-l_MouseAxisY);
			l_Pitch = Mathf.Clamp(l_Pitch,m_MinPitch,m_MaxPitch);
			//TODO: Update DesiredPosition
			if (_currIdleTime <= 0)
			{
				float angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, m_LookAt.forward, Vector3.up);
				if (l_Yaw >= 300)
				{
					l_Yaw += angle;
				} else
                {
					l_Yaw += angle +360;
				}
			}
			_currIdleTime = _idleTime;
			l_Yaw *= Mathf.Deg2Rad;
			l_Pitch *= Mathf.Deg2Rad;
			l_DesiredPosition = m_LookAt.position
				+ new Vector3( Mathf.Sin(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance,
							   Mathf.Sin(l_Pitch) * l_Distance,
							   Mathf.Cos(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance );
			l_Direction = m_LookAt.position - l_DesiredPosition;
		} else
        {
			_currIdleTime -= Time.deltaTime;
        }
		l_Direction/=l_Distance;

		//TODO: Clamp between minDistance and maxDistance. Update desiredPosition.
		l_Distance = Mathf.Clamp(l_Distance, m_MinDistanceToLookAt, m_MaxDistanceToLookAt);
		l_DesiredPosition = m_LookAt.position - l_Direction * l_Distance;
		//TODO: Bring camera closer if colliding with any object.
		Ray r = new Ray(m_LookAt.position, -l_Direction);
		if (Physics.Raycast(r, out RaycastHit info,l_Distance,_layers))
        {
			l_DesiredPosition = info.point + l_Direction * 0.8f ;
        }
		transform.forward=l_Direction;
		transform.position=l_DesiredPosition;
	} 
}
