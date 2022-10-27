using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerFill : MonoBehaviour
{
    public enum FillStatus
    {
        NotFilled,
        Filled
    };

    public FillStatus fillStatus = FillStatus.NotFilled;
    public Image[] powerImages;
    private int currentImageIndex;

    public void Start()
    {
        if(fillStatus == FillStatus.NotFilled)
            ResetFill();
    }
    public void FillNextImage(float fillAmt)
    {
        if (fillStatus == FillStatus.NotFilled)
        {
            float currentFill = powerImages[currentImageIndex].fillAmount;
            if (currentFill + fillAmt < 1)
            {
                powerImages[currentImageIndex].fillAmount += fillAmt;
            }
            else
            {
                float overfill = currentFill + fillAmt - 1;
                powerImages[currentImageIndex].fillAmount = 1;
                if (currentImageIndex < powerImages.Length - 1)
                {
                    currentImageIndex++;
                    FillNextImage(overfill);
                }
                else
                {
                    fillStatus = FillStatus.Filled;
                }
            } 
        }
    }

    public void ResetFill()
    {
        foreach (var seg in powerImages)
        {
            seg.fillAmount = 0;
        }

        currentImageIndex = 0;
        fillStatus = FillStatus.NotFilled;
    }
}
