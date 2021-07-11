using LupExercise_ArchieGonzales.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LupExercise_ArchieGonzales.Controllers.Base
{
    [ApiController]
    public class AbstractAPIController : ControllerBase
    {
        public ApplicationDbContext _dataContext { get; private set; }
        public ILogger _logger { get; }
        public AbstractAPIController(ApplicationDbContext dataContext, ILogger logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
    }
}
