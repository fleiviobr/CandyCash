using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;


public class WebSocket : MonoBehaviour
{
    private DrakenResponse drakenResponse;
    private int user_id;
    private string tokenIdUser;
    private string nativeUrl;
    private int typeId;
    public decimal saldo;

    void Awake()
    {
        // URL da página hospedada
        string url = Application.absoluteURL;
        // string url = "https://candy.nix.lat/play?tokenId_User=Gwjh4MXBPakrupYRjxn3wRHnQVNyzjhV3w4GyLjn&user_id=15&native_url=drakencandy.nix.lat";

        // Parâmetros que você deseja extrair
        List<string> parameters = new List<string> { "tokenId_User", "user_id", "native_url" };

        // Obtenha os valores dos parâmetros
        Dictionary<string, string> values = GetURLParameters(url, parameters);

        //Atribua os valores dos parâmetros às variáveis
        tokenIdUser = values["tokenId_User"];
        user_id = int.Parse(values["user_id"]);
        nativeUrl = values["native_url"];
    }
    void Start()
    {
        StartCoroutine(GetRequest($"https://candy.nix.lat/users/get.php?userId={user_id}&tokenId={tokenIdUser}&typeId=1&url={nativeUrl}"));
        // WalletManager(1000, "add", "balance", "stats");
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    // Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    // Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    // Debug.Log(pages[page] + ": " + webRequest.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    // Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    //debug data
                    var datas = webRequest.downloadHandler.text;
                    drakenResponse = JsonUtility.FromJson<DrakenResponse>(datas);
                    PlayerPrefs.SetInt("num_coins", (int)saldo);
                    saldo = decimal.Parse(drakenResponse.data.user_pointer)/100;
                    break;
            }
        }
    }

    public void WalletManager(int value, string operation, string valueType, string serviceType)
    {
        StartCoroutine(GetRequest($"https://candy.nix.lat/wallets/update.php?userId={user_id}&tokenId={tokenIdUser}&typeId=3&typeWallet={operation}&Value={value}&Value_type={valueType}&url={nativeUrl}"));
        StartCoroutine(GetRequest($"https://candy.nix.lat/wallets/balance.php?userId={user_id}&tokenId={tokenIdUser}&typeId=2&typeWallet={operation}&Value={value}&Value_type={valueType}&Service_name=Candy&Service_type={serviceType}&url={nativeUrl}"));
    }

    Dictionary<string, string> GetURLParameters(string url, List<string> parameters)
    {
        // Crie um dicionário para armazenar os valores dos parâmetros
        Dictionary<string, string> values = new Dictionary<string, string>();

        // Verifique se a URL contém os parâmetros
        if (url.Contains("?"))
        {
            // Divida a URL em partes separadas pelo caractere "?"
            string[] urlParts = url.Split('?');

            // Verifique se a segunda parte da URL contém os parâmetros
            if (urlParts.Length == 2)
            {
                // Divida a segunda parte da URL em partes separadas pelo caractere "&"
                string[] parameterParts = urlParts[1].Split('&');

                // Percorra todas as partes dos parâmetros
                foreach (string part in parameterParts)
                {
                    // Divida cada parte em partes separadas pelo caractere "="
                    string[] keyValue = part.Split('=');

                    // Verifique se a chave da parte está na lista de parâmetros
                    if (keyValue.Length == 2 && parameters.Contains(keyValue[0]))
                    {
                        // Adicione o valor do parâmetro ao dicionário
                        values.Add(keyValue[0], keyValue[1]);
                    }
                }
            }
        }

        // Retorne o dicionário com os valores dos parâmetros
        return values;
    }
    void Update()
    {
    }
}

[System.Serializable]
public class DrakenResponse
{
    public bool success;
    public string message;
    public Data data;
}

[System.Serializable]
public class Data
{
    public bool success;
    public bool error;
    public string user_pointer;
    public string user_pointer_bonus;
}
