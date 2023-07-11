using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.QuestionaireDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionaireController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionaireController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public QuestionaireController(IUnitOfWork unitOfWork, ILogger<QuestionaireController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create new questionaire
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionaireDTO questionaire)
        {
                var create = await _unitOfWork.Questionaire.AddAsync(_mapper.Map<QuestionaireEntity>(questionaire));
                if (create != null) { return Accepted(questionaire); } else return BadRequest();
                
        }
        #endregion

        #region Update Questionaire by id
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestionaireDTO questionaire)
        {
             await _unitOfWork.Questionaire.UpdateAsync(_mapper.Map<QuestionaireEntity>(questionaire));
                return Accepted(questionaire);

        }
        #endregion

        #region Delete Questionaire by id
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
                await _unitOfWork.Questionaire.DeleteQuestionaireAsync(id);
                return Accepted(id);

        }
        #endregion

        #region Restore Questionaire by id
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
              await _unitOfWork.Questionaire.RestoreQuestionaireAsync(id);
                return Accepted(id);

        }
        #endregion

        #region Get Questionaire by VehicleID
        [HttpGet("GetByVehicleType/{id}")]
        public async Task<IActionResult> GetByVehicleType(int id)
        {
            
                var questionaire = await _unitOfWork.Questionaire.GetQuestionaireByVehicleTypeAsync(id);
                if (questionaire == null)
                {
                    return BadRequest(questionaire);
                }
                return Accepted(questionaire);

        }
        #endregion

        #region Get Questionaire by ID
        [HttpGet("GetQuestionaireByID/{id}")]
        public async Task<IActionResult> GetQuestionaireByID(int id)
        {
                var questionaire = await _unitOfWork.Questionaire.GetQuestionaireByIDAsync(id);
                if (questionaire == null)
                {
                    return BadRequest(questionaire);
                }
                return Accepted(questionaire);

        }
        #endregion

        #region Get Questionaire by Company
        [HttpGet("GetQuestionaireByCompany/{Company}")]
        public async Task<IActionResult> GetQuestionaireByCompany(string Company)
        {
                var questionaire = await _unitOfWork.Questionaire.GetQuestionaireByCompanyAsync(Company);
                if (questionaire == null)
                {
                    return BadRequest(questionaire);
                }
                return Accepted(questionaire);
        }
        #endregion

        #region Get Questionaire by Company Test
        [HttpGet("GetQuestionaireByCompanyTest")]
        public async Task<IActionResult> GetQuestionaireByCompanyTest()
        {
            var user = await _unitOfWork.Questionaire.GetQuestionaireByCompanyTestAsync();
            if (user == null)
            {
                return BadRequest(user);
            }
            return Accepted(user);
        }
        #endregion
    }
}
