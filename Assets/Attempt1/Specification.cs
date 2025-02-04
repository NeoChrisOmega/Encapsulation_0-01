using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Attempt1
{
    public class Specification : MonoBehaviour
    {
        [SerializeField]
        int[] storageIndexes;
        [SerializeField]
        bool[] areInputs;
        [SerializeField]
        string[] resourceTags;
        [SerializeField]
        int[] packageSizes;
        [SerializeField]
        float[] transportationSpeeds;

        public Port[] Ports { get; set; }
        int storageSize = 2;
        public float craftTime;


        public int StorageSize()
        {
            Debug.Log("Before Return");
            return 3;// storageSize;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        void OnBeforeSceneLoadRuntimeMethod()
        {
            Debug.Log("Before scene loaded");
            storageSize = storageIndexes.Max();
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int counter = 0; counter <= Ports.Length; counter++)
            {
                Ports[counter] = new Port(storageIndexes[counter], areInputs[counter], resourceTags[counter], packageSizes[counter], transportationSpeeds[counter]);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(storageSize);
        }

    }

    public class Port
    {
        public int storageIndex;
        public bool isInput;
        public string resourceTags;
        public int packageSize;
        public float transportationSpeed;

        public Port(int storageIndex, bool isInput, string resourceTags, int packageSize, float transportationSpeed)
        {
            this.storageIndex = storageIndex;
            this.isInput = isInput;
            this.resourceTags = resourceTags;
            this.packageSize = packageSize;
            this.transportationSpeed = transportationSpeed;
        }
    }

}
