using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGenerate : MonoBehaviour
{
    [SerializeField] public Transform[] stationsOrCustomers;
    [SerializeField] private GameObject[] customer;
    [SerializeField] private GameObject[] station;
    public bool[] availableT;
    public byte[] availableM;
    public int customerPositionNumber;
    public int stationPositionNumber;

    private void Start()
    {
        availableM = new byte[customer.Length];
        availableT = new bool[stationsOrCustomers.Length];
        availableM = Enumerable.Repeat((byte)1, customer.Length).ToArray();
        availableT = Enumerable.Repeat(true, stationsOrCustomers.Length).ToArray();
    }
    private void Update()
    {
        Generator();
    }
    private void Generator()
    {
        for (int i = 0; i < availableM.Length; i++)
        {
            if (availableM[i] == 1)
            {
                customerPositionNumber = (int)Random.Range(0, stationsOrCustomers.Length);
                if (availableT[customerPositionNumber])
                {
                    Instantiate(customer[i],
                    stationsOrCustomers[customerPositionNumber].position,
                    stationsOrCustomers[customerPositionNumber].rotation);
                    availableT[customerPositionNumber] = false;
                    availableM[i] = 2;
                }
            }
            else if(availableM[i] == 3)
            {
                customerPositionNumber = (int)Random.Range(0, stationsOrCustomers.Length);
                if (availableT[customerPositionNumber])
                {
                    Instantiate(station[i],
                    stationsOrCustomers[customerPositionNumber].position,
                    stationsOrCustomers[customerPositionNumber].rotation);
                    availableT[customerPositionNumber] = false;
                    availableT[stationPositionNumber] = true;
                    availableM[i] = 2;
                }
            }
        }

    }
}
