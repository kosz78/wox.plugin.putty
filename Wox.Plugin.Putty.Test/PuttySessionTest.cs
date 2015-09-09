namespace Wox.Plugin.Putty.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PuttySessionTest
    {
        [Test]
        public void Empty_putty_session_matches()
        {
            // Arrange
            var puttySession = new PuttySession();
            var expectedResult = "://";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_with_only_identifier_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Identifier = "asdf" };
            var expectedResult = "://";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_with_only_identifier_and_protocol_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Identifier = "poop", Protocol = "asdf" };
            var expectedResult = "asdf://";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_with_only_identifier_and_protocol_and_hostname_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Identifier = "poop", Protocol = "asdf", Hostname = "bar.com" };
            var expectedResult = "asdf://bar.com";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_with_only_identifier_and_protocol_and_username_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Identifier = "poop", Protocol = "asdf", Username = "hello" };
            var expectedResult = "asdf://hello@";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_with_identifier_and_protocol_and_username_and_hostname_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Identifier = "poop", Protocol = "asdf", Username = "foo", Hostname = "bar.com" };
            var expectedResult = "asdf://foo@bar.com";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        public void Putty_session_without_identifier_matches()
        {
            // Arrange
            var puttySession = new PuttySession { Protocol = "asdf", Username = "hello", Hostname = "bar.com" };
            var expectedResult = "asdf://hello@bar.com";

            // Act
            var result = puttySession.ToString();

            // Assert
            Assert.AreEqual(result, expectedResult);
        }
    }
}