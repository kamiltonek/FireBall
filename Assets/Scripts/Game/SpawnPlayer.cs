using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject [] skinList;
    void Start()
    {
        Instantiate(skinList[SaveAndLoad.getSelectedSkin()], transform.position, Quaternion.identity, transform);
    }
}
