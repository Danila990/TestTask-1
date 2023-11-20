using UnityEngine;

public class FactorySecondInputStorage : FactoryInputStorage
{
    protected override void SetIngredient(NeedInputFactory needInputFactory, GameObject ingridient)
    {
        needInputFactory.SetIngredient2(ingridient);
    }
}
