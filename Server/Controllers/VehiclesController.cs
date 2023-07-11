using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.VehiclesDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehiclesController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public VehiclesController(IUnitOfWork unitOfWork, ILogger<VehiclesController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create Vehicle
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDTO vehicle)
        {
            var NewVehicle = await _unitOfWork.Vehicle.AddAsync(_mapper.Map<VehicleEntity>(vehicle));
            if (NewVehicle != null)
            {
                return Accepted(vehicle);
            }
            else return BadRequest();

        }
        #endregion

        #region Restore Vehicle
        [HttpPost("Restore/{vehicleID}")]
        public async Task<IActionResult> Restore(int vehicleID)
        {

            await _unitOfWork.Vehicle.RestoreVehicleAsync(vehicleID);
            return Accepted(vehicleID);

        }
        #endregion

        #region Delete Vehicle
        [HttpPost("Delete/{vehicleID}")]
        public async Task<IActionResult> Create(int vehicleID)
        {

            await _unitOfWork.Vehicle.DeleteVehicleAsync(vehicleID);
            return Accepted(vehicleID);

        }
        #endregion

        #region Update
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDTO vehicle)
        {

            await _unitOfWork.Vehicle.UpdateAsync(_mapper.Map<VehicleEntity>(vehicle));
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicle By ID
        [HttpGet("GetVehicleByID/{vehicleID}")]
        public async Task<IActionResult> GetVehicleByID(int vehicleID)
        {

            var vehicle = await _unitOfWork.Vehicle.GetVehicleByIDAsync(vehicleID);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicl By Reg
        [HttpGet("GetVehicleByReg/{Registration}")]
        public async Task<IActionResult> GetVehicleByReg(string Registration)
        {
            var vehicle = await _unitOfWork.Vehicle.GetVehicleByRegAsync(Registration);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);
        }
        #endregion

        #region Get Vehicle By Company
        [HttpGet("GetVehicleByCompany/{Company}")]
        public async Task<IActionResult> GetVehicleByCompany(string Company)
        {
            var vehicle = await _unitOfWork.Vehicle.GetVehicleByCompanyAsync(Company);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);

        }
        #endregion

        #region Get Vehicle By Type
        [HttpGet("GetVehicleByType/{vehicleTypeID}")]
        public async Task<IActionResult> GetVehicleByType(int vehicleTypeID)
        {
            var vehicle = await _unitOfWork.Vehicle.GetVehicleByTypeAsync(vehicleTypeID);
            if (vehicle == null)
            {
                return BadRequest(vehicle);
            }
            return Accepted(vehicle);

        }
        #endregion

        
    }
}
