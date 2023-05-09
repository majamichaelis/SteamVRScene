using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EyetrackingRay : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 1.0f;

    [SerializeField]
    private float rayWidth = 0.01f;

    [SerializeField]
    private LayerMask layersToInclude;

    [SerializeField]
    private Color rayColorDefaultState = Color.yellow;

    [SerializeField]
    private Color rayColorHoverState = Color.red;

    [SerializeField]
    private OVRHand handUsedforPinchSelection;

    [SerializeField]
    private bool mockHandUsedforPinchSelection;

    private bool intercepting;

    private bool allowPinchSelection;

    private LineRenderer lineRenderer;

    private Dictionary<int, EyeInteractable> interactables = new Dictionary<int, EyeInteractable>();

    private EyeInteractable lastEyeInteractable; 

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        allowPinchSelection = handUsedforPinchSelection != null;
        SetUpRay();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.enabled = !isPinching();

        SelectingStarted();

        //clear all hover states
        if (!intercepting)
        {
            lineRenderer.startColor = lineRenderer.endColor = rayColorDefaultState;
            lineRenderer.SetPosition(1, new Vector3(0, 0, transform.position.z + rayDistance));
            OnHoverEnded();
        }
    }

    private void SetUpRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;
        lineRenderer.startColor = rayColorDefaultState;
        lineRenderer.endColor = rayColorDefaultState;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + rayDistance));
    }

    private void SelectingStarted()
    {
        if (isPinching())
        {
            lastEyeInteractable?.Select(true, (handUsedforPinchSelection?.IsTracked ?? false) ? handUsedforPinchSelection.transform : transform);
        }
        else
        {
            //?????? hier das ,transform ist von mir 
            lastEyeInteractable?.Select(false, transform);
        }
    }
    //Vielleicht kein Ray in die Unendlichkeit?
    private void FixedUpdate()
    {
        if (isPinching()) return;

        Vector3 rayDirection = transform.TransformDirection(Vector3.forward) * rayDistance;

        intercepting = Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, Mathf.Infinity, layersToInclude);

        if (intercepting)
        {
            OnHoverEnded();
            lineRenderer.startColor = lineRenderer.endColor = rayColorHoverState;

            //keep cache of eye interactables
            if(!interactables.TryGetValue(hit.transform.GetHashCode(), out EyeInteractable eyeInteractable))
            {
                eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
                interactables.Add(hit.transform.gameObject.GetHashCode(), eyeInteractable);
            }

            var toLocalSpace = transform.InverseTransformPoint(eyeInteractable.transform.position);
            lineRenderer.SetPosition(1, new Vector3(0, 0, toLocalSpace.z));


            eyeInteractable.Hover(true);
            lastEyeInteractable = eyeInteractable; 
        }
    }

    private bool isPinching() => (allowPinchSelection && handUsedforPinchSelection.GetFingerIsPinching(OVRHand.HandFinger.Index)) || mockHandUsedforPinchSelection;

    private void OnDestroy() => interactables.Clear();

    private void OnHoverEnded()
    {
        foreach (var interactable in interactables)
        {
            Debug.LogError(interactable.Value);
            interactable.Value.Hover(false);
        }
    }
    

}
