using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Model;
using Microsoft.AspNetCore.Mvc;
namespace Hotels.Modules.Controller
{
    [Route("Api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _PaymentRepository;

        public PaymentController(IPaymentRepository PaymentRepository, IMapper mapper)
        {
            this._PaymentRepository = PaymentRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllPayment")]
        public async Task<ActionResult<APIResponse>> GetAllPayment()
        {
            try
            {
                IEnumerable<Payment> listPayment = await _PaymentRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<PaymentDto>>(listPayment);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpGet("GetAllPayment/{id}")]
        public async Task<ActionResult<APIResponse>> GetPaymentById(string id)
        {
            try
            {
                var model = await _PaymentRepository.GetAsync(u=> u.Id == id);
                if(model ==null){
                    return BadRequest();
                }
                _response.Result = _mapper.Map<PaymentDto>(model);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPost("CreatePayment")]
        public async Task<ActionResult<APIResponse>> CreatePayment(PaymentDto PaymentDto)
        {
            try
            {
                if (await _PaymentRepository.GetAsync(u => u.Id == PaymentDto.Id) != null || PaymentDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                PaymentDto.Id = Guid.NewGuid().ToString();
                Payment model = _mapper.Map<Payment>(PaymentDto);
                await _PaymentRepository.CreateAsync(model);
                _response.Result = _mapper.Map<PaymentDto>(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpPut("UpdatePayment/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] PaymentDto PaymentDto)
        {
            try
            {
                if (PaymentDto == null || id != PaymentDto.Id)
                {
                    return BadRequest();
                }
                Payment model = _mapper.Map<Payment>(PaymentDto);
                await _PaymentRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<PaymentDto>(model);
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){
                    ex.ToString()
                };
            }
            return _response;
        }
        [HttpDelete("DeletePayment/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _PaymentRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _PaymentRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<PaymentDto>(model);
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){
                    ex.ToString()
                };
            }
            return _response;
        }
    }
}