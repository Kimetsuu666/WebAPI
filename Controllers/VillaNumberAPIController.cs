using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Dto;
using WebAPI.Repository.IRepository;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VillaNumberAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IVillaNumberRepository _dbVillaNumber;
    private readonly IMapper _mapper;

    public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper)
    {
        _dbVillaNumber = dbVillaNumber;
        _mapper = mapper;
        _response = new APIResponse();
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpGet("{id:int}", Name ="GetVillaNumber")]
    [ProducesResponseType(200, Type = typeof(VillaNumberDTO))]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
    {
        try {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody]VillaNumberCreateDTO createDto)
    {
        try
        {
            if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDto.VillaNo) != null)
            {
                ModelState.AddModelError("CustomError", "Villa number already Exists!");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest();
            }

            VillaNumber newModel = _mapper.Map<VillaNumber>(createDto);

            await _dbVillaNumber.CreateAsync(newModel);

            _response.Result = _mapper.Map<VillaNumberDTO>(newModel);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetVillaNumber", new { id = newModel.VillaNo }, _response);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
    {
        try {
            if (id == 0)
            {
                return BadRequest();
            }

            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

            if (villaNumber == null)
            {
                return NotFound();
            }

            await _dbVillaNumber.RemoveAsync(villaNumber);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.isSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }

    [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody]VillaNumberUpdateDTO updateDto)
    {
        try {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                return BadRequest();
            }

            VillaNumber newModel = _mapper.Map<VillaNumber>(updateDto);

            await _dbVillaNumber.UpdateAsync(newModel);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.isSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _response;
    }
}