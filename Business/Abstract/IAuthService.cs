using System;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.DTOs;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Abstract;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string mail);
        IDataResult<AccessToken> CreateAccessToken(User user);


    }
}
