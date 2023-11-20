using System.Collections;
using UnityEngine;

public class FactoryOutputStorage : FactoryStorage
{

    private PlayerStorage _playerStorage;
    private Transform _productTransformInStorage;
    private bool _isCheckCoroutineRun = false;

    public delegate void StorageDelegate(Transform productTransformInStorage);
    public event StorageDelegate StorageReady;

    private void Update()
    {
        if (StorageItemList[0, 0] == null && _isCheckCoroutineRun == false)
        {
            StartCoroutine(CheckStorageFullness());
            _isCheckCoroutineRun = true;
        }
    }

    private IEnumerator SpawnRequest()
    {
        while (StorageItemList[0, 0] == null && _isCheckCoroutineRun == false)
        {
            StartCoroutine(CheckStorageFullness());
            _isCheckCoroutineRun = true;
            yield return new WaitForSeconds(1);
        }
    }
    private IEnumerator CheckStorageFullness()
    {
        for (int i = 0; i < StorageItemList.Rank; i++)
        {
            for (int j = 0; j < StorageItemList.Length / StorageItemList.Rank; j++)
            {
                if (StorageItemList[i, j] == null)
                {
                    StorageReady?.Invoke(StorageSpawnpointList[i, j].transform);
                    yield return new WaitUntil(predicate: () => CurrentProduct != null);
                    StorageItemList[i, j] = CurrentProduct;
                    CurrentProduct = null;
                }
            }
        }
        _isCheckCoroutineRun = false;
    }

    private IEnumerator TransitToPlayerStorage()
    {
        for (int i = 0; i < StorageItemList.Rank; i++)
        {
            for (int j = 0; j < StorageItemList.Length / StorageItemList.Rank; j++)
            {
                if (StorageItemList[i, j] != null)
                {
                    StorageItemList[i, j].GetComponent<ProductMover>().Init(_productTransformInStorage);
                    StorageItemList[i, j].transform.SetParent(_playerStorage.gameObject.transform);
                    _playerStorage.SetCurrentProduct(StorageItemList[i, j]);
                    StorageItemList[i, j] = null;
                    yield break;
                }
            }
        }
        yield return null;
    }

    public void FactoryStorageInit(PlayerStorage playerStorage, Transform productTransformInStorage)
    {
        _playerStorage = playerStorage;
        _productTransformInStorage = productTransformInStorage;
        StartCoroutine(TransitToPlayerStorage());
    }
}

