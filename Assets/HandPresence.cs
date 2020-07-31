using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.XR;
using Debug = UnityEngine.Debug;

public class HandPresence : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        //Debug.Log("Hello: " + gameObject.name);
        /*
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        */
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controllerPrefebs=>controllerPrefebs.name == targetDevice.name);
            if(prefab)
            {
                spawnedController = Instantiate(prefab, transform);     
                //spawnedHandModel = Instantiate(handModelPrefab, transform);           
            }
            else
            {
                Debug.LogError("Did not find controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
        }
        

    }

    // Update is called once per frame
    void Update()
    {        
        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton,out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("Primary Button has been pressed");

        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger,out float triggerValue) && triggerValue>0.1f)
            Debug.Log("Trigger has been pressed" + triggerValue);

        if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis,out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary2DAxis has been pressed" + primary2DAxisValue);
        
        if(showController)
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);      
        }
    }
}
