using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp.Service
{
    internal class LoginServerCommunicationService
    {
        public async Task<bool> LoginAsync(string pEmail, string pPwd)
        {
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
    }
}
