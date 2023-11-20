using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    private Transform _cameraPointTransform;

    private void Start()
    {
        _cameraPointTransform = GetComponent<Transform>();
        _cameraPointTransform.position = _playerTransform.position;
    }

    private void Update()
    {
        _cameraPointTransform.position = _playerTransform.position;
    }
}
