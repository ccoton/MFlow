using MFlow.Mvc;

namespace MFlow.Samples.WebApi.Models
{
    public class CreateAuditEventModel : ValidatedModel<CreateAuditEventModel>
    {
        public CreateAuditEventModel()
        {
            SetTarget(this);

            Validator
                .Check(m => m.Description).IsNotEmpty().Message("Description cannot be empty")
                .Check(m => m.Code).IsNotEmpty().Message("Code cannot be empty")
                .Check(m => m.SecurityKey)
                    .Matches(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$")
                    .Message("Security Key is invalid");
        }

        public string SecurityKey { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}