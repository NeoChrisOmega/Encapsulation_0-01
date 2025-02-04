using UnityEngine;
using UnityEngine.UI;

public class SpawnStuff : MonoBehaviour
{
    [SerializeField]
    int quantity;
    [SerializeField]
    RectTransform template;
    [SerializeField]
    float buffer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Image[] children = GetComponentsInChildren<Image>();
        foreach (Image child in children)
        {
            if(child.gameObject != template.gameObject)
            {
                Destroy(child.gameObject);
            }
        }

        for(int index = 0; index < quantity; index++)
        {
            GameObject newStuff = Instantiate(template.gameObject);
            RectTransform rect = newStuff.GetComponent<RectTransform>();
            newStuff.transform.SetParent(transform);
            rect.anchorMax = template.anchorMax;
            //rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -buffer - ((rect.sizeDelta.y + buffer) * index) );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
