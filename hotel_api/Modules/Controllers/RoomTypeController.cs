using AutoMapper;
using Hotels.Infrastructure.Interface;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Modules.Controller
{
    [Route("api/RoomType")]
    [ApiController]
    public class RoomTypeAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeAPIController(IRoomTypeRepository roomTypeRepository, IMapper mapper)
        {
            this._roomTypeRepository = roomTypeRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllRoomType")]
        [ProducesResponseType(typeof(List<RoomTypeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllRoomType()
        {
            try
            {
                IEnumerable<RoomType> listRoomTypes = await _roomTypeRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<RoomTypeDto>>(listRoomTypes);
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
        [HttpGet("GetRoomTypeById/{id}")]
        [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRoomTypeByID(string id)
        {
            try
            {
                var roomType = await _roomTypeRepository.GetAsync(u => u.Id == id);
                if (roomType == null)
                {
                    return BadRequest();
                }
                _response.Result = _mapper.Map<RoomTypeDto>(roomType);
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
        [HttpPost("CreateRoomTypeById")]
        public async Task<ActionResult<APIResponse>> CreateRoomType([FromBody] RoomTypeDto roomTypeDto)
        {
            try
            {
                if (await _roomTypeRepository.GetAsync(u => u.Id == roomTypeDto.Id || u.Name == roomTypeDto.Name) != null)
                {
                    ModelState.AddModelError("Custom model", "Room type already exists");
                    return BadRequest(ModelState);
                }
                roomTypeDto.Id = Guid.NewGuid().ToString();
                RoomType roomType = _mapper.Map<RoomType>(roomTypeDto);
                await _roomTypeRepository.CreateAsync(roomType);
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
        [HttpDelete("DeleteRoomType/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteRoomType(string id)
        {
            try
            {
                var roomTypeById = await _roomTypeRepository.GetAsync(u => u.Id == id);
                if (roomTypeById == null)
                {
                    return BadRequest();
                }
                RoomType roomType = _mapper.Map<RoomType>(roomTypeById);
                await _roomTypeRepository.RemoveAsync(roomType);
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
        [HttpPut("UpdateRoomType")]
        [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateRoomType(string id, [FromBody] RoomTypeDto roomTypeDto)
        {
            try
            {
                if (roomTypeDto == null || id != roomTypeDto.Id)
                {
                    return BadRequest();
                }
                RoomType model = _mapper.Map<RoomType>(roomTypeDto);

                await _roomTypeRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<RoomTypeDto>(model);
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