Imports PaymentSystem.Application
Imports PaymentSystem.BusinessLogic
Imports PaymentSystem.Persistence

Module Program
    Sub Main(args As String())
        Dim employeeRepository As IEmployeeRepository = new EmployeeRepository()
        Dim paymentService As IPaymentService = new PaymentService(employeeRepository)
        paymentService.SendPayment()
    End Sub
End Module
