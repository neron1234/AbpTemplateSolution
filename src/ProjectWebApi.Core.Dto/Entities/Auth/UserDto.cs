using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectWebApi.Core.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Dto.Entities.Auth
{
    /// <summary>
    /// User
    /// </summary>
    [AutoMapFrom(typeof(UserEntity))]
    public class UserDto : EntityDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
    }
}
