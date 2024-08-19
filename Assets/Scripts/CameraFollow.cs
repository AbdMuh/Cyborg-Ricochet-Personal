using UnityEngine;
using Cinemachine;


[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraX : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public double m_XPosition = 0.02;
    public double m_ZPosition = -14.5;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = (float) m_XPosition;
            pos.z = (float) m_ZPosition;
            state.RawPosition = pos;
        }
    }
}