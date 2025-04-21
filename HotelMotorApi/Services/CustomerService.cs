using HotelMotorApi.Interfaces;
using AutoMapper;
using HotelMotorShared.Dtos.CustomerDTOs;
using HotelMotorShared.Models;

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
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return null;

            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<CustomerDto> CreateAsync(CustomerCreateDto customerDto)
        {
            if (await _customerRepository.EmailExistsAsync(customerDto.Email))
                throw new ApplicationException("El email ya está registrado");

            var customer = _mapper.Map<Customer>(customerDto);
            customer = await _customerRepository.CreateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto?> UpdateAsync(int id, CustomerUpdateDto customerDto)
        {
            if (!await _customerRepository.ExistsAsync(id))
                return null;

            var customerByEmail = await _customerRepository.GetByEmailAsync(customerDto.Email);
            if (customerByEmail != null && customerByEmail.Id != id)
                throw new ApplicationException("El email ya está en uso por otro cliente");

            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = id;

            customer = await _customerRepository.UpdateAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerDto?> SearchOneAsync(string searchTerm)
        {
            var customer = await _customerRepository.SearchOneAsync(searchTerm);
            if (customer == null)
            {
                return null;  // Si no se encuentra el cliente, retorna null
            }

            return _mapper.Map<CustomerDto>(customer);
        }

    }
}
