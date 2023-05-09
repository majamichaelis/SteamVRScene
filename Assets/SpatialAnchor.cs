using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAnchor : MonoBehaviour
{
    private bool ShowSaveIcon = false;
    private OVRSpatialAnchor anchor;

    // Start is called before the first frame update
    void Start()
    {
        if(!ShowSaveIcon)
        this.gameObject.AddComponent<OVRSpatialAnchor>();


        GetComponent<OVRSpatialAnchor>().Save((anchor, success) =>
        {
            ShowSaveIcon = success;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        OVRSpatialAnchor anchor = this.GetComponent<OVRSpatialAnchor>();
        //anchor.;

    }
}
