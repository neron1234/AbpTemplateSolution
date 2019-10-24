using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ProjectWebApi.Core.Domain.Entities.Dictionaries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectWebApi.Core.Dto.Entities
{
    /// <summary>
    /// Some entity
    /// </summary>
    [AutoMapFrom(typeof(TestModelEntity))]
    public class TestModelDto : EntityDto
    {
        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }
    }
}
