using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class RandomGenerate : MonoBehaviour
    {
        [SerializeField] private GameObject customer;
        public int notPickedCustomers;

        private void Start()
        {
            notPickedCustomers = 0;
        }
        private void Update()
        {
            if (notPickedCustomers < 3)
            {
                Instantiate(customer);
                customer.transform.position = new Vector3(Random.Range(-20, 45), 1.3f, Random.Range(45, -20));
                notPickedCustomers++;
            }
        }
    }
