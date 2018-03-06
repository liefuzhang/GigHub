using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories {
    public interface IUserRepository {
        IEnumerable<ApplicationUser> GetFolloweesByUserId(string userId);
        IEnumerable<ApplicationUser> GetUsers();
    }
}