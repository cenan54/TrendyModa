using TrendyModa.Models;

namespace TrendyModa.Repositories
{
    public class FollowRepository
    {
        private readonly TrendyModaDbContext _context;

        public FollowRepository(TrendyModaDbContext context)
        {
            _context = context;
        }

        public List<User> GetFollowers(int userId)
        {
            var followers = _context.Follows
                .Where(f => f.FalloweeId == userId)
                .Select(f => f.FallowerId)
                .ToList();

            return _context.Users
                .Where(u => followers.Contains(u.UserId))
                .ToList();
        }

        public List<User> GetFollowings(int userId)
        {
            var followings = _context.Follows
                .Where(f => f.FallowerId == userId)
                .Select(f => f.FalloweeId)
                .ToList();

            return _context.Users
                .Where(u => followings.Contains(u.UserId))
                .ToList();
        }
    }
}
