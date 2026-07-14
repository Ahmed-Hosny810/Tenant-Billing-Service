using AutoMapper;
using Pos.tenant.Application.Features.SubscriptionPlans.Commands.CreateCommand;
using Pos.tenant.Application.Features.SubscriptionPlans.DTOs;
using Pos.tenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.tenant.Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateSubscriptionPlanCommand, SubscriptionPlan>();
            CreateMap<SubscriptionPlanDto, SubscriptionPlan>().ReverseMap();
        }
    }
}
