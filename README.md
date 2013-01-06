MFlow
=====

MFlow is a simple conditional logic/validation framework for .NET. 

The idea is to improve and give a more fluent approach to conditional and validation checking. Similar to the way that the fluentvalidation framework for .NET works. 

Example:

    fluentValidation
        .Check(u => u.Username).IsEqualTo("testing")
        .Check(u => u.Password).IsEqualTo("password123")
        .Check(u => u.Email).IsEmail()
        .Satisfied();
				
A simple example like the one above can also be configured using XML:

    <?xml version="1.0" encoding="utf-8" ?>
    <FluentValidation>
      <Validator>
        <Rules>
          <Equal property="Username" value="testing" />
          <Equal property="Password" value="password123" />
          <IsEmail property="Email" />
        </Rules>
      </Validator>
    </FluentValidation>
                
Or VML...

    [Display]  [When] Username [Is] NotEqual [To] testing
    [Display]  [When] Username [Is] NotEqual [To] password123
    [Display]  [When] Username [Is] NotAnEmail     

In all three of the above examples when an explicit error message is not defined the following happens. 

The validator will look for a resource xml file for the current culture and derive a default message based on the condition applied. If a resource file does not exist for the current culture the default resource (Messages.en.xml) file will be used:

    <Messages>
      <NotEmpty>{0} should not be empty</NotEmpty>
      <Equal>{0} should be equal to {1}</Equal>
      <NotEqual>{0} should not be equal to {1}</NotEqual>
      <LessThan>{0} should be less than {1}</LessThan>
      <LessThanOrEqualTo>{0} should be less than or equal to {1}</LessThanOrEqualTo>
      <GreaterThan>{0} should be greater than {1}</GreaterThan>
      <GreaterThanOrEqualTo>{0} should be greater than or equal to {1}</GreaterThanOrEqualTo>
      <RegEx>{0} should validate expression {1}</RegEx>
      <IsEmail>{0} should be an email</IsEmail>
      <Contains>{0} should contain {1}</Contains>
      <Before>{0} should be before {1}</Before>
      <After>{0} should be after {1}</After>
      <On>{0} should be on {1}</On>
    </Messages>

There is also, purely for example purposes a french resource file (Messages.fr-FR.xml) that looks like this:

    <Messages>
      <NotEmpty>{0} ne doit pas être vide</NotEmpty>
      <Equal>{0} doit être égale à {1}</Equal>
      <NotEqual>{0} ne doit pas être égal à {1}</NotEqual>
      <LessThan>{0} doit être inférieure à {1}</LessThan>
      <LessThanOrEqualTo>{0} doit être inférieure ou égale à {1}</LessThanOrEqualTo>
      <GreaterThan>{0} doit être supérieure à {1}</GreaterThan>
      <GreaterThanOrEqualTo>{0} doit être supérieure ou égale à {1}</GreaterThanOrEqualTo>
      <RegEx>{0} devrait valider l'expression {1}</RegEx>
      <IsEmail>{0} doit être un e-mail</IsEmail>
      <Contains>{0} doit contenir {1}</Contains>
      <Before>{0} doit être avant {1}</Before>
      <After>{0} devrait être après {1}</After>
      <On>{0} doit être mis sur {1}</On>
    </Messages>

You can see from the quick example how easy it can be to setup some conditions and display the default validation messages in the correct culture for the user. 
    
The above resource files can also be extended. Creating a resource file called Custom.Messages.fr-FR for example like so:

    <Messages>
      <EmailAddressShouldBeValid>La valeur entrée doit être une adresse e-mail</EmailAddressShouldBeValid>
    </Messages>

You can then reference this custom error message in either the fluent interface, XML or VML like so:

Fluent Interface:

    fluentValidation
        .Check(u => u.Username).IsEmail().Message("$EmailAddressShouldBeValid$")

XML:

    <IsEmail property="Email" message="$EmailAddressShouldBeValid$" />

VML:

    [Display] $EmailAddressShouldBeValid$ [When] Email [Is] NotAnEmail

When you reference the error message like this, the same process described above will happen and the validator will attempt to load a message for the current culture first and then fall back to the default.
    
The following validation checks are suported in the fluent interface/xml/vml config...

	Generic:
	
		IsEqual,
		IsNotEqual,
		IsRequired,
	
	Strings:
	
		IsNotEmpty,
		IsEmail,
		IsLength,
		IsLongerThan,
		IsShorterThan,
		IsCreditCard,
		IsPostCode,
		IsZipCode,
		Contains,
		Mathes (a regex),
	
	Numbers:
	
		IsLessThan,
		IsGreaterThan,
		IsLessThanOrEqualTo,
		IsGreaterThanOrEqualTo,

	Dates:
	
		IsBefore,
		IsAfter, 
		IsOn,
		IsThisYear,
		IsThisMonth,
		IsThisWeek,
		IsToday
				