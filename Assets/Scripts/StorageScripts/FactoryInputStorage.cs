using System.Collections;
using UnityEngine;

public abstract class FactoryInputStorage : FactoryStorage
{
    [SerializeField]
    private float _transitToFactoryStorageDeley;
    [SerializeField]
    private string _inputProductTag;

    private IEnumerator _checkStorageFullnessCoroutine;

    private IEnumerator CheckStorageFullness(PlayerStorage playerStorage, float transitToFactoryStorageDeley)
    {
        for (int i = 0; i < StorageItemList.Rank; i++)
        {
            for (int j = 0; j < StorageItemList.Length / StorageItemList.Rank; j++)
            {
                if (StorageItemList[i, j] == null)
                {
                    playerStorage.PlayerStorageInit(this, StorageSpawnpointList[i, j].transform, _inputProductTag);
                    yield return new WaitUntil(predicate: () => CurrentProduct != null);
                    StorageItemList[i, j] = CurrentProduct;
                    CurrentProduct = null;
                    yield return new WaitForSeconds(transitToFactoryStorageDeley);
                }
            }
        }
    }

    public IEnumerator TransitToFactory(Transform ingridientTransform, NeedInputFactory needInputFactory)
    {
        for (int i = 0; i < StorageItemList.Rank; i++)
        {
            for (int j = 0; j < StorageItemList.Length / StorageItemList.Rank; j++)
            {
                if (StorageItemList[i, j] != null)
                {
                    StorageItemList[i, j].GetComponent<ProductMover>().Init(ingridientTransform);
                    SetIngredient(needInputFactory, StorageItemList[i, j]);
                    StorageItemList[i, j] = null;
                    yield break;
                }
            }
        }
        yield return null;
    }

    protected abstract void SetIngredient(NeedInputFactory needInputFactory, GameObject ingridient);

    public void StartTransit(PlayerStorage playerStorage)
    {
        _checkStorageFullnessCoroutine = CheckStorageFullness(playerStorage, _transitToFactoryStorageDeley);
        StartCoroutine(_checkStorageFullnessCoroutine);
    }

    public void StopTransit()
    {
        StopCoroutine(_checkStorageFullnessCoroutine);
    }
}
