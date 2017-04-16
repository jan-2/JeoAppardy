
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using JeoAppardy.Client.Api;

namespace JeoAppardy.Tests
{
    [TestClass]
    public class When_get_a_round
    {
        private Round _sut;

        [TestInitialize]
        public void Setup()
        {
            _sut = new Round();
        }

        [TestMethod]
        public void It_should_contain_four_categories()
        {
            Assert.AreEqual(4, _sut.Categories.Count());
        }

        [TestMethod]
        public void First_answer_of_first_category_should_get_a_question()
        {
            Assert.AreNotEqual(String.Empty, _sut.FirstCategory.FirstAnswer.Question);
        }
    }
}
