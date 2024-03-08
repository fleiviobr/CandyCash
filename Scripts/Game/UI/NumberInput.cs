using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberInputField : MonoBehaviour
{
    private WebSocket net;
    private string input;
    private TMP_InputField inputField;
    private int saldoCount;
    private int aposta;

    private void Awake()
    {
        net = GameObject.Find("URLManager").GetComponent<WebSocket>();
        inputField = GetComponent<TMP_InputField>();
        saldoCount = PlayerPrefs.GetInt("num_coins");
    }

    public void EndedInput(string str)
    {
        if(str.Length > 0 )aposta = int.Parse(str);
        PlayerPrefs.SetInt("apostaAtual", aposta);
    }

    public void SetInput(string s)
    {
        input = s;

        if ((input.Length > 0 && !char.IsDigit(input[input.Length - 1])) || input.Length > 8)
        {
            inputField.text = input.Remove(input.Length - 1);
        }
        else
        {
            WalletMinusInput(s);
        }
    }

    private void WalletMinusInput(string str)
    {
        if (str.Length > 0)
        {
            int amount = int.Parse(str);
            if (amount > saldoCount)
            {
                inputField.text = saldoCount.ToString();
            }
        }
    }

    void Update(){
        saldoCount = (int)(net.saldo);
    }
}
