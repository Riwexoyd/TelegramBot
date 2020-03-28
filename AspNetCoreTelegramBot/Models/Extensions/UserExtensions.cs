using System.Text;

namespace AspNetCoreTelegramBot.Models.Extensions
{
    /// <summary>
    /// Расширения для пользователей
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Получить полное имя пользователя с Username
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Полное имя</returns>
        public static string GetFullName(this User user)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(user.FirstName))
            {
                stringBuilder.Append(user.FirstName);
            }

            if (!string.IsNullOrEmpty(user.LastName))
            {
                stringBuilder.Append($" {user.LastName}");
            }

            if (!string.IsNullOrEmpty(user.Username))
            {
                stringBuilder.Append($" (@{user.Username})");
            }

            return stringBuilder.ToString();
        }
    }
}