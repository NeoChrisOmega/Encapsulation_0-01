using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabelMatchValue : MonoBehaviour
{
    TextMeshProUGUI myText;
    TextMeshProUGUI childText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        childText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.color = childText.color;
    }
}
