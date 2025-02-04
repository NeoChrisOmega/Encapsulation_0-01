using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Attempt2
{
    public class Generation : MonoBehaviour
    {
        bool isManualCharging;
        bool isAutoCharging;
        static bool isManualRequirementMet;
        static bool isAutoRequirementMet;
        static float manualChargeTime;
        static float autoChargeTime;
        static float totalDuration;
        static int resourceCountA;
        static int resourceCountB;
        static int resourceCountC;
        static int resourceCountD;
        static int resourceCountE;
        public TextMeshProUGUI manualDisplayText;
        public TextMeshProUGUI autoDisplayText;
        static public TextMeshProUGUI resourceDisplayA;
        static public TextMeshProUGUI resourceDisplayB;
        static public TextMeshProUGUI resourceDisplayC;
        static public TextMeshProUGUI resourceDisplayD;
        static public TextMeshProUGUI resourceDisplayE;
        static Dictionary<string, float> productionTotals = new Dictionary<string, float>();
        public TextMeshProUGUI debugDisplay;

        // Start is called before the first frame update
        void Start()
        {
            ListifySpecs();
            InitializeResourcesUI();
            //debugDisplay.gameObject.SetActive(false); This apparently doesn't work on GitHub WebGL???
        }

        void ListifySpecs()
        {
            foreach (Specification spec in Specifications.specifications)
            {
                foreach(string resource in spec.GetProducedResources())
                {
                    if (productionTotals.ContainsKey(resource) == false)
                    {
                        productionTotals.Add(resource, 0);
                    }
                }
            }
            productionTotals.Add("A", 0);
            productionTotals.Add("E", 0);
        }

        void InitializeResourcesUI()
        {
            resourceDisplayA = GameObject.Find("LabelA").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            resourceDisplayB = GameObject.Find("LabelB").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            resourceDisplayC = GameObject.Find("LabelC").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            resourceDisplayD = GameObject.Find("LabelD").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            resourceDisplayE = GameObject.Find("LabelE").transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            resourceDisplayA.text = 0.ToString("F0");
            resourceDisplayB.text = 0.ToString();
            resourceDisplayC.text = 0.ToString();
            resourceDisplayD.text = 0.ToString();
            resourceDisplayE.text = 0.ToString();
            resourceDisplayE.transform.parent.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //This is temperary to create the base value of A that goes up at an inconsistent rate
            float randA = UnityEngine.Random.Range(0.0f, 1+Time.deltaTime);
            debugDisplay.text += $"{randA:F1}, ";
            resourceCountA += Convert.ToInt32(Math.Floor(randA));
            //ugh... I need to rework ModifyResource to allow rare production
            productionTotals["A"] += Convert.ToInt32(Math.Floor(randA)); 
            resourceDisplayA.text = resourceCountA.ToString();
            DisplayProduction();
            CheckCooldowns();

            //This is a temp addition, it's very, very messy
            float maths;

            var productionCountA = resourceDisplayA.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            maths = productionTotals["A"] / Time.realtimeSinceStartup;
            productionCountA.text = $"{maths:F2}/s";
            var productionCountB = resourceDisplayB.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            maths = productionTotals["B"] / Time.realtimeSinceStartup;
            productionCountB.text = $"{maths:F2}/s";
            var productionCountC = resourceDisplayC.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            maths = productionTotals["C"] / Time.realtimeSinceStartup;
            productionCountC.text = $"{maths:F2}/s";
            var productionCountD = resourceDisplayD.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            maths = productionTotals["D"] / Time.realtimeSinceStartup;
            productionCountD.text = $"{maths:F2}/s";
            var productionCountE = resourceDisplayE.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            maths = productionTotals["E"] / Time.realtimeSinceStartup;
            productionCountE.text = $"{maths:F2}/s";
        }

        void CheckCooldowns()
        {
            if (isManualRequirementMet == false)
            {
                isManualRequirementMet = CheckProductionRequirements(Specifications.selectedSpec);

                if (isManualRequirementMet && Input.GetMouseButton(0))
                {
                    DecrementResources();
                }
            }
            UpdateManualCooldown();

            if (isAutoRequirementMet == false)
            {
                isAutoRequirementMet = CheckProductionRequirements(Specifications.selectedSpec);

                if (isAutoRequirementMet)
                {
                    DecrementResources();
                }
            }
            UpdateAutomationCooldown();

            CheckProductionRequirements(Specifications.selectedSpec);
        }
        public static bool CheckProductionRequirements(Specification spec)
        {
            var groupedResources = ModifyResources(0, spec);
            bool isRequirementMet = true;
            resourceDisplayA.color = Color.black;
            resourceDisplayB.color = Color.black;
            resourceDisplayC.color = Color.black;
            resourceDisplayD.color = Color.black;
            resourceDisplayE.color = Color.black;

            foreach (IGrouping<string, string> group in groupedResources)
            {
                switch (group.Key)
                {
                    case "A":
                        if (resourceCountA < group.Count())
                        {
                            resourceDisplayA.color = Color.red;
                            isRequirementMet = false;
                        }
                        else
                        {
                            resourceDisplayA.color = Color.green;
                        }
                        break;
                    case "B":
                        if (resourceCountB < group.Count())
                        {
                            resourceDisplayB.color = Color.red;
                            isRequirementMet = false;
                        }
                        else
                        {
                            resourceDisplayB.color = Color.green;
                        }
                        break;
                    case "C":
                        if (resourceCountC < group.Count())
                        {
                            resourceDisplayC.color = Color.red;
                            isRequirementMet = false;
                        }
                        else
                        {
                            resourceDisplayC.color = Color.green;
                        }
                        break;
                    case "D":
                        if (resourceCountD < group.Count())
                        {
                            resourceDisplayD.color = Color.red;
                            isRequirementMet = false;
                        }
                        else
                        {
                            resourceDisplayD.color = Color.green;
                        }
                        break;
                    case "E":
                        if (resourceCountE < group.Count())
                        {
                            resourceDisplayE.color = Color.red;
                            isRequirementMet = false;
                        }
                        else
                        {
                            resourceDisplayE.color = Color.green;
                        }
                        break;
                    default:
                        Debug.LogError("ModifyResources had unhandled resource in isRequiredMet()'s switch statement");
                        break;
                }
            }

            return isRequirementMet;
        }
        void UpdateManualCooldown()
        {
            if(totalDuration > 0 && isManualRequirementMet)
            {
                manualDisplayText.color = Color.black;
                if (isManualCharging == false)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        DecrementResources();
                        isManualCharging = true;
                    }
                    else
                    {
                        foreach (Touch touch in Input.touches)
                        {
                            if (touch.phase == TouchPhase.Began)
                            {
                                DecrementResources();
                                isManualCharging = true;
                            }
                        }
                    }
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    manualDisplayText.color = Color.black;
                    if(manualChargeTime < 1)
                    {
                        ModifyResources(9001, Specifications.selectedSpec); //temp version to refund resources
                    }
                    isManualRequirementMet = false;
                    isManualCharging = false;
                    manualChargeTime = 0;
                }
                else
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                        {
                            manualDisplayText.color = Color.black;
                            if (manualChargeTime < 1)
                            {
                                ModifyResources(9001, Specifications.selectedSpec); //temp version to refund resources
                            }
                            isManualRequirementMet = false;
                            isManualCharging = false;
                            manualChargeTime = 0;
                        }
                    }
                }
                if (isManualCharging)
                {
                    manualDisplayText.color = Color.green;
                    manualChargeTime += Time.deltaTime;
                }
                if (manualChargeTime >= totalDuration)
                {
                    manualDisplayText.color = Color.yellow;
                    IncrementResources(); 
                    manualDisplayText.transform.parent.GetComponentInChildren<ParticleSystem>().Play();
                    isManualRequirementMet = false;
                    manualChargeTime = 0;
                }
            }
            else
            {
                manualDisplayText.color = Color.red;
                manualChargeTime = 0;
            }
        }
        void UpdateAutomationCooldown()
        {
            if (totalDuration > 0 && isAutoCharging && isAutoRequirementMet)
            {
                autoDisplayText.color = Color.green;
                autoChargeTime += Time.deltaTime;
                if (autoChargeTime >= totalDuration)
                {
                    autoDisplayText.color = Color.yellow;
                    IncrementResources(); 
                    autoDisplayText.transform.parent.GetComponentInChildren<ParticleSystem>().Play();
                    isAutoRequirementMet = false;
                    autoChargeTime = 0;
                }
            }
            else
            {
                autoDisplayText.color = Color.red;
                autoChargeTime += Time.deltaTime;
                isAutoCharging = autoChargeTime > 1;
                autoChargeTime %= 1;
            }
        }
        static void IncrementResources()
        {
            ModifyResources(1, Specifications.selectedSpec);
        }
        static void DecrementResources()
        {
            ModifyResources(-1, Specifications.selectedSpec);
        }
        /// <summary>
        /// If you modify by zero, the values will not change, but instead it will simply count how many resources are required
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        static IEnumerable<IGrouping<string, string>> ModifyResources(int modifier, Specification spec)
        {
            List<string> resourcesModified = new List<string>();
            string[] resources;

            if (modifier <= 0)
            {
                resources = spec.GetRequiredResources();
            }
            else
            {
                resources = spec.GetProducedResources();
            }
            if (modifier == 9001) //refund unused resources
            {
                resources = spec.GetRequiredResources();
                modifier = 1;
                Debug.LogWarning("Resources Refunded... might want a different approach to this");
            }

            foreach (string resource in resources)
            {
                productionTotals[resource] += modifier;
                resourcesModified.Add(resource);
                switch (resource)
                {
                    case "A":
                        resourceCountA += modifier;
                        resourceDisplayA.text = resourceCountA.ToString("F0");
                        break;
                    case "B":
                        resourceCountB += modifier;
                        resourceDisplayB.text = resourceCountB.ToString("F0");
                        break;
                    case "C":
                        resourceCountC += modifier;
                        resourceDisplayC.text = resourceCountC.ToString("F0");
                        break;
                    case "D":
                        resourceCountD += modifier;
                        resourceDisplayD.text = resourceCountD.ToString("F0");
                        break;
                    case "E":
                        resourceCountE += modifier;
                        if (resourceDisplayE.isActiveAndEnabled == false)
                        {
                            resourceDisplayE.transform.parent.gameObject.SetActive(true);
                        }
                        resourceDisplayE.text = resourceCountE.ToString("F0");
                        break;
                    default:
                        Debug.LogError("Specification had unhandled resource in Generation.ModifyResources()'s switch statement");
                        break;
                }
            }

            IEnumerable<IGrouping<string,string>> modifiedGrouped = resourcesModified.GroupBy(i => i);
            return modifiedGrouped;
        }

        void DisplayProduction()
        {
            DisplayCooldowns();
        }
        void DisplayCooldowns()
        {
            manualDisplayText.text = $"{manualChargeTime:F2}/{totalDuration:F2}";
            if(totalDuration > 0 && isAutoRequirementMet)
            {
                autoDisplayText.text = $"{autoChargeTime:F2}/{totalDuration:F2}";
            }
            else
            {
                autoDisplayText.text = $"{0:F2}/{totalDuration:F2}";
            }
        }

        public static void UpdateDuration(TMP_InputField duration)
        {
            if(float.TryParse(duration.text, out float durationValue))
            {
                totalDuration = durationValue;
                UpdateDuration();
            }
        }
        public static void UpdateDuration(float duration)
        {
            totalDuration = duration;
            UpdateDuration();
        }
        static void UpdateDuration()
        {
            autoChargeTime %= 1;
            manualChargeTime = 0;
            isAutoRequirementMet = false;
            isManualRequirementMet = false;
        }
    }
}