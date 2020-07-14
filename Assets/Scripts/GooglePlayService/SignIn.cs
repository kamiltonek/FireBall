using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignIn : MonoBehaviour
{
    void Start()
    {
        PlayService.SignInToGooglePlayService();
    }
}
