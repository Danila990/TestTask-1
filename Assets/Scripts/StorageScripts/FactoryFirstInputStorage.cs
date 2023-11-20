using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryFirstInputStorage : FactoryInputStorage
{
    protected override void SetIngredient(NeedInputFactory needInputFactory, GameObject ingridient)
    {
        needInputFactory.SetIngredient1(ingridient);
    }
}
