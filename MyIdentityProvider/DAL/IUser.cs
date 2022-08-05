using MyIdentityProvider.DTO;

namespace MyIdentityProvider.DAL
{
    public interface IUser
    {
        Task Registration(CreateUserDto user);
        Task<UserDto> Authenticate(string username, string password);
        Task<IEnumerable<UserDto>> GetAllUser();
    }
}
