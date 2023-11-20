using UnityEngine;

public abstract class Storage: MonoBehaviour
{
    [SerializeField]
    private GameObject _productPrefab;
    [SerializeField]
    private Transform _storageTransformSpawnpoint;

    protected GameObject[,] StorageItemList;
    protected GameObject[,] StorageSpawnpointList;
    protected float ProductLength;
    protected float ProductWidth;
    protected float ProductHeight;
    protected GameObject CurrentProduct;

    protected virtual void Start()
    {
        StorageItemList = GetStorageList();
        StorageSpawnpointList = GetStorageList();
        GetProductSize();
        GenerateStorageSpawnpointList();
    }

    private void GetProductSize()
    {
        var product = Instantiate(_productPrefab);
        ProductLength = product.transform.localScale.z;
        ProductWidth = product.transform.localScale.x;
        ProductHeight = product.transform.localScale.y;
        Destroy(product);
    }

    private void GenerateStorageSpawnpointList()
    {
        for (int i = 0; i < StorageSpawnpointList.Rank; i++)
        {
            for (int j = 0; j < StorageSpawnpointList.Length / StorageSpawnpointList.Rank; j++)
            {
                if (StorageSpawnpointList[i, j] == null)
                {
                    StorageSpawnpointList[i, j] = Instantiate(new GameObject("ItemSpawnpoint"), 
                        GenerateItemPositionInStorage(i, j, _storageTransformSpawnpoint), Quaternion.identity, transform);
                }
            }
        }
    }

    protected abstract Vector3 GenerateItemPositionInStorage(int i, int j, Transform storageTransformSpawnpoint);

    protected abstract GameObject[,] GetStorageList();

    public void SetCurrentProduct(GameObject currentProduct)
    {
        CurrentProduct = currentProduct;
    }
}
