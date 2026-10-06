using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using NZwalks.API.Repository;
using AutoMapper;
using NZwalks.API.CustomActionFilters;
using System.Text.Json;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZwalksDbContext dbContext;

        private readonly IRegionInterface regionrepository;

        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZwalksDbContext dbContext, IRegionInterface regionrepository, IMapper mapper,
            ILogger <RegionsController> logger)

        {
            this.dbContext = dbContext;
            this.regionrepository = regionrepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        //Get all Regions
[HttpGet]
public async Task<IActionResult> GetAll()
        {
            try
            {
                var regions = await regionrepository.GetAllAsync();

                // Map Domain Model to DTO
                var regionDto = mapper.Map<List<RegionDTO>>(regions);

                logger.LogInformation(
                    $"Finished GetAll regions request with data: {JsonSerializer.Serialize(regions)}"
                );

                return Ok(regionDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = e.Message,
                    innerException = e.InnerException?.Message
                });
            }
        }

        //get single Region(Get Region by Id)
        //GET:https://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid:}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
          
            //Get region Domain Model from Database
            var regionDomain = await regionrepository.GetByIdAsync(id);

            //check id id exists
            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }

        //POST To create new Region
        //POST:https://localhost:portnumber/api/regions/{id}
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDto addRegionrequestDto)
        {

            //Map or cvert Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionrequestDto);

            //call the repository          
            regionDomainModel = await regionrepository.CreateAsync(regionDomainModel);

            //Map Domain back to DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

    


        //update region
        //PUT:https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "writer")]
        public async Task<IActionResult>Update([FromRoute]Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto)
        {
            
            //Map DTO to domain model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
               
                 //checks if id exists
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

               //Domian-DTO
                return Ok(mapper.Map<RegionDTO>(regionDomainModel));
            }
            
        

        //Delete Region
        ////DELETE:https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "writer")]
        public async Task< IActionResult> Delete([FromRoute] Guid id)
        {
            //call the repository
            var regionDomainModel = await regionrepository.DeleteAsync(id);

            //check if id exists 
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Domain-DTO
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
         
        }


    }
}
