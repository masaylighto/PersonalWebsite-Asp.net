﻿using Core.DataKit;
using Core.DataKit.Result;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
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
    [Required]
    public required string Email { get; set; }
    [Required]
    public string? Password { get; protected set; }
    public UserType UserType { get; set; } = UserType.Admin;
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
            return Encoding.UTF8.GetString(sha512.ComputeHash(passwordByte));
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
        return hashResult.GetData().Equals(Password);

    }
}
