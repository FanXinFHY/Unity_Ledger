using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMonthPanel : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.instance.RefreshMonthButtonListContent();
    }
}
