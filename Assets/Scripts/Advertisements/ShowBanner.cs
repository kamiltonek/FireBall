using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBanner : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Try to show banner");
        AdController.Instance.showBanner();      

    }
}
