using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace TglCA.Utils
{
    public static class Base64EncodeHelper
    {
        public static string Base64Encode(string input)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(input));
        }

        public static string Base64Decode(string input)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input));
        }
    }
}
