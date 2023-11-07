using AutoMapper;
using Hotels.Infrastructure.Models;
using Hotels.Model;
using Hotels.Modules.Interface;
using Hotels.Modules.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Modules.Controller
{
    [Route("Api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            this._roomRepository = roomRepository;
            this._mapper = mapper;
            this._response = new();
        }
        [HttpGet("GetAllRoom")]
        [ProducesResponseType(typeof(List<RoomDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllRoom()
        {
            try
            {
                IEnumerable<Room> listRoom = await _roomRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<RoomDto>>(listRoom);
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
        [HttpGet("GetRoomById/{id}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRoomById(string id)
        {
            try
            {
                var room = await _roomRepository.GetAsync(u => u.Id == id);
                if (room == null)
                {
                    return BadRequest();
                }
                _response.Result = _mapper.Map<RoomDto>(room);
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
        [HttpPost("CreateRoom")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> CreateRoom([FromBody] RoomDto roomDto)
        {
            try
            {
                if ((await _roomRepository.GetAsync(u => u.Id == roomDto.Id) != null) || roomDto == null)
                {
                    ModelState.AddModelError("Custom model", "Hotel already exists");
                    return BadRequest(ModelState);
                }
                roomDto.Id = Guid.NewGuid().ToString();
                Room room = _mapper.Map<Room>(roomDto);
                await _roomRepository.CreateAsync(room);
                _response.Result = _mapper.Map<RoomDto>(roomDto);
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
        [HttpPut("UpdateRoom/{id}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdateHotel(string id, [FromBody] RoomDto staffDto)
        {
            try
            {
                if (staffDto == null || id != staffDto.Id)
                {
                    return BadRequest();
                }
                Room model = _mapper.Map<Room>(staffDto);
                await _roomRepository.UpdateAsync(model);
                _response.Result = _mapper.Map<RoomDto>(model);
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
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> DeleteHotel(string id)
        {
            try
            {
                var model = await _roomRepository.GetAsync(u => u.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                await _roomRepository.RemoveAsync(model);
                _response.Result = _mapper.Map<RoomDto>(model);
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