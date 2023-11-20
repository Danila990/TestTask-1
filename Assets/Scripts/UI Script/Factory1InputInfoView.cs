using System.Collections;
using TMPro;
using UnityEngine;

public class Factory1InputInfoView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _factoryInfoTexst;
    [SerializeField]
    private Need1InputFactory _factory;

    private void Start()
    {
        StartCoroutine(ResetFactoryInfoTexstDeley());
    }

    private IEnumerator ResetFactoryInfoTexstDeley()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _factoryInfoTexst.text = _factory.Factory1InputInfoCheck();
        }
    }
}
