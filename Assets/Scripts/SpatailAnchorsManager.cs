using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAnchorsManager : MonoBehaviour
{
    //Test
    [SerializeField]
    private GameObject anchorPrefab;

    private const ulong invalidAnchorHandle = ulong.MaxValue;

    public enum StorageLocation
    {
        LOCAL
    }

    // HandleId, RequestId
    public Dictionary<ulong, ulong> locateAnchorRequest = new Dictionary<ulong, ulong>();

    //HandleId, Gameobject 
    public Dictionary<ulong, GameObject> resolveAnchors = new Dictionary<ulong, GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {

    }
}
