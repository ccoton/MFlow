using MFlow.Core.Tests.Supporting;
using NUnit.Framework;

namespace MFlow.Core.Tests.Validation
{
    public partial class FluentValidation
    {

        [Test]
        public void Test_Fluent_Validation_IsLength_Valid()
        {
            var user = new User {
	Username = "ausername@somedomain.com"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsLength(24).Satisfied());
        }

       [Test]
       public void Test_Fluent_Validation_IsLength_InValid()
       {
            var user = new User {
	Username = "aaa"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                        .Check(u => u.Username).IsLength(24).Satisfied());
       }

        [Test]
        public void Test_Fluent_Validation_IsLength_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsLength(24).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsLongerThan_Valid()
        {
            var user = new User {
	Username = "afunnyusername@somedomain.com"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsLongerThan(24).Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsLongerThan_InValid()
		{
			var user = new User {
	Username = "afu"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsLongerThan(24).Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsLongerThan_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsLongerThan(24).Satisfied());
        }
        
        [Test]
        public void Test_Fluent_Validation_IsShorterThan_Valid()
        {
            var user = new User {
	Username = "afunnyusername@somedomain.com"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsShorterThan(40).Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsShorterThan_InValid()
		{
			var user = new User {
	Username = "aaaaaaaaaaaaaaaafunnyusername@somedomain.com"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsShorterThan(40).Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsShorterThan_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsLongerThan(40).Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsCeditCard_Valid()
        {
            var user = new User {
	Username = "5105 1051 0510 5100"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsCreditCard().Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsCeditCard_InValid()
		{
			var user = new User {
	Username = "5105"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsCreditCard().Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsCreditCard_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsCreditCard().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsPostCode_Valid()
        {
            var user = new User {
	Username = "B69 1TE"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsPostCode().Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsPostCode_InValid()
		{
			var user = new User {
	Username = "test"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsPostCode().Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsPostCode_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsPostCode().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsZipCode_Valid()
        {
            var user = new User {
	Username = "35801"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsZipCode().Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsZipCode_InValid()
		{
			var user = new User {
	Username = "35801PPPPP"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsZipCode().Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_IsZipCode_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsZipCode().Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_IsNumeric_Valid()
		{
			var user = new User {
	Username = "35801"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsTrue(fluentValidation
			              .Check(u => u.Username).IsNumeric().Satisfied());
		}

		[Test]
		public void Test_Fluent_Validation_IsNumeric_InValid()
		{
			var user = new User {
	Username = "35801A"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsNumeric().Satisfied());
		}
		
		[Test]
		public void Test_Fluent_Validation_IsNumeric_When_Null()
		{
			var user = new User {
	Username = null
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			               .Check(u => u.Username).IsNumeric().Satisfied());
		}

		[Test]
		public void Test_Fluent_Validation_IsAlpha_Valid()
		{
			var user = new User {
	Username = "abcd"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsTrue(fluentValidation
			              .Check(u => u.Username).IsAlpha().Satisfied());
		}

		[Test]
		public void Test_Fluent_Validation_IsAlpha_InValid()
		{
			var user = new User {
	Username = "1abcd"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).IsAlpha().Satisfied());
		}
		
		[Test]
		public void Test_Fluent_Validation_IsAlpha_When_Null()
		{
			var user = new User {
	Username = null
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			               .Check(u => u.Username).IsAlpha().Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_RegEx_Valid()
        {
            var user = new User {
	Username = "ausername@somedomain.com"
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
        }

		[Test]
		public void Test_Fluent_Validation_RegEx_InValid()
		{
			var user = new User {
	Username = "ausernamesomedomain.com"
};
			var fluentValidation = _factory.GetFluentValidation<User>(user);
			Assert.IsFalse(fluentValidation
			              .Check(u => u.Username).Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
		}

        [Test]
        public void Test_Fluent_Validation_RegEx_When_Null()
        {
            var user = new User {
	Username = null
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Matches(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_With_Valid_Value()
        {
            var user = new User {
	Password = "password123",
	Username = "ausername@somedomain.com",
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_With_InValid_Value()
        {
            var user = new User {
	Password = "password123",
	Username = "ausername",
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_IsEmail_When_Null()
        {
            var user = new User {
	Password = "password123",
	Username = null,
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).IsEmail().Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_With_Valid_Value()
        {
            var user = new User {
	Password = "password123",
	Username = "ausername@somedomain.com",
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsTrue(fluentValidation
                .Check(u => u.Username).Contains("username").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_With_InValid_Value()
        {
            var user = new User {
	Password = "password123",
	Username = "ausername",
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Contains("testing").Satisfied());
        }

        [Test]
        public void Test_Fluent_Validation_Contains_When_Null()
        {
            var user = new User {
	Password = "password123",
	Username = null,
	LoginCount = 12
};
            var fluentValidation = _factory.GetFluentValidation<User>(user);
            Assert.IsFalse(fluentValidation
                .Check(u => u.Username).Contains("test").Satisfied());
        }
    }
}
