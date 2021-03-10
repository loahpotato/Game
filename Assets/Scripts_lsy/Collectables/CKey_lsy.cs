using UnityEngine;

public class CKey_lsy : Collectables_lsy
{
    [SerializeField] private int isKey = 1;

    protected override void Pick()
    {
        PickKey();
    }

    private void PickKey()
    {
        KeyManager_lsy.Instance.AddKey(isKey);
    }
}
