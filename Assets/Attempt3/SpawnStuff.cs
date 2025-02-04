using UnityEngine;
using UnityEngine.UI;

public class SpawnStuff : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField]
    int quantity;
    [SerializeField]
    RectTransform template;
    [SerializeField]
    float buffer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Image[] children = content.GetComponentsInChildren<Image>();
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
            rect.sizeDelta = template.sizeDelta;
            Vector3 offset = new(template.offsetMin.x, (template.offsetMin.y - buffer)*index-buffer);
            newStuff.transform.position = content.transform.position + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
