using AutoMapper;
using BlazorApp1.Client.Services.Interfaces;
using BlazorApp1.Server.Entities;
using BlazorApp1.Shared.QuestionsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public QuestionsController(IUnitOfWork unitOfWork, ILogger<QuestionsController> logger, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        #region Create new Question
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionDTO question)
        {
                await _unitOfWork.Questions.AddAsync(_mapper.Map<QuestionsEntity>(question));
                return Accepted(question);
        }
        #endregion

        #region Update Question by id
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CreateQuestionDTO question)
        {
                await _unitOfWork.Questions.UpdateAsync(_mapper.Map<QuestionsEntity>(question));
                return Accepted(question);

        }
        #endregion

        #region Delete Question by id
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
                await _unitOfWork.Questions.DeleteQuestionAsync(id);
                return Accepted(id);

        }
        #endregion

        #region Restore Question by id
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
          
                await _unitOfWork.Questions.RestoreQuestionAsync(id);
                return Accepted(id);

           
        }
        #endregion

        #region Get Question by ID
        [HttpGet("GetQuestionByID/{id}")]
        public async Task<IActionResult> GetQuestionByIDAsync(int id)
        {
                var question = await _unitOfWork.Questions.GetQuestionByIDAsync(id);
                if (question == null)
                {
                    return BadRequest(question);
                }
                return Accepted(question);

        }
        #endregion

        #region Get Question by Questionaire
        [HttpGet("GetQuestionByQuestionare/{id}")]
        public async Task<IActionResult> GetQuestionByQuestionare(int id)
        {
            var question = await _unitOfWork.Questions.GetQuestionByQuestionareAsync(id);
                if (question == null)
                {
                    return BadRequest(question);
                }
                return Accepted(question);
        }
        #endregion

        #region Get Question by Questionaire
        [HttpGet("GetQuestionByCompany/{company}")]
        public async Task<IActionResult> GetQuestionByCompany(string company)
        {
            var question = await _unitOfWork.Questions.GetQuestionByCompanyAsync(company);
            if (question == null)
            {
                return BadRequest(question);
            }
            return Accepted(question);
        }
        #endregion

        #region Get Questions by Company Test
        [HttpGet("GetQuestionsByCompanyTest")]
        public async Task<IActionResult> GetQuestionsByCompanyTest()
        {
            var user = await _unitOfWork.Questions.GetQuestionsByCompanyTestAsync();
            if (user == null)
            {
                return BadRequest(user);
            }
            return Accepted(user);
        }
        #endregion
    }
}
