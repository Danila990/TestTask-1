using UnityEngine;
using TMPro;
using System.Collections;

public class FactoryOutputInfoView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _factoryInfoTexst;
    [SerializeField]
    private Factory _factory;

    private void Start()
    {
        StartCoroutine(ResetFactoryInfoTexstDeley());  
    }

    private IEnumerator ResetFactoryInfoTexstDeley()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _factoryInfoTexst.text = _factory.FactoryOutputInfoCheck();
        }
    }
}
