# Ski Resort Ticket Pricing

### Overview

Your task is to design and refactor a ski resort ticket pricing system. The system should calculate the price of lift passes based on various factors such as age, type of pass, and specific skiing dates. This application solves the problem of calculating the pricing for ski lift passes. There's some intricate logic linked to what kind of lift pass you want, your age and the specific date at which you'd like to ski. There's a new feature request, be able to get the price for several lift passes, not just one. Currently the pricing for a single lift pass is implemented, unfortunately the code as it is designed is not reusable.

### Main Business Entities

- PriceCalculator
- Ticket

### Business Rules

- Price is calculated based on the age of the skier and the type of pass.
- Child (0-6 years): Free
- Child (7-14 years): 70% off
- Student (15-23 years): 50% off
- Adult (15-63 years): Full price
- Senior (64+ years): 75% off
- If the age is not specified, the default price is the adult price.
- If the pass is a night pass and the skier's age is greater than or equal to 6, the price is 40% off.
- If the pass is a night pass and the skier's age is less than 6, the price is free.
- If the pass is not a holiday pass and it is a Monday, there is a $35 reduction in the price.

### Technical Features

As ticket pricing rules can change frequently, the solution should be designed for maintainability and flexibility.

### Requirements

1. Adhering to the business rules provided for calculating lift ticket price.
2. Implement the ability to calculate the price of a single lift ticket.
3. During development, a new request has come in from the business to allow for pricing multiple lift passes at once.

### Expectations

You have received the following code from a junior developer on your team via a pull request for your review. Based on your expertise as a software engineer, and your understanding of the business requirements, you are required to provide the junior developer with feedback on their implementation. Additionally, we would like for you to come prepared to speak to the following scenario:

_"The junior developer is out sick and will be unable to work on the request so, as the lead engineer on the team, you are asked to implement the changes to the solution to the best of your ability."_

### What are we looking for?

You will be expected to demonstrate your understanding of industry best practices by providing constructive, thoughtful comments and suggestions on the junior developer's PR. Be able to identify common code smells and bad practices in the submitted code. Point out opportunities to refactor the code to adhere to the SOLID principles. Be able to explain the rationale behind your comments, suggestion and design decisions.

1. **Single Responsibility Principle (SRP)**:
   - Each class should have a single responsibility.
   - Separate concerns related to pricing logic, input handling, and data storage.
2. **Open/Closed Principle (OCP)**:
   - Design the system to be open for extension but closed for modification.
   - Accommodate new pricing rules or pass types without altering existing code.
3. **Liskov Substitution Principle (LSP)**:
   - Derived classes (e.g., child, adult, senior, student) should be substitutable for their base class (generic ticket).
4. **Interface Segregation Principle (ISP)**:
   - Interfaces should be specific to the needs of the implementing classes.
   - Avoid fat interfaces with unnecessary methods.
5. **Dependency Inversion Principle**:
   - Depend on abstractions (interfaces or abstract classes) rather than concrete implementations.
   - Use dependency injection to inject dependencies.
