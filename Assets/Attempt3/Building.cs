using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool isActive = true;

    public Image production;
    [SerializeField] TextMeshProUGUI titleName;
    public int quantity = 1;
    [SerializeField] TextMeshProUGUI counter;
    
    public float duration = 1;
    public float fillAmount = 0;
    [SerializeField] Image timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            titleName.text = production.sprite.name;
            fillAmount += Time.deltaTime;
            if (fillAmount / duration > 1)
            {
                FindFirstObjectByType<BuildingManager>().AddToInventory(production.sprite, quantity);
            }
            fillAmount %= duration;
            timer.fillAmount = fillAmount;
            counter.text = quantity.ToString();
        }
        else
        {
            titleName.text = "Empty";
            fillAmount = 0;
            timer.fillAmount = fillAmount;
            counter.text = "";
        }
    }
}
