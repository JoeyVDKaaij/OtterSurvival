using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    int distance;
    [SerializeField]
    LayerMask lookAtMask;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    string PickUp = "Press F";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, distance, lookAtMask))
        {

            itemName.text = PickUp;

        }
        else
        {
            itemName.text = null;



        }
    }
}
