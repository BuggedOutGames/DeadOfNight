using System.Collections;
using System.Text.RegularExpressions;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer), typeof(SortingGroup))]
public class SceneryBehaviour : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private int scenerySortLevel;
    private bool transparent;
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scenerySortLevel = GetSortLevel(GetComponent<SortingGroup>());
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            if (IsInFrontOf(other.transform.parent)) {
                RenderBehindScenery(other.transform.parent);
                if (other.GetComponentInParent<NetworkBehaviour>().isLocalPlayer) {
                    MakeTransparent();
                }
            } else if (!IsInFrontOf(other.transform.parent)) {
                RenderAboveScenery(other.transform.parent);
                MakeOpaque();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("PlayerTrigger")) {
            RenderAboveScenery(other.transform.parent);
            MakeOpaque();
        }
    }

    private bool IsInFrontOf(Transform other) {
        var thisY = transform.FindChildByTag("Base")?.position.y;
        var otherY = other.FindChildByTag("Base")?.position.y;
        return thisY < otherY;
    }

    private void MakeTransparent() {
        if (!transparent) {
            transparent = true;
            StartCoroutine(TransitionOpacity(0.5f));
        }
    }

    private void MakeOpaque() {
        if (transparent) {
            transparent = false;
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
    
    private void RenderBehindScenery(Component other) {
        var otherSortingGroup = other.GetComponentInChildren<SortingGroup>();
        if (otherSortingGroup != null) {
            var otherSortLevel = GetSortLevel(otherSortingGroup);
            if (otherSortLevel >= scenerySortLevel) {
                ChangeSortingGroupLayer(otherSortingGroup, "Level " + (scenerySortLevel - 1));
            }
        }
    }

    private void RenderAboveScenery(Component other) {
        var otherSortingGroup = other.GetComponentInChildren<SortingGroup>();
        if (otherSortingGroup != null) {
            var otherSortingLevel = GetSortLevel(otherSortingGroup);
            if (otherSortingLevel == scenerySortLevel - 1) {
                ChangeSortingGroupLayer(otherSortingGroup, "Level " + scenerySortLevel);
            }
        }
    }
    
    private static void ChangeSortingGroupLayer(SortingGroup sortingGroup, string sortingLayer) {
        sortingGroup.sortingLayerID = SortingLayer.NameToID(sortingLayer);
    }

    private static int GetSortLevel(SortingGroup sortingGroup) {
        var sortLevelRegex = new Regex(@"^Level (?<level>\d)$", RegexOptions.Compiled);
        var match = sortLevelRegex.Match(sortingGroup.sortingLayerName);
        return int.Parse(match.Groups["level"].Value);
    }
}