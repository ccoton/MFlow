MFlow
=====

MFlow is a simple conditional logic/validation framework for .NET. 

The idea is to improve and give a more fluent approach to conditional and validation checking. Similar to the way that the fluentvalidation framework for .NET works. 

Example...

			fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
				.Satisfied();
				
More examples in the Wiki.
				