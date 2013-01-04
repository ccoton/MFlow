using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                .Check(m => m.Code).IsNotEmpty().Message("Code cannot be empty");
        }

        public string Description { get; set; }
        public string Code { get; set; }
    }
}