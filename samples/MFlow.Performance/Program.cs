using System;
using System.Diagnostics;
using MFlow.Core.Validation;
using MFlow.Performance.Supporting;
using MFlow.Core.Validation.Factories;
using MFlow.Core.Validation.Configuration;
using MFlow.Performance.Statistics;
using MFlow.Core.Statistics;

namespace MFlow.Performance
{
    class Program
    {
        public static void Main(string[] args)
        {
            var user = new User() { Forename = "test", Surname = "test", Email = "testing", LastLogin = DateTime.Now.AddDays(-100) };

            Configuration.Current.WithoutStatistics();
            var validator = new FluentValidationFactory().GetFluentValidation(user);

            validator
                .Check(c => c.Forename).IsNotEqualTo("d")
                .Check(c => c.Forename).IsNotEmpty()
                .Check(c => c.Surname).IsNotEmpty()
                .Check(c => c.Email).IsEmail()
                .Check(c => c.Email).IsUsername()
                .Check(c => c.Email).IsPassword()
                .Check(c => c.Email).IsUrl()
                .Check(c => c.LastLogin).IsThisYear()
                .Check(c => c.LastLogin).IsThisMonth()
                .Check(c => c.LastLogin).IsThisWeek()
                .Check(c => c.LastLogin).IsToday();

            // Validate once to ignore inital load
            validator.Validate();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < 100000; i++)
            {
                validator.Validate();
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
    }
}