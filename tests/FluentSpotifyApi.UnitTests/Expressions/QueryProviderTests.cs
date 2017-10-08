using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Expressions.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests.Expressions
{
    [TestClass]
    public class QueryProviderTests
    {
        [TestMethod]
        public void ShouldGetQueryFromASingleStatementPredicate()
        {
            // Arrange + Act
            var query = QueryProvider.Get<QueryFields>(f => f.Genre.Contains("rock"));

            // Assert
            query.Should().Be("genre:rock");
        }

        [TestMethod]
        [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1408:ConditionalExpressionsMustDeclarePrecedence", Justification = "Expression tests")]
        [SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1131:UseReadableConditions", Justification = "Expression tests")]
        public void ShouldGetQueryFromAPredicate()
        {
            // Arrange
            var testAlbum = "test album";
            var testArtist = "test artist";
            var testTrack = "test track";
            var testUpc = "test upc";

            var year1 = 2000;
            var year2 = 17;

            var hipster = Tag.Hipster;

            // Act
            var query = QueryProvider.Get<QueryFields>(f =>
                f.Any.Contains("rock") &&
                f.Album == testAlbum &&
                ((testAlbum == f.Album &&
                testArtist == f.Artist &&
                f.Track != testTrack ||
                hipster != f.Tag ||
                f.Tag == Tag.New ||
                "test genre" != f.Genre &&
                !f.Upc.Contains(testUpc) &&
                f.Isrc == "test isrc") &&
                f.Year == 2015 &&
                (f.Year >= (year1 + year2) &&
                f.Year <= 2019) &&
                !(2020 >= f.Year &&
                f.Year >= 2018)));

            // Assert
            query.Should().Be("rock album:\"test album\" album:\"test album\" artist:\"test artist\" NOT track:\"test track\" OR NOT tag:hipster OR tag:new OR NOT genre:\"test genre\" NOT upc:test upc isrc:\"test isrc\" year:2015 year:2017-2019 NOT year:2018-2020");
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenUnaryOperandIsNotSupported()
        {
            // Arrange + Act + Assert
            ((Action)(() => QueryProvider.Get<QueryFields>(f => !(f.Album == "test album" && f.Artist == "test artist"))))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Match("Unsupported NOT expression operand of type '*' has been found.");
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenExpressionTypeIsNotSupported()
        {
            // Arrange + Act + Assert
            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" | f.Artist == "test artist")))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Match("Unsupported expression of type '*' has been found.");
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenNoneOfTheEqualityOperandsIsAQueryField()
        {
            // Arrange + Act + Assert
            ((Action)(() => QueryProvider.Get<QueryFields>(f => f == null)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Match("One of the equal expression operands must be a query field.");
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenRangeIsNotValid()
        {
            // Arrange + Act + Assert
            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" && f.Year >= 2015)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("Range must be the following sequence: BinaryExpression, ExpressionType, BinaryExpression.");

            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" && f.Year >= 2015 && f.Year < 2017)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("Only bound expressions with 'GreaterThanOrEqual' or 'LessThanOrEqual' operators are supported.");

            var value = 123;
            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" && value >= 2015 && f.Year <= 2017)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("One of the bound expression operands must be a query field.");

            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" && (f.Year >= 2015 || f.Year <= 2017))))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("Only range expressions with 'AndAlso' operator are supported.");

            ((Action)(() => QueryProvider.Get<TestQueryFields>(f => f.Album == "test album" && f.TestValue >= 2015 && f.Year <= 2017)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("Both range bound expressions must reference the same query field.");

            ((Action)(() => QueryProvider.Get<QueryFields>(f => f.Album == "test album" && f.Year >= 2015 && f.Year >= 2017)))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Be("Range bound expressions must define a range.");
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenContainsCallObjectIsNotAQueryField()
        {
            // Arrange + Act + Assert
            ((Action)(() => QueryProvider.Get<QueryFields>(f => "test".Contains("e"))))
                .ShouldThrow<ArgumentException>()
                .Which.Message.Should().Match("The Contains call object expression must be a query field.");
        }

        private class TestQueryFields : QueryFields
        {
            [System.ComponentModel.Description("testvalue")]
            public int TestValue { get; }
        }
    }
}
