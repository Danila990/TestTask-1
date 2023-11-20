using UnityEngine;

public class ProductMover : MonoBehaviour
{
    [SerializeField]
    private float _productSpeed = 2f;
    [SerializeField]
    private float _lerpModifaer = 0.1f;
    private bool _isDestroy = false;
    private Transform _targetTransform;
    private Transform _productTransform;

    private void Start()
    {
        _productTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        _productTransform.position = Vector3.Lerp(_productTransform.position, _targetTransform.position, Time.deltaTime);
        _productTransform.rotation = _targetTransform.rotation;
        if (Vector3.Distance(_productTransform.position, _targetTransform.position) <= _lerpModifaer)
        {
            _productTransform.position = _targetTransform.position;
            if (_isDestroy)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Init(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    public void Destroy()
    {
        _isDestroy = true;
    }
}
