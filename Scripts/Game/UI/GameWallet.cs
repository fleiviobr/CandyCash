using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWallet : MonoBehaviour
{
    public Text saldo;
    private string saldoText;
    private int saldoCount;

    void Awake()
    {
        saldoCount = PlayerPrefs.GetInt("apostaAtual");
        saldoText = saldoCount.ToString();
    }

    public void Start()
    {
        saldo.text = "Valor Apostado: " + saldoText;
    }
}
