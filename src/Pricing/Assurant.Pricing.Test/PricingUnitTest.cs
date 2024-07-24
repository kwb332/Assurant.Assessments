namespace Assurant.Pricing.Test
{
    using Assurant.Pricing.Domain.Contract.DomainModel;
    using Assurant.Pricing.Domain.RuleEngine;
    using Assurant.Pricing.Infrastructure.Contract.Interface;
    using Moq;
    
    public class Tests
    {
        Mock<IHolidayRepository> holidayRepository;
        Mock<IPriceRepository> pricingRepository;


        [SetUp]
        public void Setup()
        {
             holidayRepository = new Mock<IHolidayRepository>();
             pricingRepository = new Mock<IPriceRepository>();

            List<DateTime> dates = new List<DateTime>();
            DateTime dateTime1 = new DateTime(2025, 01, 01);
            DateTime dateTime2 = new DateTime(2024, 11, 28);
            DateTime dateTime3 = new DateTime(2024, 12, 24);
            DateTime dateTime4 = new DateTime(2024, 12, 25);
            DateTime dateTime5 = new DateTime(2024, 12, 26);
            DateTime dateTime6 = new DateTime(2024, 11, 11);
            DateTime dateTime7 = new DateTime(2025, 09, 02);

            dates.Add(dateTime1);
            dates.Add(dateTime2);
            dates.Add(dateTime3);
            dates.Add(dateTime4);
            dates.Add(dateTime5);
            dates.Add(dateTime6);
            dates.Add(dateTime7);


            holidayRepository.Setup(h => h.GetHolidays()).Returns(dates);
            string ticketNight = "NIGHT";
            pricingRepository.Setup(h => h.GetPrice(ticketNight)).Returns(100);

            string ticketDay = "Day";
            pricingRepository.Setup(h => h.GetPrice(ticketDay)).Returns(200);

         
        }

        [Test]
        public void TestDefaultRuleEngine()
        {

            DefaultRuleEngine defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticket = new Ticket();
            ticket.Age = 3;
            ticket.Type = "Day";
            var price = defaultRuleEngine.CalculatePrice(ticket, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(200, price);
        }
        [Test]
        public void TestAgeDecorator()
        {

            RuleEngine defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketBlowSix = new Ticket();
            ticketBlowSix.Age = 3;
            ticketBlowSix.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);
         
            var price = defaultRuleEngine.CalculatePrice(ticketBlowSix, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(0, price);


            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketSevenAbove = new Ticket();
            ticketSevenAbove.Age = 7;
            ticketSevenAbove.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);

            price = defaultRuleEngine.CalculatePrice(ticketSevenAbove, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(60, price);



            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketFiftenAbove = new Ticket();
            ticketFiftenAbove.Age = 16;
            ticketFiftenAbove.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);

            price = defaultRuleEngine.CalculatePrice(ticketFiftenAbove, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(100, price);



            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticket23Above = new Ticket();
            ticket23Above.Age = 30;
            ticket23Above.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);

            price = defaultRuleEngine.CalculatePrice(ticket23Above, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(200, price);



            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticket64Above = new Ticket();
            ticket64Above.Age = 65;
            ticket64Above.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);

             price = defaultRuleEngine.CalculatePrice(ticket64Above, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(50, price);




        }

        [Test]
        public void TestDateDecorator()
        {

            RuleEngine defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketBlowSix = new Ticket();
            ticketBlowSix.Age = 3;
            ticketBlowSix.Type = "NIGHT";

            defaultRuleEngine = new DateRuleEngineDecorator(defaultRuleEngine);

            var price = defaultRuleEngine.CalculatePrice(ticketBlowSix, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(0, price);


            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketAboveSix = new Ticket();
            ticketAboveSix.Age = 20;
            ticketAboveSix.Type = "NIGHT";

            defaultRuleEngine = new DateRuleEngineDecorator(defaultRuleEngine);

            price = defaultRuleEngine.CalculatePrice(ticketAboveSix, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(60, price);





        }
        [Test]
        public void TestDateDecoratorAndAgeDecorator()
        {

            RuleEngine defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketBlowSix = new Ticket();
            ticketBlowSix.Age = 3;
            ticketBlowSix.Type = "Day";

            defaultRuleEngine = new AgeRuleEngineDecorator(defaultRuleEngine);

            var price = defaultRuleEngine.CalculatePrice(ticketBlowSix, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(0, price);


            defaultRuleEngine = new DefaultRuleEngine();
            Ticket ticketAboveSix = new Ticket();
            ticketAboveSix.Age = 20;
            ticketAboveSix.Type = "NIGHT";

            defaultRuleEngine = new DateRuleEngineDecorator(defaultRuleEngine);

            price = defaultRuleEngine.CalculatePrice(ticketAboveSix, pricingRepository.Object, holidayRepository.Object);
            Assert.AreEqual(60, price);





        }
    }
}