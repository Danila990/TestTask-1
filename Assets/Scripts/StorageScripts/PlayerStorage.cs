using System.Collections;
using UnityEngine;

public class PlayerStorage : Storage
{
    [SerializeField]
    private int _storageWidth;
    [SerializeField]
    private int _storageHeight;
    [SerializeField]
    private float _transitToPlayerStorageDeley;

    private IEnumerator _checkStorageFullnessCoroutine;
    private FactoryInputStorage _factoryInputStorage;
    private Transform _pproductTransformInStorage;
    private string _inputProductTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FactoryOutputStorage>(out FactoryOutputStorage factoryStorage))
        {
            _checkStorageFullnessCoroutine = CheckStorageFullness(factoryStorage);
            StartCoroutine(_checkStorageFullnessCoroutine);
        }
        if(other.TryGetComponent<FactoryInputStorage>(out FactoryInputStorage factoryInputStorage))
        {
            factoryInputStorage.StartTransit(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FactoryOutputStorage>(out FactoryOutputStorage factoryStorage))
        {
            StopCoroutine(_checkStorageFullnessCoroutine);
        }
        if (other.TryGetComponent<FactoryInputStorage>(out FactoryInputStorage factoryInputStorage))
        {
            factoryInputStorage.StopTransit();
        }
    }

    private IEnumerator CheckStorageFullness(FactoryOutputStorage factoryStorage)
    {
        for (int i = 0; i < _storageWidth; i++)
        {
            for (int j = 0; j < _storageHeight; j++)
            {
                if (StorageItemList[i, j] == null)
                {
                    factoryStorage.FactoryStorageInit(this, StorageSpawnpointList[i, j].transform);
                    yield return new WaitUntil(predicate: () => CurrentProduct != null);
                    StorageItemList[i, j] = CurrentProduct;
                    CurrentProduct = null;
                    yield return new WaitForSeconds(_transitToPlayerStorageDeley);
                }
            }
        }

    }

    private IEnumerator TransitToFactoryStorage()
    {
        for (int i = StorageItemList.Rank - 1; i >= 0; i--)
        {
            for (int j = StorageItemList.Length / StorageItemList.Rank - 1; j >= 0; j--)
            {
                if (StorageItemList[i, j] != null && StorageItemList[i, j].tag == _inputProductTag)
                {
                    StorageItemList[i, j].GetComponent<ProductMover>().Init(_pproductTransformInStorage);
                    StorageItemList[i, j].transform.SetParent(null);
                    _factoryInputStorage.SetCurrentProduct(StorageItemList[i, j]);
                    StorageItemList[i, j] = null;
                    yield break;
                }
            }
        }
        yield return null;
    }

    public void PlayerStorageInit(FactoryInputStorage factoryInputStorage, 
        Transform productTransformInStorage, string inputProductTag)
    {
        _inputProductTag = inputProductTag;
        _factoryInputStorage = factoryInputStorage;
        _pproductTransformInStorage = productTransformInStorage;
        StartCoroutine(TransitToFactoryStorage());
    }

    protected override Vector3 GenerateItemPositionInStorage(int i, int j, Transform storageTransformSpawnpoint)
    {
        return new Vector3(
                storageTransformSpawnpoint.position.x,
                storageTransformSpawnpoint.position.y + ProductHeight * j,
                storageTransformSpawnpoint.position.z + ProductLength * -i);
    }

    protected override GameObject[,] GetStorageList()
    {
        return new GameObject[_storageWidth, _storageHeight];
    }
}

