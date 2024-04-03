using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WpfApp.MVVM.Model;

namespace WpfApp.Service
{
    
    static class LoginServerCommunicationService
    {
        public static async Task<User?> LoginAsyncOrNull(string pEmail, string pPwd)
        {
            User? ret = new User();
            // HTTP POST 요청을 보낼 엔드포인트 URL
            string url = "http://localhost:3000/login";

            string json = JsonConvert.SerializeObject(new { email = pEmail, pwd = pPwd });
            // HttpClient 인스턴스 생성
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // HTTP POST 요청을 만들고 전송합니다.
                    HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

                    // 응답 메시지를 확인합니다.
                    if (response.IsSuccessStatusCode)
                    {
                        var rc = await response.Content.ReadAsStringAsync();
                        // 성공적으로 요청이 완료되었을 때
                        string jsonString = await response.Content.ReadAsStringAsync();
                        JObject jsonObj = JObject.Parse(jsonString);
                        ret.ID = jsonObj["id"].ToString();
                        ret.Nickname = jsonObj["nickname"].ToString();
                        ret.Name = jsonObj["name"].ToString();

                        Debug.WriteLine("서버 응답:");
                        return ret;
                    }
                    else
                    {
                        // 요청이 실패한 경우
                        Debug.WriteLine("요청 실패: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    // 오류 처리
                    Debug.WriteLine("오류 발생: " + e.Message);
                }
            }
            return null;
        }

        public static async Task<bool> SignUpAsync(User pUser)
        {
            // HTTP POST 요청을 보낼 엔드포인트 URL
            string url = "http://localhost:3000/signup";

            string json = JsonConvert.SerializeObject(new { email = pUser.Email, pwd = pUser.Password, name = pUser.Name, nickname = pUser.Nickname});
            // HttpClient 인스턴스 생성
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // HTTP POST 요청을 만들고 전송합니다.
                    HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

                    // 응답 메시지를 확인합니다.
                    if (response.IsSuccessStatusCode)
                    {
                        // 성공적으로 요청이 완료되었을 때
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine("서버 응답:");
                        Debug.WriteLine(responseBody);
                        return true;
                    }
                    else
                    {
                        // 요청이 실패한 경우
                        Debug.WriteLine("요청 실패: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    // 오류 처리
                    Debug.WriteLine("오류 발생: " + e.Message);
                }
            }
            return false;
        }

        public static async Task<User?> ReqProfile(string pId)
        {
            User? ret = new User();
            // HTTP POST 요청을 보낼 엔드포인트 URL
            string url = "http://localhost:3000/login";

            string json = JsonConvert.SerializeObject(new { _id = pId });
            // HttpClient 인스턴스 생성
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // HTTP POST 요청을 만들고 전송합니다.
                    HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

                    // 응답 메시지를 확인합니다.
                    if (response.IsSuccessStatusCode)
                    {
                        var rc = await response.Content.ReadAsStringAsync();
                        // 성공적으로 요청이 완료되었을 때
                        string jsonString = await response.Content.ReadAsStringAsync();
                        JObject jsonObj = JObject.Parse(jsonString);
                        ret.ID = jsonObj["id"].ToString();
                        ret.Nickname = jsonObj["nickname"].ToString();
                        ret.Name = jsonObj["name"].ToString();

                        Debug.WriteLine("서버 응답:");
                        return ret;
                    }
                    else
                    {
                        // 요청이 실패한 경우
                        Debug.WriteLine("요청 실패: " + response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    // 오류 처리
                    Debug.WriteLine("오류 발생: " + e.Message);
                }
            }
            return null;
        }
    }
}
