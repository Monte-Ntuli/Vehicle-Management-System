using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.VehicleMakeDTO;
using BlazorApp1.Shared.VehicleModelTypeDTO;
using BlazorApp1.Shared.VehiclesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehicleMakeController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public VehicleMakeController(IUnitOfWork unitOfWork, ILogger<VehicleMakeController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create Vehicle Model Type
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleMakeDTO vehicleModelType)
        {
            await _unitOfWork.VehicleMake.AddAsync(_mapper.Map<VehicleMakeEntity>(vehicleModelType));
            return Accepted(vehicleModelType);

        }
        #endregion
        #region Restore Vehicle Model Type
        [HttpPost("Restore/{vehicleID}")]
        public async Task<IActionResult> Restore(int vehicleID)
        {

            await _unitOfWork.VehicleMake.RestoreVehicleMakeAsync(vehicleID);
            return Accepted(vehicleID);

        }
        #endregion

        #region Delete Vehicle Model Type
        [HttpPost("Delete/{vehicleID}")]
        public async Task<IActionResult> Delete(int vehicleID)
        {

            await _unitOfWork.VehicleMake.DeleteVehicleMakeAsync(vehicleID);
            return Accepted(vehicleID);

        }
        #endregion

        #region Update Vehicle Model Type
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDTO vehicle)
        {

            await _unitOfWork.VehicleMake.UpdateAsync(_mapper.Map<VehicleMakeEntity>(vehicle));
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicle By ID
        [HttpGet("GetVehicleByID/{vehicleID}")]
        public async Task<IActionResult> GetVehicleByID(int vehicleID)
        {

            var vehicle = await _unitOfWork.VehicleMake.GetVehicleMakeByIDAsync(vehicleID);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicle By Company
        [HttpGet("GetVehicleModelByCompany/{Company}")]
        public async Task<IActionResult> GetVehicleByCompany(string Company)
        {
            var vehicle = await _unitOfWork.VehicleMake.GetVehicleMakeByCompanyAsync(Company);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicle by Company Test
        [HttpGet("GetVehicleByCompanyTest")]
        public async Task<IActionResult> GetVehicleByCompanyTest()
        {
            var user = await _unitOfWork.VehicleMake.GetVehicleByCompanyTestAsync();
            if (user == null)
            {
                return BadRequest(user);
            }
            return Accepted(user);
        }
        #endregion
    }
}
