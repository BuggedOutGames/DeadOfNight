using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SceneryBehaviour : MonoBehaviour {
    
    private const string SceneryAboveLayer = "SceneryAbove";
    private const string SceneryBelowLayer = "SceneryBelow";

    private SpriteRenderer spriteRenderer;
    private bool transparent;
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            if (IsBehind(other.transform) && !transparent) {
                MakeTransparent();
            } else if (!IsBehind(other.transform) && transparent) {
                MakeOpaque();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            MakeOpaque();
        }
    }

    private bool IsBehind(Transform other) {
        var otherY = other.FindChildByTag("Base")?.position.y ?? other.position.y;
        var thisY = transform.FindChildByTag("Base")?.position.y ?? transform.position.y;
        return otherY > thisY;
    }

    private void MakeTransparent() {
        transparent = true;
        StartCoroutine(TransitionOpacity(0.5f));
        ChangeLayer(SceneryAboveLayer);
    }

    private void MakeOpaque() {
        transparent = false;
        StartCoroutine(TransitionOpacity(1f));
        ChangeLayer(SceneryBelowLayer);
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