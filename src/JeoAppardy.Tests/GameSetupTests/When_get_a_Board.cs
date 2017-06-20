
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using JeoAppardy.Client.Api;

namespace JeoAppardy.Tests.GameSetupTests
{
  [TestClass]
  public class When_get_a_Board
  {
    private Board _sut;

    [TestInitialize]
    public void Setup()
    {
      _sut = Board.FromJson(TestData.OneBoard);
    }

    [TestMethod]
    public void It_should_contain_four_categories()
    {
      Assert.AreEqual(4, _sut.Categories.Count());
    }

    [TestMethod]
    public void Any_Category_should_contain_four_answers()
    {
      Assert.IsTrue(_sut.Categories.Any(c => c.Answers.Count() == 4));
    }

    [TestMethod]
    public void First_answer_of_first_category_should_get_a_question()
    {
      Assert.IsNotNull(_sut.FirstCategory.FirstAnswer.RelatedQuestion);
      Assert.AreNotEqual(String.Empty, _sut.FirstCategory.FirstAnswer.RelatedQuestion);
    }

    [TestMethod]
    public void First_answer_of_first_category_should_get_a_description()
    {
      Assert.IsNotNull(_sut.FirstCategory.FirstAnswer.Asset);
      Assert.AreNotEqual(String.Empty, _sut.FirstCategory.FirstAnswer.Asset);
    }

    [TestMethod]
    public void It_should_get_this_answer_types()
    {
      Assert.AreEqual(AnswerType.Text, _sut.FirstCategory.Answers[0].Type);
      Assert.AreEqual(AnswerType.File, _sut.FirstCategory.Answers[1].Type);
      Assert.AreEqual(AnswerType.Image, _sut.FirstCategory.Answers[2].Type);
      Assert.AreEqual(AnswerType.Text, _sut.FirstCategory.Answers[3].Type);
    }
  }
}
