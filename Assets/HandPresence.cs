using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR;
using Debug = UnityEngine.Debug;

public class HandPresence : MonoBehaviour
{
    // Start is called before the first frame update
   
    private InputDevices targetDevice;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics,devices);


        Debug.Log("Hello: " + gameObject.name);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
