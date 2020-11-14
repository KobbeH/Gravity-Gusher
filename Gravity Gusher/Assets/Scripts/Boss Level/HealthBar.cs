using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public Image barSprite;
    private bool isFlashing;
    private float flashTimer;
    void Start()
    {
        flashTimer = 0;
    }

    void Update()
    {   
        if(isFlashing) {
            if(flashTimer < 0.9f) {
                flashTimer += Time.deltaTime;
                barSprite.GetComponent<Image>().color = new Color32 (90, 90, 90, 255);
            } else {
                flashTimer = 0;
                isFlashing = false;
                barSprite.GetComponent<Image>().color = new Color32 (255, 0, 0, 255);
            }
        }
    }

    public void SetSize(float barSizeNormal) {
        
        if(bar.localScale.x >= 0) {
            bar.localScale = new Vector3(barSizeNormal, 1f);
        }
        
        if(bar.localScale.x < 0) {
            bar.localScale = new Vector3(0f, 1f);
        }
    }

    public void setFlashing() {
        isFlashing = true;
    }
}
