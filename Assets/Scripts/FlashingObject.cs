using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FlashingObject : MonoBehaviour {

    //config
    [SerializeField] protected Color highlightedColor;
    [Range(0f, 1f)] [SerializeField] private float flashingSpeed = 0.1f;

    //cached
    private Color defaultColor;
    private MeshRenderer meshRenderer;

    //dependencies
    [Inject] InputPlayerActions input;

    //state
    private bool isFlashing;

    void Awake () {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
    }

    private void Update()
    {
        if(input.SelectedObject == this.gameObject && !isFlashing)
        {
            StartFlashing();
        }
        else if(input.SelectedObject != this.gameObject && isFlashing)
        {
            StopFlashing();
        }
    }

    public void StartFlashing()
    {
        isFlashing = true;
        StartCoroutine(FlashCoroutine());
    }

    public void StopFlashing()
    {
        isFlashing = false;
        StopAllCoroutines();
        meshRenderer.material.color = defaultColor;
    }

    private IEnumerator FlashCoroutine()
    {
        Color currentColor;
        float t = 1;
        float derivative = flashingSpeed;
        while (true)
        {
            currentColor = Vector4.Lerp(defaultColor, highlightedColor, t);
            meshRenderer.material.color = currentColor;

            t = t + derivative;
            if (t > 1)
            {
                t = 1;
                derivative = -derivative;
            }
            else if (t < 0)
            {
                t = 0;
                derivative = -derivative;
            }
            yield return null;
        }
    }
}
