using QuickLearn.Demo.Models;
using QuickLearn.Demo.XmlUtility;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace QuickLearn.ApiApps.XmlPropertyPromotion.Controllers
{
    [RoutePrefix("disassembler")]
    public class PropertyPromoterController : ApiController
    {

        // POST api/values

        [HttpPost, Route("document")]
        [Metadata("Extract Promoted Properties", "Parses XML document given a set of schemas, and promotes requested properties")]
        [SwaggerResponse(HttpStatusCode.OK, "Promoted Properties", typeof(PropertyBag))]
        [SwaggerResponse(HttpStatusCode.NotFound,"Schema for message type not found in schema store")]
        
        public async Task<IHttpActionResult> GetPropertyBag([FromBody]PropertyPromotionRequest promotionRequest)
        {
            AzureBlobSchemaStore store = new AzureBlobSchemaStore(promotionRequest.DocumentSchemaRootUrl);

            MessageTypeManager manager = new MessageTypeManager(store);
            
            await manager.LoadSchemasFromStoreAsync();
            
            try
            {
                var promotedProperties = manager.ExtractProperties(promotionRequest.XmlContent);

                return Ok(promotedProperties);
            }
            catch (UnrecognizedMessageTypeException)
            {
                return NotFound();
            }
        }

    }
}
