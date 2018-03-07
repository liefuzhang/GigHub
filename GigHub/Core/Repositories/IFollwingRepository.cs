using GigHub.Core.Models;

namespace GigHub.Core.Repositories {
    public interface IFollwingRepository {
        Following GetFollowing(string userId, string artistId);
        void Add(Following follow);
        void Remove(Following following);
    }
}