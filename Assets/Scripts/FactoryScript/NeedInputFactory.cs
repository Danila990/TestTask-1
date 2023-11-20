using System.Collections;
using UnityEngine;

public abstract class NeedInputFactory : Factory
{
    protected GameObject Ingredient1;
    protected GameObject Ingredient2;

    private void Update()
    {
        SpawnProductCheck();
    }

    protected void SpawnProductCheck()
    {
        if (IsSpawn && IsStorageEmpty)
        {
            IngridientCheck();
        }
    }

    protected void Ingredient1Request(FactoryInputStorage factoryInputStorage)
    {
        IEnumerator transitToFactory = factoryInputStorage.TransitToFactory(_factorySpawn, this);
        if(Ingredient1 == null)
        {
            factoryInputStorage.StartCoroutine(transitToFactory);
        }
        if(Ingredient1 != null)
        {
            factoryInputStorage.StopCoroutine(transitToFactory);
        }
    }
    protected void Ingredient2Request(FactoryInputStorage factoryInputStorage)
    {
        IEnumerator transitToFactory = factoryInputStorage.TransitToFactory(_factorySpawn, this);
        if (Ingredient2 == null)
        {
            factoryInputStorage.StartCoroutine(transitToFactory);
        }
        if (Ingredient2 != null)
        {
            factoryInputStorage.StopCoroutine(transitToFactory);
        }
    }

    protected abstract void IngridientCheck();

    public void SetIngredient1(GameObject ingredient)
    {
        Ingredient1 = ingredient;
    }

    public void SetIngredient2(GameObject ingredient)
    {
        Ingredient2 = ingredient;
    }
}
