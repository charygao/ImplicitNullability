﻿using System;
using FluentAssertions;
using ImplicitNullability.Sample.NullabilityAnalysis;
using NUnit.Framework;

namespace ImplicitNullability.Sample.Tests.NullabilityAnalysis
{
    [TestFixture]
    public class IndexersSampleTests
    {
        private IndexersSample _instance;

        [SetUp]
        public void SetUp()
        {
            _instance = new IndexersSample();
        }

        [Test]
        public void IndexerSetterWithNonNullValue()
        {
            Action act = () => _instance["a"] = null;

            act.ShouldNotThrow();
        }

        [Test]
        public void IndexerSetterWithNullValue()
        {
            Action act = () => _instance[null /*Expect:AssignNullToNotNullAttribute*/] = null;

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("a");
        }

        [Test]
        public void IndexerGetterWithNonNullValue()
        {
            Action act = () => IgnoreValue(_instance["a"]);

            act.ShouldNotThrow();
        }

        [Test]
        public void IndexerGetterWithNullValue()
        {
            Action act = () => IgnoreValue(_instance[null /*Expect:AssignNullToNotNullAttribute*/]);

            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("a");
        }

        [Test]
        public void IndexerSetterWithNullableParameter()
        {
            Action act = () => _instance[canBeNull: null, nullableInt: null, optional: null] = null;

            act.ShouldNotThrow();
        }

        [Test]
        public void IndexerGetterWithNullableParameter()
        {
            Action act = () => IgnoreValue(_instance[canBeNull: null, nullableInt: null, optional: null]);

            act.ShouldNotThrow();
        }

        private void IgnoreValue(object value)
        {
        }
    }
}