using UnityEngine;
using System;

public class BelajarDelegate : MonoBehaviour
{
delegate void ContohDelegate();
    
    void Start()
    {
        UjiDelegate1();
        UjiDelegate2();
        UjiDelegate3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UjiDelegate1()
    {
        ContohDelegate halo = PanggilHello;
        halo();
    }

    void UjiDelegate2()
    {
        ContohDelegate halo = PanggilHello;
        halo += PanggilNama;
        halo();
    }

    void UjiDelegate3()
    {
        Action halo = PanggilHello;
        halo += PanggilNama;
        halo();
    }

    void PanggilHello()
    {
        Debug.Log("Hello");
    }

    void PanggilNama()
    {
        Debug.Log("Nama saya FATAH");
    }
}
