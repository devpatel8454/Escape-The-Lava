using System.Collections;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;

    [Header("Animation")]
    [SerializeField] private float moveDistance = 60f;
    [SerializeField] private float duration = 0.8f;

    private RectTransform rectTransform;
    private Color startColor;
    private Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startColor = popupText.color;
    }

    private void Start()
    {
        popupText.text = "+1";

        startPosition = rectTransform.localPosition;

        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / duration;

            // Move upward
            rectTransform.localPosition =
                startPosition +
                Vector3.up * (moveDistance * progress);

            // Fade out
            Color color = startColor;
            color.a = Mathf.Lerp(1f, 0f, progress);

            popupText.color = color;

            // Small scale animation
            float scale = Mathf.Lerp(0.5f, 1f, progress);

            transform.localScale =
                Vector3.one * scale;

            yield return null;
        }

        Destroy(gameObject);
    }
}