using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hotels.Modules.Controller
{
    [Route("Api/Guest")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IGuestRepository _GuestRepository;

        public GuestController(IGuestRepository GuestRepository, IMapper mapper)
        {
            this._GuestRepository = GuestRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllGuest")]
        public async Task<ActionResult<APIResponse>> GetAllGuest()
        {
            try
            {
                IEnumerable<Guest> listGuest = await _GuestRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<GuestDto>>(listGuest);
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
        [HttpGet("GetAllGuest/{id}")]
        public async Task<ActionResult<APIResponse>> GetGuestById(string id)
        {
            try
            {
                var model = await _GuestRepository.GetAsync(u=> u.Id == id);
                if(model ==null){
                    return BadRequest();
                }
                _response.Result = _mapper.Map<GuestDto>(model);
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
        [HttpPost("CreateGuest")]
        public async Task<ActionResult<APIResponse>> CreateGuest(GuestDto GuestDto)
        {
            try
            {
                if (await _GuestRepository.GetAsync(u => u.Id == GuestDto.Id) != null || GuestDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                GuestDto.Id = Guid.NewGuid().ToString();
                Guest model = _mapper.Map<Guest>(GuestDto);
                await _GuestRepository.CreateAsync(model);
                _response.Result = _mapper.Map<GuestDto>(model);
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
        [HttpPut("UpdateGuest/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] GuestDto GuestDto)
        {
            try
            {
                if (GuestDto == null || id != GuestDto.Id)
                {
                    return BadRequest();
                }
                Guest model = _mapper.Map<Guest>(GuestDto);
                await _GuestRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<GuestDto>(model);
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
        [HttpDelete("DeleteGuest/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _GuestRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _GuestRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<GuestDto>(model);
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