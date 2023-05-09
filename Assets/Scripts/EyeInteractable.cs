using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class EyeInteractable : MonoBehaviour
{
    [field: SerializeField]
    public bool isHovered { get; set; }

    [field: SerializeField]
    public bool isSelected { get; private set; }

    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private UnityEvent<GameObject> OnObjectSelected;

    [SerializeField]
    private Material OnHoverActiveMaterial;

    [SerializeField]
    private Material OnIdleMaterial;

    [SerializeField]
    private Material OnSelectActiveMaterial;

    private MeshRenderer meshRenderer;

    private Transform originalAnchor;

    private TextMeshPro statusText;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        statusText = GetComponentInChildren<TextMeshPro>();
        originalAnchor = transform.parent;
    }

    public void Hover(bool state)
    {
        isHovered = state;
    }

    public void Select(bool state, Transform anchor)
    {
        isSelected = state;
        if (anchor) transform.SetParent(anchor);
        if (!isSelected) transform.SetParent(originalAnchor);
    }
    // Update is called once per frame
    void Update()
    {
        if(isHovered)
        {
            meshRenderer.material = OnHoverActiveMaterial;
            OnObjectHover?.Invoke(gameObject);
            statusText.text = $"HOVERED";
        }
        if(isSelected)
        {
            meshRenderer.material = OnSelectActiveMaterial;
            OnObjectSelected?.Invoke(gameObject);
            statusText.text = $"SELECTED";
        }
        if(!isHovered && !isSelected)
        {
            meshRenderer.material = OnSelectActiveMaterial;
            statusText.text = $"IDLE";
        }
    }
}
