using UnityEngine;

public class Need2InputFactory : NeedInputFactory
{
    [SerializeField]
    private FactoryFirstInputStorage _factoryFirstInputStorage;
    [SerializeField]
    private FactorySecondInputStorage _factorySecondInputStorage;

    private bool _isNeedIngridient;

    protected override void IngridientCheck()
    {
        StartIngredientRequest();
        if (Ingredient1 != null && Ingredient2 != null)
        {
            SpawnProduct();
            Ingredient1.GetComponent<ProductMover>().Destroy();
            Ingredient1 = null;
            Ingredient2.GetComponent<ProductMover>().Destroy();
            Ingredient2 = null;
            _isNeedIngridient = false;
        }
    }

    private void StartIngredientRequest()
    {
        Ingredient1Request(_factoryFirstInputStorage);
        Ingredient2Request(_factorySecondInputStorage);
        _isNeedIngridient = true;
    }

    public string Factory2InputInfoCheck()
    {
        if (_isNeedIngridient)
        {
            return FactoryName + " - " + "Input Storage Enpty";
        }
        if (_isNeedIngridient == false)
        {
            return "";
        }
        return "";
    }
}
