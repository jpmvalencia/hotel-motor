using HotelMotorApi.Interfaces;
using AutoMapper;
using HotelMotorShared.DTOs;

namespace HotelMotorApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }
        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return null;

            return _mapper.Map<CustomerDTO>(customer);
        }
        public Task<CustomerDTO> CreateAsync(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDTO> UpdateAsync(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }
    }
}
