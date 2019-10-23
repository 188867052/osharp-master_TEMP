using System;

using OSharp.UnitTest.Infrastructure;

using Shouldly;

using Xunit;

namespace OSharp.Data.Tests
{
    public class SerializeHelperTests
    {
        private readonly TestEntity _entity = new TestEntity() { Id = 1, Name = "test", IsDeleted = false, AddDate = DateTime.Now };

        [Fact]
        public void Binary_Test()
        {
            byte[] bytes = SerializeHelper.ToBinary(this._entity);
            TestEntity newEntity = SerializeHelper.FromBinary<TestEntity>(bytes);
            newEntity.Name.ShouldBe(this._entity.Name);
        }

        [Fact]
        public void BinaryFile_Test()
        {
            string file = "bin-test.tmp";

            SerializeHelper.ToBinaryFile(file, this._entity);
            TestEntity newEntity = SerializeHelper.FromBinaryFile<TestEntity>(file);
            newEntity.Name.ShouldBe(this._entity.Name);
        }

        [Fact]
        public void Xml_Test()
        {
            string xml = SerializeHelper.ToXml(this._entity);
            TestEntity newEntity = SerializeHelper.FromXml<TestEntity>(xml);
            newEntity.Name.ShouldBe(this._entity.Name);
        }

        [Fact]
        public void XmlFile_Test()
        {
            string file = "xml-test.tmp";

            SerializeHelper.ToXmlFile(file,this._entity);
            TestEntity newEntity = SerializeHelper.FromXmlFile<TestEntity>(file);
            newEntity.Name.ShouldBe(this._entity.Name);
        }
    }
}
