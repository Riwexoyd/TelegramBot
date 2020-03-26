using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTelegramBot.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан код")]
        [DataType(DataType.Text)]
        public string Code { get; set; }
    }
}