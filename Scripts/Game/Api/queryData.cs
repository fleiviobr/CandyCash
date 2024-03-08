using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using FullSerializer;
using UnityEngine;
using UnityEngine.Networking;


public class UserDataQueryService : MonoBehaviour
{
    public async Task<string> QueryData(string idUser, string tokenUser, string tokenSystem, string url)
    {
        try
        {
            var arrays = new
            {
                userId = idUser,
                tokenId = tokenUser,
                tokenSystem,
                typeId = 1
            };
            
            var json = JsonUtility.ToJson(arrays);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenSystem);
                var response = await client.PostAsync($"https://{url}/rest_api_process", content);
                var responseString = await response.Content.ReadAsStringAsync();
                Debug.Log(responseString);
                return responseString;
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error querying user data: {ex.Message}");
            throw;
        }
    }
}
