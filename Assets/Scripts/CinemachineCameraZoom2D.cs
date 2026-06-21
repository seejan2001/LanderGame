using UnityEngine;
using Unity.Cinemachine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;
    public static CinemachineCameraZoom2D Instance {get; private set;}
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float targetOrthographicSize = 10f;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float zoomSize = 2f;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSize);
    }

    public void setTargetOrthographicSize(float targetOrthographicSize)
    {
        this.targetOrthographicSize = targetOrthographicSize;
    }
    public void setNormalOrthographicSize()
    {
        setTargetOrthographicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
