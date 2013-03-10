﻿using Machine.Specifications;
using MFlow.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFlow.Core.Tests.for_ValidationLoader
{
    public class when_creating_an_xml_validation_loader : given.a_validation_loader
    {
        static IFluentValidationLoader loader;

        Because of = () =>
        {
            loader = validation_loader.Create<MFlow.Loaders.Xml.XmlValidationLoader>();
        };

        It should_return_the_correct_implementation = () =>
        {
            loader.ShouldBeOfType<MFlow.Loaders.Xml.XmlValidationLoader>();
        };
    }
}
