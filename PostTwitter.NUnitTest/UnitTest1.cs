using NUnit.Framework;
using PostTwitter.Model;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void Test1()
        {
            Execucao execucao = new Execucao
            {
                Id = 1,
                Dataexecucao = DateTime.Now,
                idStatus = (int)ENUM.STATUS.EM_PROCESSAMENTO,
                Usuario = "Tests",
            };

            List<Twitters> list = new List<Twitters>();
            list.Add(new Twitters()
            {
                idHashTag = 1,
                idioma = "portugues",
                texto = "texto texto texto",
            });

            //// Moq
            //Mock<IBLL> mock = new Mock<IBLL>();
            //mock.Setup(m => m.PostagensTwitter(null, null)).Returns(list);
            ////mock.Setup(m => m.ListarPostagensTwitter()).Returns(new Execucao());
            //var result = mock.Object.ListarPostagensTwitter();
            
            ////Substitute
            //var teste = Substitute.For<BLL>();
            //teste.When(a => a.PostagensTwitter(null, null).Returns(list));
            //var result = teste.ListarPostagensTwitter();
        }
    }
}