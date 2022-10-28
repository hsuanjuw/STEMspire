using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFlash : Button
{
    public bool flashAllowed = true;
    public bool flashButton = true;
    
    public Image btnImage;

    // Update is called once per frame
    void Start()
    {
        base.Start();
        btnImage = GetComponent<Image>();
        if (flashButton)
            StartCoroutine(FlashWhite());
    }
    
    public void RemoveFlashAbility()
    {
        flashAllowed = false;
        flashButton = false;
        btnImage.color = Color.white;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        flashButton = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        flashButton = true;
        StartCoroutine(FlashWhite());
    }

    private IEnumerator FlashBlack()
    {
        if(flashButton && flashAllowed)
        btnImage.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        if(flashButton && flashAllowed)
            StartCoroutine(FlashWhite());
    }
    private IEnumerator FlashWhite()
    {
        if(flashButton && flashAllowed)
        btnImage.color = Color.white;
        yield return new WaitForSeconds(1f);
        if(flashButton && flashAllowed)
            StartCoroutine(FlashBlack());
    }

    public void ResetFlash()
    {
        flashAllowed = true;
        flashButton = true;
        StartCoroutine(FlashWhite());
    }

}
