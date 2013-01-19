
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;

using MFlow.WebApi.Models;
using Newtonsoft.Json;

namespace MFlow.WebApi.Api
{
    public class ValidationController : ApiController
    {
        /// <summary>
        ///      Handles post requests, trys to validate the passed in model.    
        /// </summary>
        public IEnumerable<ValidationResult> Post(ModelToValidate model)
        {           
            if (model == null)
                throw new ArgumentException("ModelToValidate must have some data to validate");

            if (model.Validate == null)
                throw new ArgumentException("ModelToValidate must have some data to validate");

            if (string.IsNullOrEmpty(model.Type))
                throw new ArgumentException("ModelToValidate must define a type to validate");

            var assembly = model.Type.Split(',').First();
            var type = model.Type.Split(',').Skip(1).First();
            var validateType = System.Reflection.Assembly.Load(assembly).GetType(type);
            if (validateType == null || !typeof(IValidatableObject).IsAssignableFrom(validateType))
                throw new ArgumentException("ModelToValidate must implement IValidatableObject");

            var objectToValidate = (IValidatableObject)JsonConvert.DeserializeObject(model.Validate.ToString(), validateType);

            return objectToValidate.Validate(new ValidationContext(objectToValidate, null, null));

        }
    }
}