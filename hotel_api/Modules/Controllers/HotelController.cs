using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Model;
using Microsoft.AspNetCore.Mvc;
namespace Hotels.Modules.Controller
{

    [Route("api/Hotel")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IHotelRepository _hotelRepository;

        public HotelAPIController(IHotelRepository hotelRepository, IMapper mapper)
        {
            this._hotelRepository = hotelRepository;
            this._mapper = mapper;
            this._response = new();
        }

        [HttpGet("GetAllHotel")]
        [ProducesResponseType(typeof(List<HotelDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllHotel()
        {
            try
            {
                IEnumerable<Hotel> listHotel = await _hotelRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<HotelDto>>(listHotel);
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
        [HttpGet("GetHotelById/{id}")]
        [ProducesResponseType(typeof(HotelDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetHotelById(string id)
        {
            try
            {
                var hotel = await _hotelRepository.GetAsync(u => u.Id == id);
                if (hotel == null)
                {
                    return BadRequest();
                }
                _response.Result = _mapper.Map<HotelDto>(hotel);
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
        [HttpPost("CreateHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> AddHotel([FromBody] HotelDto hotelDto)
        {

            try
            {
                if (await _hotelRepository.GetAsync(u => u.Id == hotelDto.Id) != null || hotelDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                hotelDto.Id = Guid.NewGuid().ToString();
                Hotel model = _mapper.Map<Hotel>(hotelDto);
                await _hotelRepository.CreateAsync(model);
                _response.Result = hotelDto;
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
        [HttpPut("UpdateHotel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] HotelDto hotelDto)
        {
            try
            {
                if (hotelDto == null || id != hotelDto.Id)
                {
                    return BadRequest();
                }
                Hotel model = _mapper.Map<Hotel>(hotelDto);
                await _hotelRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<HotelDto>(model);
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
        [HttpDelete("DeleteHotel/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _hotelRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _hotelRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<HotelDto>(model);
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