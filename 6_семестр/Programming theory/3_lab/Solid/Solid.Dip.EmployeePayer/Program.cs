using Solid.Dip.Persistance;
using Solid.Dip.Personnel;
using Solid.Dip.Notifications;
using Solid.Dip.Payment;

var serializer = new EmployeeSerializer();
var fileWorker = new CsvFileWorker();

var repository = new FileRepository<Employee>(fileWorker, serializer);
var notifier = new Notifier();

var paymentProcessor = new PaymentProcessor<Employee>(repository, notifier);

var totalPayments = paymentProcessor.SendPayments();
Console.WriteLine($"Total payments: ${totalPayments}");

Console.ReadKey();