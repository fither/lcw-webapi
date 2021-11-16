using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Models;
using BC = BCrypt.Net.BCrypt;

namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMailService _mailService;
        public UserRepository(DataContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }
        public async Task CreateAsync(User user)
        {
            SecureRandom random = new();
            var randomNumber = random.Next().ToString();

            var email = new MailRequest(user.EmailAddress, "Mail Confirm", randomNumber);

            await _mailService.SendEmailAsync(email);

            user.Password = BC.HashPassword(user.Password);
            user.ConfirmCode = randomNumber;
            user.Confirmed = false;

            _context.Users.Add(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id.Equals(id));
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.EmailAddress.Equals(email));
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
