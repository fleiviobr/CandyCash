using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeWallet : MonoBehaviour
{
    public Text saldo;
    private int saldoCount;
    private WebSocket net;

    void Awake()
    {
        net = GameObject.Find("URLManager").GetComponent<WebSocket>();
    }

    void Update()
    {
        saldo.text = net.saldo.ToString();
    }
    // Start is called before the first frame update

}
