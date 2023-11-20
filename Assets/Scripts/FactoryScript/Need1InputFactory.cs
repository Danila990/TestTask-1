using UnityEngine;

public class Need1InputFactory : NeedInputFactory
{
    [SerializeField]
    private FactoryFirstInputStorage _factoryFirstInputStorage;

    private bool _isNeedIngridient;

    protected override void IngridientCheck()
    {
        StartIngredientRequest();
        Factory1InputInfoCheck();
        if (Ingredient1 != null)
        {
            SpawnProduct();
            Ingredient1.GetComponent<ProductMover>().Destroy();
            Ingredient1 = null;
            _isNeedIngridient = false;
        }
    }

    private void StartIngredientRequest()
    {
        Ingredient1Request(_factoryFirstInputStorage);
        _isNeedIngridient = true;
    }

    public string Factory1InputInfoCheck()
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
