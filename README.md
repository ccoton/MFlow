MFlow
=====

MFlow is a simple conditional logic/validation framework for .NET. 

The idea is to improve and give a more fluent approach to conditional and validation checking. Similar to the way that the fluentvalidation framework for .NET works. 

Example...

            var user = new User() { Password = "password123", Username = "testingx" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
            
			fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
				.Satisfied();
				
Or....

            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
			
            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Then(() => {
                    user.Username = "valid";
                });
				
Or....

            var user = new User() { Password = "password123", Username = "testing" };
            var fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);
			
            fluentValidation
                .If(u => String.IsNullOrEmpty(u.Password))
                .Throw(new ArgumentException("Password");
				
Maybe even raise an event, and subscribe to it....

            var user = new User() { Password = "password123", Username = "testing" };
            IFluentValidation<User> fluentValidation = new MFlow.Core.Validation.FluentValidation<User>(user);

            var events = new EventsFactory().GetEventStore();
            events.Register<UserCreatedEvent>(s =>
                {
                    s.Source.Username = "caught event";
                    user = s.Source;
                });

            fluentValidation
                .If(u => u.Username == "testing")
                .And(u => u.Password == "password123")
                .Raise(new UserCreatedEvent(user));
				
				