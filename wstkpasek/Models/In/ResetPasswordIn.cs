namespace wstkpasek.Models.In
{
    public class ResetPasswordIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public string ReturnUrl { get; set; }

        public bool CheckPassword()
        {
            if (Password.Length < 3) return false;
            return Password == Password2;
        }
    }
}