using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Sprite hearth;
    public Vector2 spriteSize = new Vector2(64, 64);
    public float spaceBetween = 10;

    private List<Image> images;

    void Start()
    {
        images = new List<Image>();

        for(int i = 0; i < EnvironmentSystem.InitLife; i++)
        {
            GameObject newGO = new GameObject("Life-" + i);
            newGO.transform.parent = transform;
            
            
            Image tmpImage = newGO.AddComponent<Image>();
            images.Add(tmpImage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateByLife();
    }

    private void UpdateByLife()
    {
        
        for (int i = 0; i < images.Count; i++)
        {
            if(i < EnvironmentSystem.CurrentLife)
            {
                images[i].enabled = true;
                RectTransform rt = images[i].gameObject.GetComponent<RectTransform>();
                //rt.position = new Vector3(i + spriteSize.x + spaceBetween, 0, 0);
                rt.anchoredPosition = new Vector3(i * (spriteSize.x + spaceBetween), 0, 0);
                rt.sizeDelta = spriteSize;
                images[i].sprite = hearth;
            } else
            {
                images[i].enabled = false;
            }
        }
    }
}
