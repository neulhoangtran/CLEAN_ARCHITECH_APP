using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        // Kiểm tra token không trống và có đúng định dạng 3 phần không
        if (string.IsNullOrEmpty(jwt) || jwt.Split('.').Length != 3)
        {
            throw new SecurityTokenMalformedException("Token is not in the correct JWT format.");
        }

        try
        {
            // Loại bỏ ký tự không hợp lệ (nếu có)
            //jwt = jwt.Replace("\0", "").Trim();
            string pattern = @"[^a-zA-Z0-9-_\.]";
            string cleanedJwt = Regex.Replace(jwt, pattern, "");

            // In token ra để kiểm tra
            Console.WriteLine($"JWT Token: {cleanedJwt}");

            // Sử dụng JsonWebTokenHandler để phân tích token
            var handler = new JsonWebTokenHandler();
            var jsonWebToken = handler.ReadJsonWebToken(cleanedJwt);

            // Trả về các claims từ token
            return jsonWebToken.Claims;
        }
        catch (Exception ex)
        {
            // Log chi tiết lỗi và ném ra ngoại lệ nếu phân tích token thất bại
            Console.WriteLine($"Error parsing JWT: {ex.Message}");
            throw new SecurityTokenMalformedException("An error occurred while parsing the JWT.", ex);
        }
    }
}
