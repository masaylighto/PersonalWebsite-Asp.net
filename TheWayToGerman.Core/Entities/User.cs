using Core.DataKit;
using Core.DataKit.MockWrapper;
using Core.DataKit.Result;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using TheWayToGerman.Core.Enums;

namespace TheWayToGerman.Core.Entities;

public class User : BaseEntity
{
    public User()
    {
        
    }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Username { get; set; }
    [Required,EmailAddress]
    public required string Email { get; set; }
    [Required]
    public string Password { get; protected set; }
    public UserType UserType { get; set; } = UserType.Admin;
    public bool IsPasswordNullOrEmpty()
    {
        return Password is null || Password.Length == 0;

    }

    public Result<OK> SetPassword(string password)
    {
        if (password is null)
        {
            return new NullReferenceException("password is null");
        }
        var hashResult = Hash(password);
        if (hashResult.ContainError())
        {
            return hashResult.GetError();
        }
        Password = hashResult.GetData();
        return new OK();
    }

    private Result<string> Hash(string password)
    {
        try
        {
            //double hash with SHA512 use part of the first hash as salt for the second hash
            SHA512 sha512 = SHA512.Create();
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            byte[] hashedPasswordBytes = sha512.ComputeHash(passwordByte);
            passwordByte = passwordByte.Concat(hashedPasswordBytes.Take(3)).ToArray();
            return Convert.ToBase64String(sha512.ComputeHash(passwordByte));
        }
        catch (Exception ex)
        {
            return ex;
        }


    }
    public bool IsPasswordEqual(string password)
    {
        if (Password is null)
        {
            return false;
        }
        var hashResult = Hash(password);
        if (hashResult.ContainError())
        {
            return false;
        }
        return hashResult.GetData()==Password;

    }
    public void UpdateFrom(User user, DateTime updateDate)
    {
        bool isUserUpdated = false;
        if (user.Name is not null && Name != user.Name)
        {
           
            Name = user.Name;
            isUserUpdated = true;
        }
        if (user.Password is not null && Password != user.Password)
        {
            Password = user.Password;
            isUserUpdated = true;
        }
        if (user.Email is not null && Email != user.Email)
        {
            Email = user.Email;
            isUserUpdated = true;
        }
        if (user.Username is not null && Username != user.Username)
        {
            Username = user.Username;
            isUserUpdated = true;
        }
        if (isUserUpdated)
        {
            UpdateDate = updateDate;
        }

    }
}
