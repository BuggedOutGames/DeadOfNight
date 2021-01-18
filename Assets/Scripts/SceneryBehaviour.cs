using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneryBehaviour : MonoBehaviour {
    
    private const string SceneryAboveLayer = "SceneryAbove";
    private const string SceneryBelowLayer = "SceneryBelow";

    private SpriteRenderer spriteRenderer;
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            ChangeLayer(SceneryAboveLayer);
            StartCoroutine(TransitionOpacity(0.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            ChangeLayer(SceneryBelowLayer);
            StartCoroutine(TransitionOpacity(1f));
        }
    }

    private float GetOpacity() {
        return spriteRenderer.color.a;
    }
    
    private void SetOpacity(float opacity) {
        var spriteColor = spriteRenderer.color;
        spriteColor.a = opacity;
        spriteRenderer.color = spriteColor;
    }

    private void ChangeLayer(string layerName) {
        spriteRenderer.sortingLayerID = SortingLayer.NameToID(layerName);
    }

    private IEnumerator TransitionOpacity(float finalOpacity) {
        var time = 0f;
        const float duration = 0.5f;
        var startOpacity = GetOpacity();
        while (time < duration) {
            var newOpacity = Mathf.Lerp(startOpacity, finalOpacity, time / duration);
            SetOpacity(newOpacity);
            time += Time.deltaTime;
            yield return null;
        }
        SetOpacity(finalOpacity);
    }
}