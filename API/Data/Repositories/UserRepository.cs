using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context,
            IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            // return await _context.Users
            //     .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            //     .ToListAsync();
            var query = _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName.ToLower() == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            // return await _context.Users
            //     .Include(p => p.Photos)
            //     .SingleOrDefaultAsync(x => x.UserName == username);

            var user = await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);

            if (user != null)
            {
                // Separate the main photo from the rest
                var mainPhoto = user.Photos.FirstOrDefault(p => p.IsMain);
                var remainingPhotos = user.Photos.OrderBy(p => p.Id).Where(p => !p.IsMain).ToList();

                // Combine the main photo and the remaining photos in the desired order
                user.Photos = new List<Photo>();
                if (mainPhoto != null)
                {
                    user.Photos.Add(mainPhoto);
                }
                user.Photos.AddRange(remainingPhotos);
            }

            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}