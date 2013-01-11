using System;
using System.Diagnostics;
using MFlow.Core.Validation;
using MFlow.Performance.Supporting;

namespace MFlow.Performance
{
	class Program
	{
		public static void Main(string[] args)
		{
			var user = new User() { Forename = "test", Surname = "test", Email = "testing" };
			var validator = new FluentValidationFactory().GetFluentValidation(user);
			
			validator
				.Check(c=>c.Forename).IsNotEmpty()
				.Check(c=>c.Surname).IsNotEmpty()
				.Check(c=>c.Email).IsNotEmpty()
				.Check(c=>c.Email).IsEmail();
				
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			
			for(var i = 0; i < 1000000; i++)
			{
				validator
					.Satisfied();
			}
			
			stopwatch.Stop();
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
			Console.Read();
		}
	}
}