using GigHub.Core.Repositories;

namespace GigHub.Core {
    public interface IUnitOfWork {
        IAttendanceRepository Attendances { get; }
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IFollwingRepository Followings { get; }
        IUserRepository Users { get; }
        void Complete();
        void Dispose();
    }
}