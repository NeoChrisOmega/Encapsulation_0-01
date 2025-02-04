using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attempt1
{
    public class Encapsulation : MonoBehaviour
    {
        public Specification specification;
        public float chargeTime;
        bool isCharging;
        [SerializeField]
        float totalDuration;
        [SerializeField]
        GameObject[] storage;
        [SerializeField] //Remove this once visuals are added
        GameObject[] ports;

        // Start is called before the first frame update
        void Start()
        {
            if (specification != null)
            {
                SetSpecs(specification);
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckDuration();
        }

        public void SetSpecs(Specification spec)
        {
            specification = spec;
            Debug.Log(spec.StorageSize());
            storage = new GameObject[spec.StorageSize()];
            totalDuration = spec.craftTime;
        }

        void CheckDuration()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isCharging = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isCharging = false;
                chargeTime = 0;
            }
            if (isCharging)
            {
                chargeTime += Time.deltaTime;
            }
            if (chargeTime >= totalDuration)
            {
                Debug.Log("Charge complete");
                chargeTime = 0;
            }
        }
    }
}

