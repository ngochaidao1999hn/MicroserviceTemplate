using AutoMapper;
using Basket.Application.Features.Commands;
using Basket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mapper
{
    public class BasketProfile: Profile
    {
        public BasketProfile()
        {
            CreateMap<UpdateBasketCommand, ShoppingCart>().ReverseMap();
        }
    }
}
