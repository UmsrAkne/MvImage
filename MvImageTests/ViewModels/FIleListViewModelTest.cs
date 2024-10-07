using System.IO.Abstractions;
using Moq;
using MvImage.ViewModels;
using NUnit.Framework;

namespace MvImageTests.ViewModels
{
    [TestFixture]
    public class FIleListViewModelTest
    {
        [Test]
        public void LoadFiles_test()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            var mockFileInfos = new List<IFileInfo>
            {
                MockFileInfo("file1.safetensors", ".safetensors"),
                MockFileInfo("file2.safetensors", ".safetensors"),
                MockFileInfo("file3.txt", ".txt")
            };

            // Mock GetFiles to return file paths
            mockFileSystem.Setup(fs => fs.Directory.GetFiles(It.IsAny<string>()))
                .Returns(new[] { "file1.safetensors", "file2.safetensors", "file3.txt", });

            // Mock FileInfo.New to return corresponding IFileInfo instances
            mockFileSystem.Setup(fs => fs.FileInfo.New(It.IsAny<string>()))
                .Returns((string path) => mockFileInfos.First(f => f.Name == path));

            var fileListViewModel = new FileListViewModel(mockFileSystem.Object);

            // Act
            var result = fileListViewModel.LoadFiles("dummyDirectory");

            // Assert
            Assert.Multiple(() =>
            {
                var fileInfos = result.ToList();
                Assert.That(fileInfos, Has.Count.EqualTo(2));
                Assert.That(fileInfos, Is.All.Property("Extension").EqualTo(".safetensors"));
            });
        }

        private static IFileInfo MockFileInfo(string name, string extension)
        {
            var mockFileInfo = new Mock<IFileInfo>();
            mockFileInfo.Setup(f => f.Name).Returns(name);
            mockFileInfo.Setup(f => f.Extension).Returns(extension);
            return mockFileInfo.Object;
        }
    }
}