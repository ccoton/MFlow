using NUnit.Framework;
using System;

namespace MFlow.Client.Tests.Api
{
    [TestFixture]
    public class ValidationController
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Validation_Controller_Post_Null_Model()
        {
            var validationController = new MFlow.Client.ValidationController();
            validationController.Post(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Validation_Controller_Post_Model_With_Null_Validate()
        {
            var modelToValidate = new ModelToValidate() { Validate = null };
            var validationController = new MFlow.Client.ValidationController();
            validationController.Post(modelToValidate); 
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Validation_Controller_Post_Model_With_Null_Type()
        {
            var modelToValidate = new ModelToValidate() { Validate = this, Type=null };
            var validationController = new MFlow.Client.ValidationController();
            validationController.Post(modelToValidate);  
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Validation_Controller_Post_Model_With_Invalid_Type()
        {
            var modelToValidate = new ModelToValidate() { Validate = this, Type="" };
            var validationController = new MFlow.Client.ValidationController();
            validationController.Post(modelToValidate);  
        }

        [Test]
        public void Test_Validation_Controller_Post_Model()
        {
            
            
        }
    }
}

