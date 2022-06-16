var serializer = new Solid.Dip.Persistance.EmployeeFileSerializer();
var employeeRepository = new Solid.Dip.Persistance.EmployeeFileRepository(serializer);
var emplpyeeNotifier = new Solid.Dip.Notifications.EmployeeNotifier();

var paymentProcessor = new Solid.Dip.Payment.PaymentProcessor(employeeRepository, emplpyeeNotifier);
var totalPayments = paymentProcessor.SendPayments();
Console.WriteLine($"Total payments: ${totalPayments}");