using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hotels.Modules.Controller
{
    [Route("Api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _BookingRepository;

        public BookingController(IBookingRepository BookingRepository, IMapper mapper)
        {
            this._BookingRepository = BookingRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllBooking")]
        public async Task<ActionResult<APIResponse>> GetAllBooking()
        {
            try
            {
                IEnumerable<Booking> listBooking = await _BookingRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<BookingDto>>(listBooking);
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
        [HttpGet("GetAllBooking/{id}")]
        public async Task<ActionResult<APIResponse>> GetBookingById(string id)
        {
            try
            {
                var model = await _BookingRepository.GetAsync(u=> u.Id == id);
                if(model ==null){
                    return BadRequest();
                }
                _response.Result = _mapper.Map<BookingDto>(model);
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
        [HttpPost("CreateBooking")]
        public async Task<ActionResult<APIResponse>> CreateBooking(BookingDto BookingDto)
        {
            try
            {
                if (await _BookingRepository.GetAsync(u => u.Id == BookingDto.Id) != null || BookingDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                BookingDto.Id = Guid.NewGuid().ToString();
                Booking model = _mapper.Map<Booking>(BookingDto);
                await _BookingRepository.CreateAsync(model);
                _response.Result = _mapper.Map<BookingDto>(model);
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
        [HttpPut("UpdateBooking/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] BookingDto BookingDto)
        {
            try
            {
                if (BookingDto == null || id != BookingDto.Id)
                {
                    return BadRequest();
                }
                Booking model = _mapper.Map<Booking>(BookingDto);
                await _BookingRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<BookingDto>(model);
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
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _BookingRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _BookingRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<BookingDto>(model);
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