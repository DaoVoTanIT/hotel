using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Model;
using Microsoft.AspNetCore.Mvc;
namespace Hotels.Modules.Controller
{
    [Route("Api/Staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository, IMapper mapper)
        {
            this._staffRepository = staffRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllStaff")]
        public async Task<ActionResult<APIResponse>> GetAllStaff()
        {
            try
            {
                IEnumerable<Staff> listStaff = await _staffRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<StaffDto>>(listStaff);
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
        [HttpGet("GetAllStaff/{id}")]
        public async Task<ActionResult<APIResponse>> GetStaffById(string id)
        {
            try
            {
                var model = await _staffRepository.GetAsync(u=> u.Id == id);
                if(model ==null){
                    return BadRequest();
                }
                _response.Result = _mapper.Map<StaffDto>(model);
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
        [HttpPost("CreateStaff")]
        public async Task<ActionResult<APIResponse>> CreateStaff(StaffDto staffDto)
        {
            try
            {
                if (await _staffRepository.GetAsync(u => u.Id == staffDto.Id) != null || staffDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                staffDto.Id = Guid.NewGuid().ToString();
                Staff model = _mapper.Map<Staff>(staffDto);
                await _staffRepository.CreateAsync(model);
                _response.Result = _mapper.Map<StaffDto>(model);
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
        [HttpPut("UpdateStaff/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] StaffDto staffDto)
        {
            try
            {
                if (staffDto == null || id != staffDto.Id)
                {
                    return BadRequest();
                }
                Staff model = _mapper.Map<Staff>(staffDto);
                await _staffRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<StaffDto>(model);
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
        [HttpDelete("DeleteStaff/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _staffRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _staffRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<StaffDto>(model);
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