using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR;
using Debug = UnityEngine.Debug;

public class HandPresence : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawedController;
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
            GameObject prefab = controllerPrefabs.Find(controllerPrefabs=>controllerPrefabs.name == targetDevice.name);
            if(prefab)
            {
                spawedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Do not find the controller model!");
                spawedController = Instantiate(controllerPrefabs[0], transform);
            }
        
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("Primary Button has been pressed!!");
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
            Debug.Log("Trigger Button has been pressed!!");
        if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis,out Vector2 primary2DAxis) && primary2DAxis!=Vector2.zero)
            Debug.Log("Primary 2D Axis values is: " + primary2DAxis);
        
    }
}
