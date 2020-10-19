using System.Collections.Generic;
using System.Threading.Tasks;
using wstkp.Models.Resources;
using Microsoft.AspNetCore.Identity;

namespace wstkp.Models.User
{
    public interface IUserRepository
    {
        Task<Profile> GetProfile(string email);
        Task<Weight> GetLastWeightAsync(string email);
        Task<List<Weight>> GetWeights(string email);
        Task<Settings> GetSettings(string email);
        Task<string> GetUserLanguage(string email);
        Task<MyMessage> GetMessage(string code, string language);
        Task<List<MyMessage>> GetMessagesAsync();
        Task<List<IdentityUser<int>>> GetUsersAsync(string email, string name, string lastName);
        Task<IdentityUser<int>> GetUserAsync(string email);
    }
}