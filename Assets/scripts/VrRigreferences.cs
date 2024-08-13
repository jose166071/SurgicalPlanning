using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrRigreferences : MonoBehaviour
{
    public static VrRigreferences Singleton;
    
    public Transform root;
    public Transform Head;
    public Transform Left;
    public Transform Rigth;

    private void Awake()
    {
        Singleton = this;
    }
}
