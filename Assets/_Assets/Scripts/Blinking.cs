using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
   
    public Sprite TaptoPlayBlink;
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
            spriteRenderer.sprite = TaptoPlayBlink;
            yield return new WaitForSeconds(0.1f);
           
        }
    }
}

