using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Attempt2
{
    public class Specifications : MonoBehaviour
    {
        public static List<Specification> specifications = new List<Specification>();
        public TMP_Dropdown specificationDDL;
        public static Specification selectedSpec;
        public string selectedSpecRep;

        //The actual names are assigned in the Specification() Constructor at the moment
        Specification defaultSpec = new Specification("None Selected", 
            new string[] { }, 
            new string[] { }, 
            0.00f);
        Specification specAA_B = new Specification("A + A = B",
            new string[] { "A", "A" },
            new string[] { "B"}, 
            3.00f);
        Specification specAB_C = new Specification("A + B = C",
            new string[] { "A", "B" },
            new string[] { "C"}, 
            5.00f);
        Specification specBB_D = new Specification("B + B = D",
            new string[] { "B", "B" },
            new string[] { "D"}, 
            1.00f);

        // Start is called before the first frame update
        void Awake()
        {
            GenerateSpecifications();
            PopulateSpecificationDDL();
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            selectedSpecRep = selectedSpec.recipe; //Both this and the variable are just for testing

            UpdateDDLVisuals(specificationDDL);
        }

        void UpdateDDLVisuals(TMP_Dropdown ddl)
        {
            Toggle[] toggles = ddl.GetComponentsInChildren<Toggle>();
            bool updateToggles = false;
            foreach (Toggle toggle in toggles)
            {
                foreach (Specification spec in specifications)
                {
                    if (toggle.name.Contains(spec.recipe))
                    {
                        if (Generation.CheckProductionRequirements(spec) == false)
                        {
                            if (toggle.interactable)
                            {
                                toggle.interactable = false;
                                updateToggles = true;
                            }
                        }
                        else
                        {

                            toggle.interactable = true;
                        }
                    }

                }
            }

            /* Has a game breaking flicker delay
             * Link has a solution to avoid flicker: https://stackoverflow.com/questions/55516877/unity-dynamically-update-dropdown-list-when-opened-while-not-losing-focus-on-in
             * 
            if(updateToggles)
            {
                ddl.enabled = false;
                ddl.enabled = true;
                ddl.Show();
            }
            */

        }

        void GenerateSpecifications()
        {
            specifications.Add(defaultSpec);
            specifications.Add(specAA_B);
            specifications.Add(specAB_C);
            specifications.Add(specBB_D);
        }

        void PopulateSpecificationDDL()
        {
            specificationDDL.ClearOptions();
            List<string> options = new List<string>();
            foreach (Specification specification in specifications)
            {
                if (selectedSpec is null)
                {
                    selectedSpec = specification;
                }
                options.Add(specification.recipe);
            }
            specificationDDL.AddOptions(options);
            specificationDDL.RefreshShownValue();
        }

        public void ChangeSpecification(TMP_Dropdown specDDL)
        {
            foreach (Specification specification in specifications)
            {
                if (specification.recipe.Equals(specDDL.options[specDDL.value].text))
                {
                    selectedSpec = specification;
                    Generation.UpdateDuration(specification.duration);
                }
            }
        }
    }

    public class Specification
    {
        public string recipe;
        public float duration;
        string[] required;
        string[] product;

        public Specification(string recipe, string[] required, string[] product, float duration)
        {
            //This is temperary until I assign actual names to specifications
            if (required.Length < 1)
            {
                this.recipe = "No Recipe";
            }
            else
            {
                foreach (string resource in required)
                {
                    if (string.IsNullOrEmpty(this.recipe))
                    {
                        this.recipe += $"{resource}";
                    }
                    else
                    {
                        this.recipe += $" + {resource}";
                    }
                }
                foreach (string resource in product)
                {
                    if (this.recipe.Contains("=") == false)
                    {
                        this.recipe += $" = {resource}";
                    }
                    else
                    {
                        this.recipe += $", {resource}";
                    }
                }
            }

            this.required = required;
            this.product = product;
            this.duration = duration;
        }

        public string[] GetRequiredResources()
        {
            return required;
        }
        public string[] GetProducedResources()
        {
            foreach(string resource in required)
            {
                if (resource.Contains("A"))
                {
                    int randRare = Random.Range(0, 100);
                    if (randRare < 1)
                    {
                        return new string[] { "E" };
                    }
                }
            }
            return product;
        }
    }
}