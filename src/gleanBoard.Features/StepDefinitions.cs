using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace gleanBoard.Features
{
    [Binding]
    class StepDefinitions
    {
        private WebResponse LastResponse { get; set; }

        [When(@"I visit the homepage")]
        public void WhenIVisitTheHomepage()
        {
            LastResponse = WebRequest.CreateDefault(new Uri("http://localhost:9222")).GetResponse();
        }

        [Then(@"I should see a welcome message")]
        public void ThenIShouldSeeAWelcomeMessage()
        {
            var ll = LastResponse.GetResponseStream();
            var kk = new byte[LastResponse.ContentLength];
            ll.Read(kk, 0, kk.Length);
            var output = Encoding.UTF8.GetString(kk);

            Assert.IsTrue(output.Contains("Welcome to gleanBoard"));

        }

    }
}
