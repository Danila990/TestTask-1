using UnityEngine;

public abstract class FactoryStorage : Storage
{
    [SerializeField]
    private int _storageLength;
    [SerializeField]
    private int _storageWidth;

    protected override Vector3 GenerateItemPositionInStorage(int i, int j, Transform storageTransformSpawnpoint)
    {
        return new Vector3(
                storageTransformSpawnpoint.position.x + ProductWidth * i,
                storageTransformSpawnpoint.position.y,
                storageTransformSpawnpoint.position.z + ProductLength * j);
    }

    protected override GameObject[,] GetStorageList()
    {
        return new GameObject[_storageWidth, _storageLength];
    }
}
