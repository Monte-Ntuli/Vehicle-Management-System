using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.VehicleTypeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehicleTypeController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public VehicleTypeController(IUnitOfWork unitOfWork, ILogger<VehicleTypeController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }
        #region Create new VehicleType
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleTypeDTO vehicleType)
        {
            await _unitOfWork.VehicleType.AddAsync(_mapper.Map<VehicleTypeEntity>(vehicleType));
            return Accepted(vehicleType);

        }
        #endregion

        #region Update VehicleType by id
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleTypeDTO vehicleType)
        {
            await _unitOfWork.VehicleType.UpdateAsync(_mapper.Map<VehicleTypeEntity>(vehicleType));
            return Accepted(vehicleType);

        }
        #endregion

        #region Delete VehicleType by id
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int vehicleType)
        {

            await _unitOfWork.VehicleType.DeleteVehicleTypeAsync(vehicleType);
            return Accepted(vehicleType);

        }
        #endregion

        #region Restore VehicleType by id
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int vehicleType)
        {
            await _unitOfWork.VehicleType.DeleteVehicleTypeAsync(vehicleType);
            return Accepted(vehicleType);
        }
        #endregion

        #region Get VehicleType by ID
        [HttpGet("GetVehicleTypeByID/{id}")]
        public async Task<IActionResult> GetVehicleTypeByID(int vehicleTypeID)
        {
            var vehicleType = await _unitOfWork.VehicleType.GetVehicleByIDAsync(vehicleTypeID);
            if (vehicleType == null)
            {
                return BadRequest(vehicleType);
            }
            return Accepted(vehicleType);

        }
        #endregion

        #region Get VehicleType by Company
        [HttpGet("GetVehicleTypeByCompany/{company}")]
        public async Task<IActionResult> GetVehicleTypeByCompany(string company)
        {
            var vehicleType = await _unitOfWork.VehicleType.GetVehicleTypeByCompanyAsync(company);
            if (vehicleType == null)
            {
                return BadRequest(vehicleType);
            }
            return Accepted(vehicleType);

        }
        #endregion

        #region Get VehicleType by Company Test
        [HttpGet("GetVehicleTypeByCompanyTest")]
        public async Task<IActionResult> GetVehicleTypeByCompanyTest()
        {
            var user = await _unitOfWork.VehicleType.GetVehicleTypeByCompanyTestAsync();
            if (user == null)
            {
                return BadRequest(user);
            }
            return Accepted(user);
        }
        #endregion
    }
}
