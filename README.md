MFlow
=====

MFlow is a simple conditional logic/validation framework for .NET. 

The idea is to improve and give a more fluent approach to conditional and validation checking. Similar to the way that the fluentvalidation framework for .NET works. 

Example...

            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            
			fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123").Satisfied();