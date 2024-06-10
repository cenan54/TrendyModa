using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TrendyModa.Models;

namespace TrendyModa.Repositories
{
    public class HomeRepository
    {
        private readonly TrendyModaDbContext _context;

        public HomeRepository(TrendyModaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Photo>> GetFollowedUsersPostsAsync(int userId)
        {
            var followedUserIds = await _context.Follows
                .Where(f => f.FallowerId == userId)
                .Select(f => f.FalloweeId)
                .ToListAsync();

            var posts = await _context.Photos
                .Where(p => followedUserIds.Contains(p.UserId))
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.User)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ToListAsync();

            return posts;
        }
    }
}
