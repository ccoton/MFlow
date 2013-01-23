using System;
using System.Linq;
using MFlow.WebApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MFlow.WebApi.Tests.Api
{
    [TestFixture]
    public class SuggestionController
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Suggestion_Controller_Post_Null_Model()
        {
            var validationController = new MFlow.WebApi.Api.SuggestionController();
            validationController.Post(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Suggestion_Controller_Post_Model_With_Null_Validate()
        {
            var modelToValidate = new ModelToValidate() { Validate = null };
            var validationController = new MFlow.WebApi.Api.SuggestionController();
            validationController.Post(modelToValidate); 
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Validation_Controller_Post_Model_With_Null_Type()
        {
            var modelToValidate = new ModelToValidate() { Validate = this, Type=null };
            var validationController = new MFlow.WebApi.Api.ValidationController();
            validationController.Post(modelToValidate);  
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Suggestion_Controller_Post_Model_With_Invalid_Type()
        {
            var modelToValidate = new ModelToValidate() { Validate = this, Type="" };
            var validationController = new MFlow.WebApi.Api.SuggestionController();
            validationController.Post(modelToValidate);  
        }

        [Test]
        public void Test_Suggestion_Controller_Post_Model_With_Suggestions()
        {
            var modelToValidate = new ModelToValidate() { Validate = JsonConvert.SerializeObject(new User()), Type="MFlow.WebApi.Tests, MFlow.WebApi.Tests.User" };
            var validationController = new MFlow.WebApi.Api.SuggestionController();
            var response = validationController.Post(modelToValidate);  
            Assert.AreEqual(response.ToList().Count, 1);
        }

        [Test]
        public void Test_Suggestion_Controller_Post_Model()
        {
            var modelToValidate = new ModelToValidate() { Validate = JsonConvert.SerializeObject(new User() { Username = "test", Password="test"}), Type="MFlow.WebApi.Tests, MFlow.WebApi.Tests.User" };
            var validationController = new MFlow.WebApi.Api.SuggestionController();
            var response = validationController.Post(modelToValidate);  
            Assert.AreEqual(response.ToList().Count, 0);
        }
    }
}
