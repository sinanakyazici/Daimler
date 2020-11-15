using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Daimler.Lib.Helpers
{
    public static class Helper
    {
        public static string GetCurrentUsername(this HttpContext httpContext)
        {
            var value = httpContext?.User?.FindFirst("preferred_username")?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                value = httpContext?.User?.FindFirst("client_id")?.Value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = string.Empty;
            }

            return value;
        }

        public static string GetCurrentUsername(this HubConnectionContext httpContext)
        {
            var value = httpContext?.User?.FindFirst("preferred_username")?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                value = httpContext?.User?.FindFirst("client_id")?.Value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = string.Empty;
            }

            return value;
        }

        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }


        // basit olmasi acisindan int uzerinden giderek, 6 haneli unique bir kod elde ettim.
        // long olsaydi 8 hane uretecekti. burada kod uretmek icin farkli algoritmalar kullanilabilr
        public static string GetCode(int id)
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
        }

        public static int GetId(string code)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(code));
        }

        // burada random olarak 6 karakter uretiyoruz. ayni olma ihtimali var!!!
        public static string GenerateCode(int length)
        {
            string code = string.Empty; //karater dizisini olusturuyoruz. rakamlar ve buyuk kucuk harfler
            Enumerable.Range(48, 75)
                .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
                .OrderBy(o => new Random().Next())
                .ToList()
                .ForEach(i => code += Convert.ToChar(i));

            var p = new Random().Next(0, code.Length - length);
            return code.Substring(p, length); 
        }
    }
}