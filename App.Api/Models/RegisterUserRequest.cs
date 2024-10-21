using App.Domain.Entities;

namespace App.Api.Models
{
    public class RegisterUserRequest
    {
        public string Username
        {
            get => _username;
            set => _username = RemoveSpecialCharacters(value);
        }
        public string Email
        {
            get => _email;
            set => _email = RemoveSpecialCharacters(value);
        }
        //public string Password
        //{
        //    get => _password;
        //    set => _password = RemoveSpecialCharacters(value); // Có thể thêm mã hóa mật khẩu sau khi loại bỏ ký tự đặc biệt
        //}

        public string EmployeeId { get; set; }
        public int? Role { get; set; }
        public string? PhoneNumber { get; set; } // Có thể null
        public string? FullName { get; set; } // Có thể null
        public string? Address { get; set; } // Có thể null

        public UserStatus Status { get; set; }

        // Field backing
        private string _username;
        private string _email;
        //private string _password;

        // Hàm loại bỏ ký tự đặc biệt
        private string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Chỉ giữ lại các ký tự chữ cái, số và các dấu phân cách tiêu chuẩn
            //return System.Text.RegularExpressions.Regex.Replace(input, @"[^a-zA-Z0-9@_.\p{IsHangul}\p{IsHiragana}\p{IsKatakana}\p{IsHan}\p{IsLatin}À-ỹ]", "");
            //\uAC00 - \uD7A3: Đây là dải mã Unicode cho Hangul.
            //\u3040-\u309F: Đây là dải mã Unicode cho Hiragana.
            //\u30A0-\u30FF: Đây là dải mã Unicode cho Katakana.
            //\u4E00 -\u9FFF: Đây là dải mã Unicode cho các ký tự chữ Hán(CJK Unified Ideographs).
            //À - ỹ: Đây là dải ký tự cho các ký tự Latin có dấu.
            return System.Text.RegularExpressions.Regex.Replace(input, @"[^a-zA-Z0-9@_.\uAC00-\uD7A3\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFFÀ-ỹ]", "");
        }
    }
}
