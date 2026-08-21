using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private Image redFlash;

    public void PlayDamageEffect()
    {
        StartCoroutine(DamageAnimation());
    }

    private IEnumerator DamageAnimation()
    {
        Color color = redFlash.color;

        color.a = 0.0f;
        redFlash.color = color;

        // Fade in
        float timer = 0;

        while (timer < 0.1f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0,
                0.65f,
                timer / 0.1f
            );

            redFlash.color = color;

            yield return null;
        }

        // Fade out
        timer = 0;

        while (timer < 0.25f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0.65f,
                0,
                timer / 0.25f
            );

            redFlash.color = color;

            yield return null;
        }

        color.a = 0;
        redFlash.color = color;
    }
}