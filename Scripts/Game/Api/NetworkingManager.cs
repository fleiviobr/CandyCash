using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using WebSocketSharp;
using UnityEngine.Networking;
using System.Collections;

public class NetWorkingManager : MonoBehaviour
{
    private WebSocket webSocket;
    private UserDataQueryService userDataQueryService;
    private UserWalletUpdateService updateWallet;
    private string url;
    public string tokenIdUser;
    public string userId;
    public string nativeUrl;
    public string tokenSecret;
    void Start()
    {
        updateWallet = GameObject.Find("URLManager").GetComponent<UserWalletUpdateService>();

        // URL da página hospedada
        url = Application.absoluteURL;

        // Parâmetros que você deseja extrair
        List<string> parameters = new List<string> { "tokenId_User", "user_id", "native_url" };

        // Obtenha os valores dos parâmetros
        Dictionary<string, string> values = GetURLParameters(url, parameters);

        // // Imprima os valores dos parâmetros
        // foreach (KeyValuePair<string, string> pair in values)
        // {
        // }
        //Atribua os valores dos parâmetros às variáveis
        tokenIdUser = values["tokenId_User"];
        userId = values["user_id"];
        nativeUrl = values["native_url"];
        tokenSecret = TokenRequestGame(nativeUrl);
        Debug.Log("TokenIdUser:" + tokenIdUser);
        Debug.Log("userId:" + userId);
        Debug.Log("NativeUrl:" + nativeUrl);
        Debug.Log("TokenSecret:" + tokenSecret);
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

    public string TokenRequestGame(string url)
    {
        // Lista de tuplas de URL e token
        List<(string url, string token)> urls = new List<(string, string)>
    {
        ("www.lucreibet.com", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("lucreibet.com", "DADBF-IUFBUJNASD-WJSHDA-JKHFBL"),
        ("www.acerteibet.com", "KA5RtLdgFXrMYoOONA4sSw3gMRT0DCE5"),
        ("acerteibet.com", "KA5RtLdgFXrMYoOONA4sSw3gMRT0DCE5"),
        ("www.bet-homolog.nixenapi.com", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("www.oflappypix.online", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("oflappypix.online", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("www.oflappypix.site", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("oflappypix.site", "pTeHixx8NJ4FARbQLkM2wsM0ShZKEIiBqF"),
        ("www.flappypix.pro", "EAAKT7h2DWW2TgM3GWwTffkHTg9exP3oXRhjZN6xykr6zKVJpDThvXCvqwK5urAj"),
        ("flappypix.pro", "EAAKT7h2DWW2TgM3GWwTffkHTg9exP3oXRhjZN6xykr6zKVJpDThvXCvqwK5urAj"),
        ("dino.be7z.com", "oJHbbF6XziqOlJI6kMXYp67G1qVoyz5Yvxf1iCWMO52nfDCYUk"),
        ("drakencandy.nix.lat", "oJHbbF6XziqOlJI6kMXYp67G1qVoyz5Yvxf1iCWMO52nfDCYUk")
    };

        // Procura a URL na lista e retorna o token correspondente
        foreach ((string u, string t) in urls)
        {
            if (u == url)
            {
                return t;
            }
        }

        // Se a URL não for encontrada, retorna uma string vazia
        return string.Empty;
    }

    //return parameters
    public List<string> ReturnParameters()
    {
        List<string> parameters = new List<string> { tokenIdUser, userId, nativeUrl, tokenSecret };
        return parameters;
    }
}
