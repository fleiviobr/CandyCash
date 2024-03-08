using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletSaldo : MonoBehaviour
{
    private int saldo;

    // Start is called before the first frame update
    void Awake()
    {
        saldo = PlayerPrefs.GetInt("num_coins");        
    }

    public void Start(){
        PlayerPrefs.SetInt("apostaAtual", 0);
    }

    // Update is called once per frame

    public void AddSaldo(int amount)
    {
        saldo += amount;
        PlayerPrefs.SetInt("num_coins", saldo);
    }

    public void SpendSaldo(int amount)
    {
        saldo -= amount;
        if (saldo < 0)
        {
            saldo = 0;
        }
        PlayerPrefs.SetInt("num_coins", saldo);
    }
}
