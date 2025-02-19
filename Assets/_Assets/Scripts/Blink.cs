using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public Sprite eyeOpenSprite;
    public Sprite eyeClosedSprite;
    public float blinkInterval = 2f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            spriteRenderer.sprite = eyeClosedSprite;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.sprite = eyeOpenSprite;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
