﻿namespace TheWayToGerman.Api.ResponseObject.Login;

public class AuthenticateResponse
{
    public required string JwtToken { get; set; }

    public required string RefreshToken { get; set; }
}
