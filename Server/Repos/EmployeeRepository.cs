using BlazorApp1.Client.Repos.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlazorApp1.Client.Repos
{
    public class EmployeeRepository : Repository<EmployeeEntity>, IEmployeeRepository
    {
        private VehicleDbContext _dbContext => (VehicleDbContext)_context;
        private readonly IConfiguration _config;

        public EmployeeRepository(VehicleDbContext context, IConfiguration config) : base(context)
        {
            _config = config;
        }
        public async Task<List<EmployeeEntity>> GetEmployeeByEmailAsync(string email)
        {
            var user = await _dbContext.Employees.Where(x => x.Email == email).ToListAsync();
            if (user == null) { return null; }
            else return user;
        }
        public async Task<EmployeeEntity> Login(LoginUserRM userEntity)
        {
            var user = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == userEntity.Email);
            if (user != null)
            {
                var getUserPasswordHash = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == userEntity.Email);
                var getUserPasswordSalt = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == userEntity.Email);

                if (getUserPasswordHash != null && getUserPasswordSalt != null)
                {
                    var userPasswordHash = getUserPasswordHash.PasswordHash;
                    var userPasswordSalt = getUserPasswordSalt.PasswordSalt;

                    if (VerifyPasswordHash(userEntity.Password, userPasswordHash, userPasswordSalt))
                    {
                        string token = CreateToken(user);
                        return user;
                    }
                }
                return user;
            }
            return user;
        }
       
        private string CreateToken(EmployeeEntity entity)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("JwtConfig:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        
        public async Task<List<EmployeeEntity>> GetEmployeeByRoleAsync(string role)
        {
            var user = await _dbContext.Employees.Where(x => x.Role == role).ToListAsync();
            if (user == null) { return null; }
            else { return user; }
        }
        public async Task<List<EmployeeEntity>> GetEmployeeByCompanyAsync(string company)
        {
            var user = await _dbContext.Employees.Where(x => x.Company == company).ToListAsync();
            if (user == null) { return null; }
            else { return user; }
        }
        public async Task<bool> RestoreEmployeeAsync(int employee)
        {
            var user = await _dbContext.Employees.FirstOrDefaultAsync(x => x.UserID == employee);
            if (user != null)
            {
                user.isDeleted = false;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteEmployeeAsync(int employee)
        {
            var user = await _dbContext.Employees.FirstOrDefaultAsync(x => x.UserID == employee);
            if (user != null) 
            {
                user.isDeleted = true;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
            
        }
        public async Task<List<EmployeeEntity>> GetEmployeeByIDAsync(int employee)
        {
            var user = await _dbContext.Employees.Where(x => x.UserID == employee).ToListAsync();
                if(user == null) { return null; }
            else { return user; }
        }
        public async Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity)
        {
            var user = await _dbContext.Employees.FirstOrDefaultAsync(x => x.UserID == entity.UserID);

            if (user == null)  
            { 
                return null; 
            }
            
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                user.PhoneNum = entity.PhoneNum;
                user.Email = entity.Email;
                user.Role = entity.Role;
                user.UserName = entity.UserName;

                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
                return entity;
        }
        public async override Task<EmployeeEntity> AddAsync(EmployeeEntity entity)
        {
            var check = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == entity.Email);

            if (check == null)
            {
                entity.Id = Guid.NewGuid();
                entity.UserID = GenerateUserID();
                entity.isDeleted = false;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            if (check != null)
            {
                return null;
            }
            else return null;
            
        }
        private int GenerateUserID()
        {
            var highestId = _dbContext.Employees.OrderByDescending(x => x.UserID).FirstOrDefault();
            if (highestId != null)
            {
                return highestId.UserID + 1;
            }
            return 1;
        }

    }
}
