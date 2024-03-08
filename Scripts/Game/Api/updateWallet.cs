using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class UserWalletUpdateService : MonoBehaviour
{
    public async Task UpdateWallet(decimal value, string typeWallet, string idUser, string tokenUser, string tokenSystem, string nameGame, string status, string url)
    {
        try
        {
            var valueFormattedBRL = FormattedIntValue(value); // Formata o número para inteiro. Ex: 10.90 --> 1090.

            var arrays = new
            {
                userId = idUser, // Id do usuário.
                tokenId = tokenUser, // Token do usuário.
                tokenSystem, // Token do sistema.
                typeId = 3, // Type id do serviço.
                typeWallet, // Add => adicionar valores || remove => remover valores.
                Value = valueFormattedBRL,
                Value_type = "balance", // Balance => saldo real || bonus => saldo do bônus.
                Service_name = nameGame, // Nome do serviço.
                Service_type = status // Win => venceu || loser => perdeu || stats => iniciou a "partida".
            };

            using (var client = new HttpClient())
            {
                var jsonContent = JsonUtility.ToJson(arrays);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://{url}/wallets", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Aposta realizada com sucesso!");
                }
                else
                {
                    throw new Exception($"Failed to update user wallet. Status code: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user wallet: {ex.Message}");
            throw;
        }
    }

    // Converte valor para inteiro:
    private int FormattedIntValue(decimal value)
    {
        var valueFormattedInt = (int)(value * 100);
        return valueFormattedInt;
    }
}
