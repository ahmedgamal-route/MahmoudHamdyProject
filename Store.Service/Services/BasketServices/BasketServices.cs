using AutoMapper;
using Store.Repository.Basket.Models;
using Store.Repository.Basket;
using Store.Service.Services.BasketServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
         => await _basketRepository.DeleteBasketAsync(BasketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null)
                return new CustomerBasketDto();
            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id = GenerateRundomBasketId();
            var customerBasket = _mapper.Map<CustomerBasket>(input);
            var updateBasaket = await _basketRepository.UpdateBasketAsync(customerBasket);
            var mappedUpdateBasket = _mapper.Map<CustomerBasketDto>(updateBasaket);
            return mappedUpdateBasket;
        }
        private string GenerateRundomBasketId()
        {
            Random random = new Random();
            int randomDigits = random.Next(1000, 10000);
            return $"BS-{randomDigits}";
        }
    }
}
